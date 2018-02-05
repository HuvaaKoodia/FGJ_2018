using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningPopup : MonoBehaviour
{
	public static WarningPopup I;
	public Text warningText;

	void Awake()
	{
		I = this;
		gameObject.SetActive(false);
	}

	public void Open(string warning)
	{
		GetComponent<Window>().Open();
		AudioManager.I.error.Play();
		warningText.text = warning;
	}
}
