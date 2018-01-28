using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ServerAccess : MonoBehaviour 
{
#region variables
	DosNode server1StartNode, server2StartNode, server3StartNode, server4StartNode,
	adminNodeServer1, adminNodeServer2, adminNodeServer3,
	adminPasswordNode,
	badCredentialsNode;
	string name;

	public UnityEvent onServer1EmailDownloaded, onServer3LogsDownloaded;
#endregion
#region initialization
	private void Awake () 
	{
		var startNode = new DosNode("");
		startNode.programStartCommand = "Sacs -noW";
		GetComponent<MSDOSPrompt>().startNode = startNode;

		//shared admin nodes
		adminPasswordNode = new DosNode("");
		adminPasswordNode.startTextLines.Add("Password:");
		adminPasswordNode.customChoiceInputAction += OnPasswordInput;
		
		badCredentialsNode = new DosNode("");
		badCredentialsNode.startTextLines.Add("Access denied");
		badCredentialsNode.customChoiceInputAction += GoBackToCurrentServerStart;

		var disconnectNode = new DosNode("Disconnect");
		disconnectNode.startTextLines.Add("Disconnected from server.");
		disconnectNode.nextNode = startNode;

		string exceptionOption = "Add firewall exception";
		string exceptionQuestionText = "From which address:";
		string exceptionText = "Exception added";
		string noSelfAddText = "Cannot add self as an exception";

		//server connections
		var connectionTextLines = new List<string>();
		connectionTextLines.Add("Connecting to server");
		connectionTextLines.Add(".");
		connectionTextLines.Add("..");
		connectionTextLines.Add("...");
		connectionTextLines.Add("Connection established");

		server1StartNode = new DosNode("Menu");
		server2StartNode = new DosNode("Menu");
		server3StartNode = new DosNode("Back");
		server4StartNode = new DosNode("Menu");
		
		var connectToServer1 = new DosNode("");
		connectToServer1.startTextLines = connectionTextLines;
		connectToServer1.nextNode = server1StartNode;

		var connectToServer2 = new DosNode("");
		connectToServer2.startTextLines = connectionTextLines;
		connectToServer2.nextNode = server2StartNode;

		var connectToServer3 = new DosNode("");
		connectToServer3.startTextLines = connectionTextLines;
		connectToServer3.nextNode = server3StartNode;

		var connectToServer4 = new DosNode("");
		connectToServer4.startTextLines = connectionTextLines;
		connectToServer4.nextNode = server4StartNode;

		startNode.startTextLines.Add("Input server address to connect to:");
		startNode.showChoices = false;
		startNode.choices.Add(WorldState.server1_1Address, connectToServer1);
		startNode.choices.Add(WorldState.server2_1Address, connectToServer2);
		startNode.choices.Add(WorldState.server3_1Address, connectToServer3);
		startNode.choices.Add(WorldState.server4Address, connectToServer4);

		{//server 1
			adminNodeServer1 = new DosNode("");
			var adminNameNode = new DosNode("Admin options");
			var emailNode = new DosNode("Email options");
			var downloadEmails = new DosNode("Download emails");
			var removeEmails = new DosNode("Remove emails");
			var addPortForwardException = new DosNode(exceptionOption);
			var addedCorrectAddress = new DosNode("");
			var addedSelfAddress = new DosNode("");
			var addedOtherAddress = new DosNode("");

			server1StartNode.startTextLines.Add("Welcome to Brady's Petshop server :-)");

			server1StartNode.choices.Add("a", adminNameNode);
			server1StartNode.choices.Add("e", emailNode);
			server1StartNode.choices.Add("d", disconnectNode);

			adminNameNode.startTextLines.Add("Name:");
			adminNameNode.onStartEvent += SetCurrentServerAsServer1;
			adminNameNode.customChoiceInputAction += OnNameInput;

			adminNodeServer1.startTextLines.Add("Admin options:");
			adminNodeServer1.choices.Add("e", addPortForwardException);
			adminNodeServer1.choices.Add("m", server1StartNode);

			addPortForwardException.startTextLines.Add(exceptionQuestionText);
			addPortForwardException.showChoices = false;
			addPortForwardException.choices.Add(WorldState.homeAddress, addedCorrectAddress);
			addPortForwardException.choices.Add(WorldState.server1_1Address, addedSelfAddress);
			addPortForwardException.choices.Add(WorldState.server2_1Address, addedOtherAddress);
			addPortForwardException.choices.Add(WorldState.server3_1Address, addedOtherAddress);
			addPortForwardException.choices.Add(WorldState.server4Address, addedOtherAddress);

			addedCorrectAddress.startTextLines.Add(exceptionText);
			addedCorrectAddress.nextNode = adminNodeServer1;
			addedCorrectAddress.onStartEvent += OpenServer1;

			addedOtherAddress.startTextLines.Add(exceptionText);
			addedOtherAddress.nextNode = adminNodeServer1;

			addedSelfAddress.startTextLines.Add(noSelfAddText);
			addedSelfAddress.nextNode = adminNodeServer1;

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
		}

		{//server 2
			adminNodeServer2 = new DosNode("");
			var adminNameNode = new DosNode("Admin options");
			var addPortForwardException = new DosNode(exceptionOption);
			var addedCorrectAddress = new DosNode("");
			var addedSelfAddress = new DosNode("");
			var addedOtherAddress = new DosNode("");

			adminNameNode.startTextLines.Add("Name:");
			adminNameNode.onStartEvent += SetCurrentServerAsServer2;
			adminNameNode.customChoiceInputAction += OnNameInput;

			adminNodeServer2.startTextLines.Add("Admin options:");
			adminNodeServer2.choices.Add("e", addPortForwardException);
			adminNodeServer2.choices.Add("m", server2StartNode);

			addPortForwardException.startTextLines.Add(exceptionQuestionText);
			addPortForwardException.showChoices = false;
			addPortForwardException.choices.Add(WorldState.homeAddress, addedOtherAddress);
			addPortForwardException.choices.Add(WorldState.server1_1Address, addedCorrectAddress);
			addPortForwardException.choices.Add(WorldState.server2_1Address, addedSelfAddress);
			addPortForwardException.choices.Add(WorldState.server3_1Address, addedOtherAddress);
			addPortForwardException.choices.Add(WorldState.server4Address, addedOtherAddress);

			addedCorrectAddress.startTextLines.Add(exceptionText);
			addedCorrectAddress.nextNode = adminNodeServer2;
			addedCorrectAddress.onStartEvent += OpenServer2;

			addedOtherAddress.startTextLines.Add(exceptionText);
			addedOtherAddress.nextNode = adminNodeServer2;
			
			addedSelfAddress.startTextLines.Add(noSelfAddText);
			addedSelfAddress.nextNode = adminNodeServer2;

			//custom nodes
			var databaseAccess = new DosNode("Database addresses");

			server2StartNode.startTextLines.Add("Wideforest institute of rehabilitation.");

			server2StartNode.choices.Add("1", adminNameNode);
			server2StartNode.choices.Add("2", databaseAccess);
			server2StartNode.choices.Add("3", disconnectNode);

			databaseAccess.startTextLines.Add("Database addresses:");
			databaseAccess.startTextLines.Add("Users - "+WorldState.server2_userDatabaseAddress);
			databaseAccess.choices.Add("1", server2StartNode);
		}

		{//server 3
			var adminNameNode = new DosNode("Admin options");
			var addPortForwardException = new DosNode(exceptionOption);
			var addedCorrectAddress = new DosNode("");
			var addedSelfAddress = new DosNode("");
			var addedOtherAddress = new DosNode("");

			adminNameNode.startTextLines.Add("Name:");
			adminNameNode.onStartEvent += SetCurrentServerAsServer3;
			adminNameNode.customChoiceInputAction += OnNameInput;

			adminNodeServer3 = new DosNode("");
			adminNodeServer3.startTextLines.Add("Admin options:");
			adminNodeServer3.choices.Add("e", addPortForwardException);
			adminNodeServer3.choices.Add("m", server3StartNode);

			addPortForwardException.startTextLines.Add(exceptionQuestionText);
			addPortForwardException.showChoices = false;
			addPortForwardException.choices.Add(WorldState.homeAddress, addedOtherAddress);
			addPortForwardException.choices.Add(WorldState.server1_1Address, addedOtherAddress);
			addPortForwardException.choices.Add(WorldState.server2_1Address, addedCorrectAddress);
			addPortForwardException.choices.Add(WorldState.server3_1Address, addedSelfAddress);
			addPortForwardException.choices.Add(WorldState.server4Address, addedOtherAddress);

			addedCorrectAddress.startTextLines.Add(exceptionText);
			addedCorrectAddress.nextNode = adminNodeServer3;
			addedCorrectAddress.onStartEvent += OpenServer3;

			addedOtherAddress.startTextLines.Add(exceptionText);
			addedOtherAddress.nextNode = adminNodeServer3;

			addedSelfAddress.startTextLines.Add(noSelfAddText);
			addedSelfAddress.nextNode = adminNodeServer3;

			//custom nodes
			var logsAccess = new DosNode("Database addresses");
			var downloadLogs = new DosNode("Download");

			server3StartNode.startTextLines.Add("Ez-Tech Mainframe - Unauthorized access forbidden");

			server3StartNode.choices.Add("adm", adminNameNode);
			server3StartNode.choices.Add("log", logsAccess);
			server3StartNode.choices.Add("dis", disconnectNode);

			logsAccess.startTextLines.Add("Logs:");
			logsAccess.choices.Add("dwn", downloadLogs);
			logsAccess.choices.Add("bck", server3StartNode);

			downloadLogs.startTextLines.Add("Success!");
			downloadLogs.nextNode = server3StartNode;
			downloadLogs.onStartEvent += DownloadLogs;
		}

		{//server 4
			server4StartNode.startTextLines.Add("Nameless proxy. Nothing to see here.");
			server4StartNode.choices.Add("x", disconnectNode);

		}
	}
#endregion
#region logic
#endregion
#region public interface
#endregion
#region private interface
#endregion
#region events

	DosNode currentServerStartNode;

	void SetCurrentServerAsServer1()
	{
		currentServerStartNode = server1StartNode;
	}

	void SetCurrentServerAsServer2()
	{
		currentServerStartNode = server2StartNode;
	}

	void SetCurrentServerAsServer3()
	{
		currentServerStartNode = server3StartNode;
	}

	DosNode OnNameInput(string input)
	{
		name = input;
		return adminPasswordNode;
	}

	DosNode OnPasswordInput(string input)
	{//HACK!
		if(currentServerStartNode == server1StartNode && name == "admin" && input == "password")
			return adminNodeServer1;
		if(currentServerStartNode == server2StartNode && name == "Sweetman" && input == "NoWorries123")
			return adminNodeServer2;
		if(currentServerStartNode == server3StartNode && name == "MasterMack" && input == "ThumbsDown")
			return adminNodeServer3;
		return badCredentialsNode;
	}

	DosNode GoBackToCurrentServerStart(string input)
	{
		return currentServerStartNode;
	}

	bool OnEmailChoiceAvailable(string input)
	{
		if(input == "d" || input == "r")
			return WorldState.Server1HasEmails;
		return true;
	}

	void OpenServer1()
	{
		WorldState.ServerOpen1 = true;
	}

	void OpenServer2()
	{
		WorldState.ServerOpen2 = true;
	}

	void OpenServer3()
	{
		WorldState.ServerOpen3 = true;
	}

	void RemoveEmails()
	{
		WorldState.Server1HasEmails = false;
	}

	void DownloadEmails()
	{
		onServer1EmailDownloaded.Invoke();
	}

	void DownloadLogs()
	{
		onServer3LogsDownloaded.Invoke();
	}
#endregion
}
