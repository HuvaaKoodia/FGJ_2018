using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
	public static AudioManager I;

	public AudioMixer mixer;

	public AudioSource error;
	public AudioSource dial;
	public AudioSource startup;
	public AudioSource tada;
	public AudioSource bsod;
	public AudioSource cracksong;

	void Awake()
	{
		I = this;
	}

	public void PlayMusic()
	{
		if (!cracksong.isPlaying)
			cracksong.Play();
	}

	public void StopMusic()
	{
		cracksong.Stop();
	}

	public void StopSounds()
	{
		cracksong.Stop();
		dial.Stop();
	}

	public void StopDial()
	{
		dial.Stop();
	}

	public void SetMute(bool mute)
	{
		mixer.SetFloat("Volume", mute ? -80 : 0);
	}
}
