using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ServerAccess : MonoBehaviour 
{
#region variables
	DosNode server1StartNode, adminPasswordNode, adminNode, badCredentialsNode;
	string name;

	public UnityEvent onServer1EmailDownloaded;
#endregion
#region initialization
	private void Awake () 
	{
		var startNode = new DosNode("");

		var connectToServer1 = new DosNode("");

		server1StartNode = new DosNode("Menu");
		var adminNameNode = new DosNode("Admin options");
		adminPasswordNode = new DosNode("");
		adminNode = new DosNode("");
		var emailNode = new DosNode("Email options");
		badCredentialsNode = new DosNode("");
		var disconnectNode = new DosNode("Disconnect");
		var downloadEmails = new DosNode("Download emails");
		var removeEmails = new DosNode("Remove emails");
		var addPortForwardException = new DosNode("Add port forwarding exception");
		var addedHomeAddress = new DosNode("");
		var addedServerAddress = new DosNode("");
		var addedOtherAddress = new DosNode("");

		startNode.programStartCommand = "Sacs -noW";

		startNode.startTextLines.Add("Input server address to connect to:");
		startNode.showChoices = false;
		startNode.choices.Add(WorldState.server1_1Address, connectToServer1);

		connectToServer1.startTextLines.Add("Connecting to server");
		connectToServer1.startTextLines.Add(".");
		connectToServer1.startTextLines.Add("..");
		connectToServer1.startTextLines.Add("...");
		connectToServer1.startTextLines.Add("Connection established");
		connectToServer1.nextNode = server1StartNode;

		server1StartNode.startTextLines.Add("Welcome to X-server.");

		server1StartNode.choices.Add("a", adminNameNode);
		server1StartNode.choices.Add("e", emailNode);
		server1StartNode.choices.Add("d", disconnectNode);

		adminNameNode.startTextLines.Add("Name:");
		adminNameNode.customChoiceInputAction += OnNameInput;

		adminPasswordNode.startTextLines.Add("Password:");
		adminPasswordNode.customChoiceInputAction += OnPasswordInput;

		adminNode.startTextLines.Add("Admin options:");
		adminNode.choices.Add("e", addPortForwardException);
		adminNode.choices.Add("m", server1StartNode);

		addPortForwardException.startTextLines.Add("Input exception address:");
		addPortForwardException.showChoices = false;
		addPortForwardException.choices.Add(WorldState.homeAddress, addedHomeAddress);
		addPortForwardException.choices.Add(WorldState.server1_1Address, addedServerAddress);
		addPortForwardException.choices.Add(WorldState.server2_1Address, addedOtherAddress);
		addPortForwardException.choices.Add(WorldState.server3_1Address, addedOtherAddress);
		addPortForwardException.choices.Add(WorldState.server4Address, addedOtherAddress);

		addedHomeAddress.startTextLines.Add("Exception added");
		addedHomeAddress.nextNode = adminNode;

		addedOtherAddress.startTextLines.Add("Exception added");
		addedOtherAddress.nextNode = adminNode;

		addedServerAddress.startTextLines.Add("Cannot add local address to exceptions");
		addedServerAddress.nextNode = adminNode;

		badCredentialsNode.startTextLines.Add("Access denied");
		badCredentialsNode.customChoiceInputAction += GoBackToServer1Start;

		disconnectNode.startTextLines.Add("Disconnect");

		emailNode.onChoiceAvailable += OnEmailChoiceAvailable;
		emailNode.choices.Add("d", downloadEmails);
		emailNode.choices.Add("r", removeEmails);
		emailNode.choices.Add("m", server1StartNode);

		downloadEmails.startTextLines.Add("Emails downloaded");
		downloadEmails.onStartEvent += DownloadEmails;
		downloadEmails.nextNode = emailNode;

		removeEmails.startTextLines.Add("Emails removed");
		removeEmails.onStartEvent += RemoveEmails;
		removeEmails.nextNode = emailNode;

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

	DosNode GoBackToServer1Start(string input)
	{
		return server1StartNode;
	}

	bool OnEmailChoiceAvailable(string input)
	{
		if(input == "d" || input == "r")
			return WorldState.Server1HasEmails;
		return true;
	}

	void RemoveEmails()
	{
		WorldState.Server1HasEmails = false;
	}

	void DownloadEmails()
	{
		onServer1EmailDownloaded.Invoke();
	}
#endregion
}
