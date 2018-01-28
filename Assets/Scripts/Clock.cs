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
		clock.text = System.DateTime.Now.ToString("hh:mm tt");
    }
}
