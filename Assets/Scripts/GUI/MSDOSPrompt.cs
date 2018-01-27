using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MSDOSPrompt : MonoBehaviour
{
	#region variables

	public Window window;

	public class DosProgram
	{
		//Dictionary<string, DosNode> nodeTable;
		public DosNode startNode;

		public DosProgram()
		{
			//nodeTable = new Dictionary<string, DosNode>();
		}

		//public void AddNode(DosNode node)
		//{
		//	nodeTable.Add(node);
		//}
	}

	public class DosNode
	{
		public List<string> startTextLines;
		public Dictionary<string, DosNode> choices;

		public bool showChoices = true;

		public DosNode()
		{
			choices = new Dictionary<string, DosNode>();
			startTextLines = new List<string>();
		}
	}

	public InputField inputField;
	IEnumerator programEnumerator;
	public const string homeAddress = "1.2.3.4";
	public const string server1_1Address = "12.2.41.77";
	public const string server2_1Address = "edu.Kamk.fi";
	public const string server3_1Address = "corp.eztech.biz";
	public const string server4Address = "target.goal.destination";

	#endregion

	#region initialization

	private void Awake()
	{
		window.onWindowOpen.AddListener(OnOpen);
	}

	private void OnOpen()
	{
		inputField.text = "Microsoft(R) Windows 95\n    (C)Copyright Microsoft Corp 1981-1995\n";

		var program = new DosProgram();

		var serverInputNode = new DosNode();
		var homeConnections = new DosNode();
		var server1_1Connections = new DosNode();
		var server2_1Connections = new DosNode();
		var server3_1Connections = new DosNode();
		var server4Connections = new DosNode();

		program.startNode = serverInputNode;

		serverInputNode.startTextLines.Add("Input start server address:");

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

		if(programEnumerator != null)
			StopCoroutine(programEnumerator);

		programEnumerator = ProcessProgramCoroutine(program);
		StartCoroutine(programEnumerator);
	}

	#endregion

	#region logic

	#endregion

	#region public interface

	#endregion

	#region private interface

	IEnumerator ProcessProgramCoroutine(DosProgram program)
	{
		var node = program.startNode;
		while(node != null)
		{
			inputField.readOnly = true;
			yield return new WaitForSeconds(0.9f);

			for(int i = 0; i < node.startTextLines.Count; i++)
			{
				inputField.text += "\n"+ node.startTextLines[i];
				yield return new WaitForSeconds(0.4f);
			}

			if(node.showChoices)
			{
				foreach(var choice in node.choices)
				{
					if(choice.Value == null) continue;
					inputField.text += string.Format("\n   {0} - {1}", choice.Key, choice.Value);
					yield return new WaitForSeconds(0.4f);
				}
			}

			while(true)
			{
				inputField.text += "\n";
				int inputTextLineCount = GetTextLineCount();

				inputField.readOnly = false;

				while(inputTextLineCount == GetTextLineCount())
					yield return null;

				inputField.readOnly = true;

				yield return new WaitForSeconds(0.3f);

				string input = inputField.text.Substring(inputField.text.LastIndexOf('\n'));
				DosNode nextNode;
				if(node.choices.TryGetValue(input, out nextNode))
				{
					node = nextNode;
					break;
				} else
				{
					inputField.text += "\nBad command";
				}
			}
		}

		inputField.text += "\nProgram terminated";
		programEnumerator = null;
	}

	int GetTextLineCount()
	{
		return inputField.text.Split('\n').Length;
	}

	#endregion

	#region events

	#endregion
}
