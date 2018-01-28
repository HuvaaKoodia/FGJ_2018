using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shutdown : MonoBehaviour {
    public GameObject bg, black, text;
	
    public void ShutDownWindows() {
        StartCoroutine(ShutDown());
    }
    IEnumerator ShutDown() {
        bg.SetActive(true);
        FindObjectOfType<AudioManager>().tada.Play();
        yield return new WaitForSeconds(1f);
        black.SetActive(true);
        yield return new WaitForSeconds(3f);
        text.SetActive(true);
        yield return new WaitForSeconds(2f);
        if (!Application.isEditor) {
            Application.Quit();
        } else {
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
