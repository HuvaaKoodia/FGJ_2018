using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Clock : MonoBehaviour {
    Text clock;
    int currentHour, currentMinute;
    float endTime = 1800, playedTime;

    public UnityEvent endGame;

    void Start () {
        clock = GetComponentInChildren<Text>();
        UpdateTime();
    }
	
	void Update () {
		if(System.DateTime.Now.Minute != currentMinute) {
            UpdateTime();
        }
        playedTime += Time.deltaTime;
        if(playedTime > endTime) {
            endGame.Invoke();
        }
	}
    void UpdateTime() {
        currentHour = System.DateTime.Now.Hour;
        currentMinute = System.DateTime.Now.Minute;
        string AMPM = currentHour < 12 ? " AM" : " PM";
        clock.text = currentHour.ToString() + ":" + currentMinute.ToString() + AMPM;
    }
}
