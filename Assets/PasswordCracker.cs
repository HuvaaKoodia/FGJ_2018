using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCracker : MonoBehaviour {
    public InputField crackField, outputField;
    public List<string> hashes = new List<string>();

    public void SolveHash() {
        if (!CheckForValidHash(crackField.text)) {
            print("Not a valid hash");
        } else {
            print("hash found");
            outputField.text = "password";
        }
        crackField.text = "";
    }
    bool CheckForValidHash(string hash) {
        for (int i = 0; i < hashes.Count; i++) {
            if (hashes[i].Contains(hash)) {
                return true;
            }
        }
        return false;
    }
}
