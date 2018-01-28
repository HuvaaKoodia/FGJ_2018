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

	AudioManager sound;
	float timer;
	int serverIndex;
	bool validConnection;

	static readonly float CONNECTION_TIME = 10f;

	// Use this for initialization
	void Start () {
		sound = GameObject.Find("Audio").GetComponent<AudioManager>();
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
		if(WorldState.ServerOpen1 && serverIndex == 1 && server1.IsInteractable()) {
			networkText.text += "\nConnecting to " + server1.text;
			timer += CONNECTION_TIME;
			return;
		} else if(!WorldState.ServerOpen1 && serverIndex == 1 && server1.IsInteractable()) {
			validConnection = false;
			timer += 1f;
		} else if (serverIndex == 1) {
			networkText.text += "\nLink 1 not defined";
			timer += 1f;
		}
		if(WorldState.ServerOpen2 && serverIndex == 2 && server2.IsInteractable()) {
			networkText.text += "\nConnecting to " + server2.text;
			timer += CONNECTION_TIME;
			return;
		} else if(!WorldState.ServerOpen2 && serverIndex == 2 && server2.IsInteractable()) {
			validConnection = false;
			timer += 1f;
		} else if (serverIndex == 2){
			networkText.text += "\nLink 2 not defined";
			timer += 1f;
		}
		if(WorldState.ServerOpen3 && serverIndex == 3 && server3.IsInteractable()) {
			networkText.text += "\nConnecting to " + server3.text;
			timer += CONNECTION_TIME;
			return;
		} else if(!WorldState.ServerOpen3 && serverIndex == 3 && server3.IsInteractable()) {
			validConnection = false;
			timer += 1f;
		} else if (serverIndex == 3){
			networkText.text += "\nLink 3 not defined";
			timer += 1f;
		}
		if(serverIndex == 4 && server4.IsInteractable()) {
			networkText.text += "\nConnecting to " + server4.text;
			timer += CONNECTION_TIME;
			return;
		} else if(serverIndex == 4 && !server1.IsInteractable()) {
			validConnection = false;
			timer += 1f;
		} else if (serverIndex == 4){
			networkText.text += "\nTarget Link not defined";
			timer += 1f;
		}

		if(validConnection && serverIndex == 5) {
			sound.dial.Stop();
			transmissionWindow.Open();
			print("Send data: " + dataInput.text);
		} else if(serverIndex == 5) {
			//TODO: error popup
			print("Invalid pipe");
			timer = 0;
			networkWindow.Close();
		}

	}

	public void Send() {

		// End server must be defined
		if(!server4.IsInteractable()) {
			//TODO: error popup
			print("Invalid pipe");
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
