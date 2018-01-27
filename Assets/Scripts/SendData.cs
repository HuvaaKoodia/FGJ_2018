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
	public Window transmissionWindow;

	AudioManager sound;
	float timer;
	int serverIndex;
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
			// Show network window shit
			timer += CONNECTION_TIME;
			return;
		}
		if(WorldState.ServerOpen2 && serverIndex == 2 && server2.IsInteractable()) {
			// Show network window shit
			timer += CONNECTION_TIME;
			return;
		}
		if(WorldState.ServerOpen3 && serverIndex == 3 && server3.IsInteractable()) {
			// Show network window shit
			timer += CONNECTION_TIME;
			return;
		}
		if(serverIndex == 4 && server3.IsInteractable()) {
			// Show network window shit
			timer += CONNECTION_TIME;
			return;
		}

		// TODO: Check if valid pipe connection

		if(true) {
			sound.dial.Stop();
			transmissionWindow.Open();
			print("Send data: " + dataInput.text);
		}

	}

	public void Send() {

		// TODO: Check valid pipe input

		sound.dial.Play();
		networkWindow.Open();
		serverIndex = 0;
		timer = CONNECTION_TIME; // Random?
	
	}
}
