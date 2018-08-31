using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCracker : MonoBehaviour
{
    public InputField crackField, outputField;
    public List<string> hashes = new List<string>();
    public List<string> passwords = new List<string>();
    public Scrollbar loadingbar;
    public Text log;
    bool cracking = false;

    public void PlaySong()
    {
        AudioManager.I.PlayMusic();
    }
    public void StopSong()
    {
        AudioManager.I.StopMusic();
    }
    public void SolveHash()
    {
        if (cracking) return;
        
        StartCoroutine(ProgressBar(5f));
    }
    
    int CheckForValidHash(string hash)
    {
        for (int i = 0; i < hashes.Count; i++)
        {
            if (hashes[i] == hash)
                return i;
        }
        return -1;
    }
    
    IEnumerator ProgressBar(float loadTime)
    {
        string text = crackField.text.Trim();
        if (string.IsNullOrEmpty(text))
            yield break;
            
        log.text = "";
        outputField.text = "";
        
        cracking = true;

        float t = 0f;
        while (t < loadTime)
        {
            t += Time.deltaTime;
            loadingbar.size = t / loadTime;
            yield return null;
        }

        int i = CheckForValidHash(text);
        if (i < 0)
        {
            log.text = "Error";
        }
        else
        {
            outputField.text = passwords[i];
            log.text = "Success!";
        }
        crackField.text = "";
        loadingbar.size = 0;
        cracking = false;
    }
    
    public void ResetProgressBar()
    {
        cracking = false;
        loadingbar.size = 0;
    }
}