using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour 
{
#region variables
	public Sprite onSprite, offSprite;
	public Image image;
	public bool startOn = true;
	bool volumeOn;
#endregion
#region initialization
	void Start()
	{
		SetValue(startOn);
	}
#endregion
#region logic
#endregion
#region public interface
#endregion
#region private interface

	void SetValue(bool on)
	{
		volumeOn = on;
		image.sprite = volumeOn ? onSprite : offSprite;

		AudioManager.I.SetMute(!volumeOn);
	}
#endregion
#region events
	public void OnToggled()
	{
		SetValue(!volumeOn);
	}
#endregion
}
