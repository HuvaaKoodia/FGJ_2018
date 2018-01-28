using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendData : MonoBehaviour {

	public InputField dataInput;

	public InputField server1;
	public InputField server2;
	public InputField server3;
	public InputField server4;

	public Window networkWindow;
	public Text networkText;
	public Window transmissionWindow;
	public WarningPopup warning;

	AudioManager sound;
	float timer;
	int serverIndex;
	bool validConnection;

	static readonly float CONNECTION_TIME = 10f;

	// Use this for initialization
	void Awake () {
		if(sound == null) {
			sound = GameObject.Find("Audio").GetComponent<AudioManager>();			
		}
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if(timer > 0) {
			timer -= Time.deltaTime;
			if(timer < 0) {
				serverIndex++;
				Connect();		
			}
		}
	}

	void Connect() {

		// Debug
		WorldState.ServerOpen1 = true;
		WorldState.ServerOpen2 = true;
		WorldState.ServerOpen3 = true;

		var error = "Connection failed.";

		if(serverIndex == 1 && server1.IsInteractable()) {
			networkText.text += "\nConnecting to " + server1.text;
			if(!WorldState.ServerOpen1) {
				validConnection = false;
				timer = 0;
				error = "Access denied.";
				goto Check;
			}
			if(server1.text.Trim() != WorldState.server1_1Address) {
				validConnection = false;
				timer = 0;
				error = "Address not found.";
				goto Check;
			}
			timer += CONNECTION_TIME;
			return;
		} else if(serverIndex == 1) {
			networkText.text += "\nLink 1 not defined";
			timer += 1f;
		}
		if(serverIndex == 2 && server2.IsInteractable()) {
			networkText.text += "\nConnecting to " + server2.text;
			if(!WorldState.ServerOpen2) {
				validConnection = false;
				timer = 0;
				error = "Access denied.";
				goto Check;
			}
			if(server2.text.Trim() != WorldState.server2_1Address) {
				validConnection = false;
				timer = 0;
				error = "Address not found.";
				goto Check;
			}
			timer += CONNECTION_TIME;
			return;
		} else if(serverIndex == 2) {
			networkText.text += "\nLink 2 not defined";
			timer += 1f;
		}

		if(serverIndex == 3 && server3.IsInteractable()) {
			networkText.text += "\nConnecting to " + server3.text;
			if(!WorldState.ServerOpen3) {
				validConnection = false;
				timer = 0;
				error = "Access denied.";
				goto Check;
			}
			if(server3.text.Trim() != WorldState.server3_1Address) {
				validConnection = false;
				timer = 0;
				error = "Address not found.";
				goto Check;
			}
			timer += CONNECTION_TIME;
			return;
		} else if(serverIndex == 3) {
			networkText.text += "\nLink 3 not defined";
			timer += 1f;
		}
		// End
		if(serverIndex == 4 && server4.text.Trim() == WorldState.server4Address) {
			networkText.text += "\nConnecting to " + server4.text;
			timer += CONNECTION_TIME;
			return;
		} else if(serverIndex == 4 && server4.text.Trim() != WorldState.server4Address) {
			validConnection = false;
			error = "Address not found.";
			timer = 0;
		} else if(serverIndex == 4) {
			Debug.LogWarning("Should not happen!");
			timer += 1f;
		}

		Check:

		if(validConnection && serverIndex == 5) {
			sound.dial.Stop();
			transmissionWindow.Open();
			print("Send data: " + dataInput.text);
		} else if(!validConnection) {
			timer = 0;
			networkWindow.Close();
			warning.Open("Invalid pipe.\n" + error);
		}

	}

	public void Send() {

		// End server must be defined
		if(server4.text.Trim() == "") {
			warning.Open("Invalid pipe.\nTarget not defined.");
			networkWindow.Close();
		} else {
			sound.dial.Play();
			networkWindow.Open();
			serverIndex = 0;
			validConnection = true;
			timer = CONNECTION_TIME; // Random?		
		}
	}

	public void ResetNetworkText() {
		networkText.text = "Connecting ...";
	}

}
