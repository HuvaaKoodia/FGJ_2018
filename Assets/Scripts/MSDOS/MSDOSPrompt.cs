using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public delegate DosNode CustomDosNodeAction(string input);
public delegate bool StrintToBoolEvent(string input);

public class DosNode
{
	public string programStartCommand = null;

	public List<string> startTextLines;
	public Dictionary<string, DosNode> choices;
	public DosNode nextNode = null;

	public bool showChoices = true;
	public string name;
	public CustomDosNodeAction customChoiceInputAction;
	public System.Action onStartEvent;
	public StrintToBoolEvent onChoiceAvailable;
	
	public DosNode(string name)
	{
		this.name = name;
		choices = new Dictionary<string, DosNode>();
		startTextLines = new List<string>();
	}
}

public class MSDOSPrompt : MonoBehaviour
{
	#region variables

	public RectTransform scrollViewRect;
	public Window window;
	public InputField textArea, commandField;
	public Scrollbar scrollbar;

	public DosNode startNode;

	IEnumerator programEnumerator;
	#endregion

	#region initialization

	private void Awake()
	{
		window.onWindowOpen.AddListener(OnOpen);
	}

	private void OnOpen()
	{
		textArea.text = "Microsoft(R) Windows 95\n    (C)Copyright Microsoft Corp 1981-1995\n";
		commandField.text = "";

		if(programEnumerator != null)
			StopCoroutine(programEnumerator);

		programEnumerator = ProcessProgramCoroutine(startNode);
		StartCoroutine(programEnumerator);
	}

	#endregion

	#region logic

	#endregion

	#region public interface

	#endregion

	#region private interface

	IEnumerator ProcessProgramCoroutine(DosNode node)
	{
		scrollbar.value = 1;
		commandField.readOnly = true;

		if(node.programStartCommand != null)
		{
			yield return new WaitForSeconds(0.2f);
			textArea.text += "\nC:/> " + node.programStartCommand+"\n";
			yield return new WaitForSeconds(0.4f);
		}

		while(node != null)
		{
			if (node.onStartEvent != null) node.onStartEvent();

			LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewRect);
			commandField.readOnly = true;
			yield return new WaitForSeconds(0.9f);

			for(int i = 0; i < node.startTextLines.Count; i++)
			{
				textArea.text += "\n"+ node.startTextLines[i];
				yield return null;
				scrollbar.value = 0;

				LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewRect);
				yield return new WaitForSeconds(0.4f);
			}

			if(node.nextNode != null)
			{
				node = node.nextNode;
				continue;
			}

			if(node.showChoices)
			{
				foreach(var choice in node.choices)
				{
					if(choice.Value == null) continue;
					if(node.onChoiceAvailable != null && !node.onChoiceAvailable(choice.Key)) continue;

					textArea.text += string.Format("\n   {0} - {1}", choice.Key, choice.Value.name);
					yield return null;
					scrollbar.value = 0;

					LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewRect);
					yield return new WaitForSeconds(0.4f);
				}
			}

			while(true)
			{
				int inputTextLineCount = GetTextLineCount();

				commandField.readOnly = false;
				commandField.text = "";

				yield return null;
				scrollbar.value = 0;

				yield return null;
				LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewRect);
				commandField.Select();
				yield return null;
				commandField.Select();
				yield return null;

				commandField.Select();

				//bool notSelected = true;
				while(true)
				{
//					if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == commandField.gameObject)
//					{
//						notSelected = false;
//					}
//					else if (notSelected)
//						commandField.Select();
//
//					if(Input.anyKey)
//					{
//						commandField.Select();
//					}
					LayoutRebuilder.ForceRebuildLayoutImmediate(scrollViewRect);

					if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == commandField.gameObject && Input.GetKeyDown(KeyCode.Return)) 
						break;
					
					yield return null;
				}
				commandField.readOnly = true;
				string input = commandField.text.Replace("\n", "").Trim();
				commandField.text = "";
				textArea.text += "\n" + input;
				scrollbar.value = 0;

				yield return new WaitForSeconds(0.3f);


				if(node.customChoiceInputAction != null)
				{
					node = node.customChoiceInputAction(input);
					break;
				} else
				{
					DosNode nextNode;
					if(node.choices.TryGetValue(input, out nextNode))
					{
						node = nextNode;
						break;
					} else
					{
						textArea.text += "\nBad command";
						scrollbar.value = 0;
						yield return null;
					}
				}
			}
		}

		textArea.text += "\nProgram terminated";
		programEnumerator = null;
		scrollbar.value = 0;
	}

	int GetTextLineCount()
	{
		return textArea.text.Split('\n').Length;
	}

	#endregion

	#region events

	#endregion
}
