using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Clock : MonoBehaviour
{
	public float alarmTime =  1800;

	Text clock;
	int currentMinute;
	float alarmTimer;

	public UnityEvent alarmEvent;

	void Start()
	{
		alarmTimer = Time.time + alarmTime;
		clock = GetComponentInChildren<Text>();
		UpdateTime();
	}

	void Update()
	{
		if(System.DateTime.Now.Minute != currentMinute)
		{
			currentMinute = System.DateTime.Now.Minute;
			UpdateTime();
		}
			
		if(Time.time > alarmTimer)
		{
			alarmTimer = float.MaxValue;
			alarmEvent.Invoke();
		}
	}

	void UpdateTime()
	{
		clock.text = System.DateTime.Now.ToString("hh:mm tt");
	}
}
