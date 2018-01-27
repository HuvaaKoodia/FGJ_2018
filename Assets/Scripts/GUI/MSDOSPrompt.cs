using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MSDOSPrompt : MonoBehaviour
{
	#region variables

	public class DosNode
	{
		public List<string> startTextLines;
		public Dictionary<string, DosNode> choices;

		public bool showChoices = true;
		public string name;

		public DosNode(string name)
		{
			this.name = name;
			choices = new Dictionary<string, DosNode>();
			startTextLines = new List<string>();
		}
	}

	public const string homeAddress = "1.2.3.4";
	public const string server1_1Address = "12.2.41.77";
	public const string server2_1Address = "edu.Kamk.fi";
	public const string server3_1Address = "corp.eztech.biz";
	public const string server4Address = "target.goal.destination";

	public Window window;
	public InputField textArea, commandField;
	public Scrollbar scrollbar;

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

		var serverInputNode = new DosNode("");
		var homeConnections = new DosNode("");
		var server1_1Connections = new DosNode(server1_1Address);
		var server2_1Connections = new DosNode(server2_1Address);
		var server3_1Connections = new DosNode(server3_1Address);
		var server4Connections = new DosNode(server4Address);

		serverInputNode.startTextLines.Add("Input server address:");

		serverInputNode.showChoices = false;
		serverInputNode.choices.Add(homeAddress, homeConnections);
		serverInputNode.choices.Add(server1_1Address, server1_1Connections);
		serverInputNode.choices.Add(server2_1Address, server2_1Connections);
		serverInputNode.choices.Add(server3_1Address, server3_1Connections);

		var sharedTextLines = new List<string>();
		sharedTextLines.Add("This server has forward connections.");
		sharedTextLines.Add("Choose a connection to inspect, or input q to quit");

		homeConnections.choices.Add("1", server1_1Connections);
		homeConnections.choices.Add("2", server4Connections);
		homeConnections.choices.Add("q", null);

		homeConnections.startTextLines = sharedTextLines;

		server1_1Connections.choices.Add("1", server2_1Connections);
		server1_1Connections.choices.Add("2", server4Connections);
		server1_1Connections.choices.Add("q", null);

		server1_1Connections.startTextLines = sharedTextLines;

		server2_1Connections.choices.Add("1", server3_1Connections);
		server2_1Connections.choices.Add("2", server4Connections);
		server2_1Connections.choices.Add("q", null);

		server2_1Connections.startTextLines = sharedTextLines;

		server3_1Connections.choices.Add("1", server4Connections);
		server3_1Connections.choices.Add("q", null);

		server4Connections.startTextLines.Add("This server has no forward connections.");
		server4Connections.startTextLines.Add("Input q to quit.");

		server4Connections.choices.Add("q", null);

		if(programEnumerator != null)
			StopCoroutine(programEnumerator);

		programEnumerator = ProcessProgramCoroutine(serverInputNode);
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

		while(node != null)
		{
			commandField.readOnly = true;
			yield return new WaitForSeconds(0.9f);

			for(int i = 0; i < node.startTextLines.Count; i++)
			{
				textArea.text += "\n"+ node.startTextLines[i];
				yield return null;
				scrollbar.value = 0;

				Canvas.ForceUpdateCanvases();
				LayoutRebuilder.ForceRebuildLayoutImmediate(window.GetComponent<RectTransform>());
				yield return new WaitForSeconds(0.4f);
			}

			if(node.showChoices)
			{
				foreach(var choice in node.choices)
				{
					if(choice.Value == null) continue;
					textArea.text += string.Format("\n   {0} - {1}", choice.Key, choice.Value.name);
					yield return null;
					scrollbar.value = 0;

					Canvas.ForceUpdateCanvases();
					LayoutRebuilder.ForceRebuildLayoutImmediate(window.GetComponent<RectTransform>());
					yield return new WaitForSeconds(0.4f);
				}
			}

			while(true)
			{
				//textArea.text += "\n";
				int inputTextLineCount = GetTextLineCount();

				commandField.readOnly = false;
				commandField.text = "";

				yield return null;
				scrollbar.value = 0;

				yield return null;

				commandField.Select();
				Canvas.ForceUpdateCanvases();
				LayoutRebuilder.ForceRebuildLayoutImmediate(window.GetComponent<RectTransform>());

				bool notSelected = true;
				while(true)
				{
					if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject == commandField.gameObject)
					{
						notSelected = false;
					}
					else if (notSelected)
						commandField.Select();

					if(Input.anyKeyDown)
					{
						commandField.Select();
					}
						
					if(Input.GetKeyDown(KeyCode.Return)) break;
					yield return null;
				}
				commandField.readOnly = true;
				string input = commandField.text;
				commandField.text = "";
				textArea.text += "\n" + input;
				scrollbar.value = 0;

				yield return new WaitForSeconds(0.3f);

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
