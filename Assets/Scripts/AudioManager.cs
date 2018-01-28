using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public AudioSource error;
	public AudioSource dial;
    public AudioSource startup;
    public AudioSource tada;
    public AudioSource bsod;
    public AudioSource cracksong;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void StopDial() {
		dial.Stop();
	}
}
