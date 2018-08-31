using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EncryptText : MonoBehaviour
{
	public InputField input;
	public InputField output;
	public Button encryptButton;
	public Animator waveAnimation;

	float timer;

	// Use this for initialization
	void Start()
	{
		input.onValueChanged.AddListener(OnInputChanged);
		ResetProgress();
	}
	
	// Update is called once per frame
	void Update()
	{
		if(timer > 0)
		{
			timer -= Time.deltaTime;
			if(timer < 0)
			{
				timer = 0;
				OutputHash();
				waveAnimation.SetBool("Wave", false);
			}
		} 
	}

	void OutputHash()
	{
		output.interactable = true;
		// Super secure MD5 hash
		MD5 md5hash = MD5.Create(); 
		byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input.text.Trim()));
		StringBuilder sb = new StringBuilder();
		for(int i = 0; i < data.Length; i++)
		{
			sb.Append(data[i].ToString("x2"));
		}
		output.text = sb.ToString();
	}

	public void Enrypt()
	{
		output.interactable = false;
		timer = 2f;
		output.text = "";
		waveAnimation.SetBool("Wave", true);
	}

	public void OnInputChanged(string value)
	{
		bool interactable = value != "";
		encryptButton.interactable = interactable;
	}
	
	public void ResetProgress()
	{
		input.text = "";
		output.text = "";
		timer = 0;
		OnInputChanged("");
		output.interactable = false;
		waveAnimation.SetBool("Wave", false);
	}
}
