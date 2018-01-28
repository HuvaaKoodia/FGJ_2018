using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopup : MonoBehaviour {

	public Text warningText;
	AudioManager sound;

	// Use this for initialization
	void Awake () {
		if(sound == null) {
			sound = GameObject.Find("Audio").GetComponent<AudioManager>();	
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Open(string warning) {
		GetComponent<Window>().Open();
		sound.error.Play();
		warningText.text = warning;
	}
}
