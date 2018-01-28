using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EndingPanel : MonoBehaviour 
{
#region variables
	public SendData sendData;
	public CanvasGroup fader;
	public GameObject inputBlock;
	public Text successText;
#endregion
#region initialization
#endregion
#region logic
#endregion
#region public interface
	public void ShowEndingTransmit(bool transmissionEnded)
	{
		inputBlock.SetActive(true);
		StartCoroutine(FadeToBlack(0.5f));

		if(transmissionEnded)
		{
			
		} 
		else
		{
			
		}
	}
#endregion
#region private interface

	IEnumerator FadeToBlack(float s)
	{
		while(fader.alpha < 1)
		{
			yield return null;
			fader.alpha += s * Time.deltaTime;
		}

		fader.alpha = 1f;
		successText.enabled = true;
	}
#endregion
#region events
#endregion
}
