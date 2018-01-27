using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EncryptText : MonoBehaviour {

	public InputField input;
	public InputField output;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Enrypt() {
		// Super secure MD5 hash
		MD5 md5hash = MD5.Create(); 
		byte[] data = md5hash.ComputeHash(Encoding.UTF8.GetBytes(input.text.Trim()));
		StringBuilder sb = new StringBuilder();
		for(int i = 0; i < data.Length; i++) {
			sb.Append(data[i].ToString("x2"));
		}
		output.text = sb.ToString();
	}
}
