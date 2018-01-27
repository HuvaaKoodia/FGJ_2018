using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Clock : MonoBehaviour {
    Text clock;
    int currentHour, currentMinute;

    //public UnityEvent 
	void Start () {
        clock = GetComponentInChildren<Text>();
        UpdateTime();
    }
	
	// Update is called once per frame
	void Update () {
		if(System.DateTime.Now.Minute != currentMinute) {
            UpdateTime();
        }
	}
    void UpdateTime() {
		clock.text = System.DateTime.Now.ToShortTimeString();
    }
}
