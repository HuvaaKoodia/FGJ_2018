using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortScanner : MonoBehaviour 
{
#region variables
#endregion
#region initialization
	private void Awake ()
	{
		var startNode = new DosNode("");
		var homeConnections = new DosNode("");
		var server1_1Connections = new DosNode(WorldState.server1_1Address);
		server1_1Connections.customChoiceName += server1ChoiceName;

		var server2_1Connections = new DosNode(WorldState.server2_1Address);
		server2_1Connections.customChoiceName += server2ChoiceName;

		var server3_1Connections = new DosNode(WorldState.server3_1Address);
		server3_1Connections.customChoiceName += server3ChoiceName;

		var server4Connections = new DosNode(WorldState.server4Address);
		server4Connections.customChoiceName += server4ChoiceName;

		startNode.programStartCommand = "PrtScn -f -u";
		startNode.startTextLines.Add("Local server address:\n"+WorldState.homeAddress+"\n");

		startNode.startTextLines.Add("Input server address to inspect:");

		startNode.showChoices = false;
		startNode.choices.Add(WorldState.homeAddress, homeConnections);
		startNode.choices.Add(WorldState.server1_1Address, server1_1Connections);
		startNode.choices.Add(WorldState.server2_1Address, server2_1Connections);
		startNode.choices.Add(WorldState.server3_1Address, server3_1Connections);

		var sharedTextLines = new List<string>();
		sharedTextLines.Add("This server has forward connections.");
		sharedTextLines.Add("Input a connection index to inspect it");

		homeConnections.startTextLines = sharedTextLines;
		homeConnections.choices.Add("1", server1_1Connections);
		homeConnections.choices.Add("2", server4Connections);

		server1_1Connections.startTextLines = sharedTextLines;
		server1_1Connections.choices.Add("1", server2_1Connections);
		server1_1Connections.choices.Add("2", server4Connections);

		server2_1Connections.startTextLines = sharedTextLines;
		server2_1Connections.choices.Add("1", server3_1Connections);
		server2_1Connections.choices.Add("2", server4Connections);

		server3_1Connections.startTextLines = sharedTextLines;
		server3_1Connections.choices.Add("1", server4Connections);

		server4Connections.startTextLines.Add("This server has no forward connections.");
		server4Connections.startTextLines.Add("Input r to restart.");
		server4Connections.showChoices = false;
		server4Connections.choices.Add("r", startNode);

		GetComponent<MSDOSPrompt>().startNode = startNode;
	}

	string server1ChoiceName()
	{
		return WorldState.server1_1Address + " " + (WorldState.ServerOpen1 ? "[Upload port open]" : "[Upload port blocked]");
	}

	string server2ChoiceName()
	{
		return WorldState.server2_1Address + " " + (WorldState.ServerOpen2 ? "[Upload port open]" : "[Upload port blocked]");
	}

	string server3ChoiceName()
	{
		return WorldState.server3_1Address + " " + (WorldState.ServerOpen3 ? "[Upload port open]" : "[Upload port blocked]");
	}

	string server4ChoiceName()
	{
		return WorldState.server4Address + " " + (true ? "[Upload port open]" : "[Upload port blocked]");
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
