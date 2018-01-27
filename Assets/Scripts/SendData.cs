using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendData : MonoBehaviour {

	public InputField dataInput;

	public InputField server1;
	public InputField server2;
	public InputField server3;

	public Window networkWindow;
	public Window transmissionWindow;

	AudioManager sound;

	// Use this for initialization
	void Start () {
		sound = GameObject.Find("Audio").GetComponent<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Send() {
		sound.dial.Play();
		networkWindow.Open();
		print("Send data: " + dataInput.text);
	}
}
