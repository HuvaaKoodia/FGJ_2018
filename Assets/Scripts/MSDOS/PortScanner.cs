using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortScanner : MonoBehaviour 
{
#region variables
	public const string homeAddress = "1.2.3.4";
	public const string server1_1Address = "65.201.411.771";
	public const string server2_1Address = "edu.Kajak.fi";
	public const string server3_1Address = "corp.eztech.biz";
	public const string server4Address = "919.939.989.929";

#endregion
#region initialization
	private void Awake ()
	{
		var startNode = new DosNode("");
		var homeConnections = new DosNode("");
		var server1_1Connections = new DosNode(server1_1Address);
		var server2_1Connections = new DosNode(server2_1Address);
		var server3_1Connections = new DosNode(server3_1Address);
		var server4Connections = new DosNode(server4Address);

		startNode.programStartCommand = "PrtScn -f -u";
		startNode.startTextLines.Add("Local server address:\n"+homeAddress+"\n");

		startNode.startTextLines.Add("Input server address to inspect:");

		startNode.showChoices = false;
		startNode.choices.Add(homeAddress, homeConnections);
		startNode.choices.Add(server1_1Address, server1_1Connections);
		startNode.choices.Add(server2_1Address, server2_1Connections);
		startNode.choices.Add(server3_1Address, server3_1Connections);

		var sharedTextLines = new List<string>();
		sharedTextLines.Add("This server has forward connections.");
		sharedTextLines.Add("Input a connection index to inspect it");

		homeConnections.choices.Add("1", server1_1Connections);
		homeConnections.choices.Add("2", server4Connections);

		homeConnections.startTextLines = sharedTextLines;

		server1_1Connections.choices.Add("1", server2_1Connections);
		server1_1Connections.choices.Add("2", server4Connections);

		server1_1Connections.startTextLines = sharedTextLines;

		server2_1Connections.choices.Add("1", server3_1Connections);
		server2_1Connections.choices.Add("2", server4Connections);

		server2_1Connections.startTextLines = sharedTextLines;

		server3_1Connections.choices.Add("1", server4Connections);

		server4Connections.startTextLines.Add("This server has no forward connections.");
		server4Connections.startTextLines.Add("Input q to quit.");

		server4Connections.choices.Add("q", null);

		GetComponent<MSDOSPrompt>().startNode = startNode;
	}
#endregion
#region logic
#endregion
#region public interface
#endregion
#region private interface
#endregion
#region events
#endregion
}
