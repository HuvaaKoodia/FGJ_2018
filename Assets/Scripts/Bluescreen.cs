using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bluescreen : MonoBehaviour {

    private void Start() {
        FindObjectOfType<AudioManager>().bsod.Play();
    }
    void Update () {
        if (Input.anyKeyDown){
            Application.Quit();
            print("Closing application");
        }
	}
}
