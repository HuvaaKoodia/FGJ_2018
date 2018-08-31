using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bluescreen : MonoBehaviour
{
	float inputTimer;
	private void Start()
	{
		Cursor.visible = false;
		AudioManager.I.StopSounds();
		AudioManager.I.bsod.Play();
		inputTimer = Time.time + 0.6f;
	}

	void Update()
	{
		if(Time.time > inputTimer && Input.anyKeyDown)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
