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
        currentHour = System.DateTime.Now.Hour;
        currentMinute = System.DateTime.Now.Minute;
        string AMPM = currentHour < 12 ? " AM" : " PM";
        clock.text = currentHour.ToString() + ":" + currentMinute.ToString() + AMPM;
    }
}
