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
	public Text successText, descText;
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

		string text = "";
		//if(transmissionEnded)
		{
			if(sendData.extraConnectionCount <= 1)
			{
				text = "The message was intercepted by the feds.";
				if(sendData.inputDataEncrypted)
				{
					text += "\nIt took them long to decrypt the message, but you were arrested nonetheless.";
				} else
				{
					text +="\nThey arrested you nearly immediately.";
				}

				if(sendData.inputDataUseful && sendData.inputDataEncrypted)
				{
					text += "\nThe revolution was successfull.";
				} 
				else
				{
					text += "\nThe revolution was foiled.";
				}

			}
			else if (sendData.extraConnectionCount <= 2)
			{
				text = "The message was intercepted by the feds.";
				if(sendData.inputDataEncrypted)
				{
					text += "\nIt took them too long to decrypt the message and trace it back so you managed to escape in time.";
				} else
				{
					text +="\nThey got you months later.";
				}

				if(sendData.inputDataUseful && sendData.inputDataEncrypted)
				{
					text += "\nThe revolution was successfull.";
				} 
				else
				{
					text += "\nThe revolution was foiled.";
				}
			}
			else if (sendData.extraConnectionCount == 3) 
			{
				text = "The message eluded prying eyes for long enough. The trace was discovered much later rendering the evidence cold.";

				text += "\nThe revolution was successfull.";

				if(!sendData.inputDataEncrypted)
				{
					text = "\nBut political instability ensued once the unencrypted message was linked to the coup.";
				}

			}	

			if (!sendData.inputDataEncrypted)
			{
				text += "\nNext time try not sending plain text over the pipes.";
			}

		} 
		//else
		{
			
		}

		descText.text = text;
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
		descText.enabled = true;


	}
#endregion
#region events
#endregion
}
