using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour {

	public InputField toggleField;

	Button button;

	// Use this for initialization
	void Start () {
		button = GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Toggle() {
		toggleField.interactable = !toggleField.interactable;
		if(toggleField.interactable) {
			button.GetComponentInChildren<Text>().text = "Off";
		} else {
			button.GetComponentInChildren<Text>().text = "On";	
		}
	}
}
