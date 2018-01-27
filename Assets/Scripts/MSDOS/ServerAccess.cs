using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerAccess : MonoBehaviour 
{
#region variables
	DosNode server1StartNode, adminPasswordNode, adminNode, badCredentialsNode, currentServerNode;
	string name;
#endregion
#region initialization
	private void Awake () 
	{
		var startNode = new DosNode("");

		var connectToServer1 = new DosNode("");

		var server1StartNode = new DosNode("Menu");
		var adminNameNode = new DosNode("Admin options");
		adminPasswordNode = new DosNode("");
		adminNode = new DosNode("");
		var emailNode = new DosNode("Email options");
		badCredentialsNode = new DosNode("");
		var disconnectNode = new DosNode("Disconnect");
		var downloadEmails = new DosNode("Download emails");
		var removeEmails = new DosNode("Remove emails");

		startNode.programStartCommand = "Sacs -noW";

		startNode.startTextLines.Add("Input server address to connect to:");
		startNode.showChoices = false;
		startNode.choices.Add(PortScanner.server1_1Address, connectToServer1);

		connectToServer1.startTextLines.Add("Connecting to server");
		connectToServer1.startTextLines.Add(".");
		connectToServer1.startTextLines.Add("..");
		connectToServer1.startTextLines.Add("...");
		connectToServer1.startTextLines.Add("Connection established");
		connectToServer1.nextNode = server1StartNode;

		server1StartNode.startTextLines.Add("Welcome to X-server.");

		server1StartNode.choices.Add("1", adminNameNode);
		server1StartNode.choices.Add("2", emailNode);
		server1StartNode.choices.Add("3", disconnectNode);

		adminNameNode.startTextLines.Add("Name:");
		adminNameNode.onStartEvent += SetCurrentServerToServer1;
		adminNameNode.customChoiceInputAction += OnNameInput;

		adminPasswordNode.startTextLines.Add("Password:");
		adminNameNode.customChoiceInputAction += OnPasswordInput;

		badCredentialsNode.startTextLines.Add("Access denied.");
		badCredentialsNode.customChoiceInputAction += GoBackToServerStart;

		disconnectNode.startTextLines.Add("Disconnect");

		emailNode.choices.Add("1", downloadEmails);
		emailNode.choices.Add("2", removeEmails);
		emailNode.choices.Add("3", server1StartNode);

		downloadEmails.startTextLines.Add("Emails downloaded!");
		downloadEmails.nextNode = server1StartNode;

		removeEmails.startTextLines.Add("Emails removed!");
		removeEmails.nextNode = server1StartNode;

		GetComponent<MSDOSPrompt>().startNode = startNode;

		disconnectNode.startTextLines.Add("Disconnected from server.");
		disconnectNode.nextNode = startNode;
	}
#endregion
#region logic
#endregion
#region public interface
#endregion
#region private interface
#endregion
#region events
	void SetCurrentServerToServer1()
	{
		currentServerNode = server1StartNode;
	}

	DosNode OnNameInput(string input)
	{
		name = input;
		return adminPasswordNode;
	}

	DosNode OnPasswordInput(string input)
	{
		if(name == "admin" && input == "password")
			return adminNode;
		return badCredentialsNode;
	}

	DosNode GoBackToServerStart(string input)
	{
		return currentServerNode;
	}
#endregion
}
