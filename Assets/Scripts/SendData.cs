using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendData : MonoBehaviour {

	public InputField dataInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Send() {
		print("Send data: " + dataInput.text);
	}
}
