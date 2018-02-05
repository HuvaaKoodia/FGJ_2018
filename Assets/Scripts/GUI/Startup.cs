using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Startup : MonoBehaviour
{
	public InputField caret;
	public GameObject content, splashscreen, black, bg;

	void Start()
	{
		caret.Select();
		StartCoroutine(StartWindows());
	}

	IEnumerator StartWindows()
	{
		Cursor.visible = false;
		yield return new WaitForSeconds(2f);
		content.SetActive(false);
		yield return new WaitForSeconds(.5f);
		splashscreen.SetActive(true);
		yield return new WaitForSeconds(4f);
		splashscreen.SetActive(false);
		yield return new WaitForSeconds(1f);
		splashscreen.SetActive(true);
		yield return new WaitForSeconds(3f);
		splashscreen.SetActive(false);
		yield return new WaitForSeconds(1f);
		black.SetActive(false);
		AudioManager.I.startup.Play();
		yield return new WaitForSeconds(2f);
		gameObject.SetActive(false);
		Cursor.visible = true;
	}
}
