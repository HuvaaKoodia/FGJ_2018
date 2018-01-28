using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Startup : MonoBehaviour {
    public InputField caret;
    public GameObject content, splashscreen, black, bg;
	// Use this for initialization
	void Start () {
        caret.Select();
        StartCoroutine(StartWindows());
	}
	
	IEnumerator StartWindows() {
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
        FindObjectOfType<AudioManager>().startup.Play();
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
