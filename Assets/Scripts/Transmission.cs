using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transmission : MonoBehaviour {

	public Transform document;
	public Scrollbar transferbar;

	// Use this for initialization
	void Awake () {
		StartCoroutine(ProgressBar(5f));
	}
	
	// Update is called once per frame
	void Update () {
		//move document
		document.eulerAngles += (Vector3.forward * 150f) * Time.deltaTime; 
	}

	IEnumerator ProgressBar(float loadTime) {
		float t = 0f;
		while (t < loadTime) {
			t += Time.deltaTime;
			transferbar.size = t / loadTime;
			yield return null;
		}
		// TODO: End Game
		print("WIN GAME");
	}
}
