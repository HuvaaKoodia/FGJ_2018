using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCracker : MonoBehaviour {
    public InputField crackField, outputField;
    public List<string> hashes = new List<string>();
    public List<string> passwords = new List<string>();
    public Scrollbar loadingbar;
    public Text log;

    public void SolveHash() {
        StartCoroutine(ProgressBar(5f));
        log.text = "";
        outputField.text = "";

    }
    int CheckForValidHash(string hash) {
        for (int i = 0; i < hashes.Count; i++) {
            if (hashes[i].Contains(hash)) {
                return i;
            }
        }
        return -1;
    }
    IEnumerator ProgressBar(float loadTime) {
        float t = 0f;
        while (t < loadTime) {
            t += Time.deltaTime;
            loadingbar.size = t / loadTime;
            yield return null;
        }
        int i = CheckForValidHash(crackField.text);
        if (i < 0) {
            log.text = "Error";
        } else {
            outputField.text = passwords[i];
            log.text = "Success!";
        }
        crackField.text = "";
        loadingbar.size = 0;
    }
}
