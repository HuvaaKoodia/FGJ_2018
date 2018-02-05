using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{

	public bool inputDataEncrypted, inputDataUseful;
	public int extraConnectionCount = 0;

	public InputField dataInput;

	public InputField server1;
	public InputField server2;
	public InputField server3;
	public InputField server4;

	public Window networkWindow;
	public Text networkText;
	public Window transmissionWindow;
	public WarningPopup warning;

	float timer;
	int serverIndex;
	bool validConnection;

	static readonly float CONNECTION_TIME = 6.5f;

	// Use this for initialization
	void Awake()
	{
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		if(timer > 0)
		{
			timer -= Time.deltaTime;
			if(timer < 0)
			{
				serverIndex++;
				Connect();		
			}
		}
	}

	bool server1Success = false, server2Success = false, server3Success = false;

	void Connect()
	{

		inputDataEncrypted = false;

		var error = "Connection failed.";

		if(serverIndex == 1 && server1.IsInteractable())
		{
			networkText.text += "\nConnecting to:\n" + server1.text;
			if(server1.text.Trim() != WorldState.serverAddress1)
			{
				validConnection = false;
				timer = 0;
				error = "Address not found.\n"+server1.text;
				goto Check;
			}
			if(!WorldState.serverOpen1)
			{
				validConnection = false;
				timer = 0;
				error = "Access denied.\n"+WorldState.serverAddress1;
				goto Check;
			}

			timer += CONNECTION_TIME;
			server1Success = true;
			return;
		} else if(serverIndex == 1)
		{
			networkText.text += "\nLink 1 not defined";
			timer += 1f;
		}


		if(serverIndex == 2 && server2.IsInteractable())
		{
			networkText.text += "\nConnecting to:\n" + server2.text;
			if(server2.text.Trim() != WorldState.serverAddress2)
			{
				validConnection = false;
				timer = 0;
				error = "Address not found.\n"+server2.text;
				goto Check;
			}
			if(!WorldState.serverOpen2)
			{
				validConnection = false;
				timer = 0;
				error = "Access denied.\n"+WorldState.serverAddress2;
				goto Check;
			}
			timer += CONNECTION_TIME;
			server2Success = true;
			return;
		} else if(serverIndex == 2)
		{
			networkText.text += "\nLink 2 not defined";
			timer += 1f;
		}

		if(serverIndex == 3 && server3.IsInteractable())
		{
			networkText.text += "\nConnecting to:\n" + server3.text;
			if(server3.text.Trim() != WorldState.serverAddress3)
			{
				validConnection = false;
				timer = 0;
				error = "Address not found.\n"+server3.text;
				goto Check;
			}
			if(!WorldState.serverOpen3)
			{
				validConnection = false;
				timer = 0;
				error = "Access denied.\n"+WorldState.serverAddress3;
				goto Check;
			}
			timer += CONNECTION_TIME;
			server3Success = true;
			return;
		} else if(serverIndex == 3)
		{
			networkText.text += "\nLink 3 not defined";
			timer += 1f;
		}
		// End
		if(serverIndex == 4 && server4.text.Trim() == WorldState.serverAddress4)
		{
			networkText.text += "\nConnecting to:\n" + server4.text;
			timer += CONNECTION_TIME;
			return;
		} else if(serverIndex == 4 && server4.text.Trim() != WorldState.serverAddress4)
		{
			validConnection = false;
			error = "Address not found.";
			timer = 0;
		} else if(serverIndex == 4)
		{
			Debug.LogWarning("Should not happen!");
			timer += 1f;
		}

		Check:

		if(validConnection && serverIndex == 5)
		{


			extraConnectionCount = 0;
			if(server1Success) extraConnectionCount += 1;
			if(server2Success) extraConnectionCount += 1;
			if(server3Success) extraConnectionCount += 1;

			inputDataEncrypted = false;
			inputDataUseful = false;

			if(dataInput.text.Trim() == "crocodile is down sending more zebras beware of the lion")
			{
				inputDataEncrypted = false;
				inputDataUseful = true;
			} else if(dataInput.text.Trim() == "05c70862939a523fd67b632a0d64f238")
			{
				inputDataEncrypted = true;
				inputDataUseful = true;
			}

			AudioManager.I.dial.Stop();
			transmissionWindow.Open();
			print("Send data: " + dataInput.text);




		} else if(!validConnection)
		{
			timer = 0;
			networkWindow.Close();
			AudioManager.I.dial.Stop();
			warning.Open("Invalid pipe.\n" + error);
		}

	}

	public void Send()
	{

		server1Success = false;
		server2Success = false;
		server3Success = false;

		// End server must be defined
		if(server4.text.Trim() == "")
		{
			AudioManager.I.dial.Stop();
			warning.Open("Invalid pipe.\nTarget not defined.");
			networkWindow.Close();
		} else
		{
			AudioManager.I.dial.Play();
			networkWindow.Open();
			serverIndex = 0;
			validConnection = true;
			timer = CONNECTION_TIME; // Random?		
		}
	}

	public void ResetNetworkText()
	{
		networkText.text = "Connecting ...";
	}

}
