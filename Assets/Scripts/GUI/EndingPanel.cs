using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class EndingPanel : MonoBehaviour
{
	#region variables

	public SendData sendData;
	public CanvasGroup fader;
	public GameObject inputBlock;
	public Text successText, descText;

	public UnityEvent onEnd;


	bool overtime = false;

	#endregion

	#region initialization

	void Start()
	{
		//tests

//		WorldState.ServerOpen1 = true;
//		WorldState.ServerOpen2 = true;
//		WorldState.ServerOpen3 = true;
//
//		sendData.extraConnectionCount = 3;
//		sendData.inputDataEncrypted = false;
//		sendData.inputDataUseful = false;
//		overtime = false;
//
//		ShowEndingTransmit();
	}

	#endregion

	#region logic

	#endregion

	#region public interface

	public void ShowEndingTransmit()
	{
		Cursor.visible = false;
		inputBlock.SetActive(true);
		AudioManager.I.SetMute(true);

		StartCoroutine(FadeToBlack(0.5f));

		string text = "";
		bool skipPlainTextMessage = false;

		if(sendData.extraConnectionCount <= 1)
		{
			text = "The message was intercepted by the feds.";
			if(sendData.inputDataEncrypted)
			{
				text += "\nIt took them long to decrypt the message, but you were arrested nonetheless.";
			} else
			{
				text += "\nThey arrested you nearly immediately.";
			}
			text += "\n";

			text += "\nThe data never reached its intended address.";

		} else if(sendData.extraConnectionCount <= 2)
		{
			text = "The message was traced by the feds.";
			if(sendData.inputDataEncrypted)
			{
				text += "\nIt took them too long to decrypt the message so you managed to escape in time.";
			} else
			{
				text += "\nThey got you months later.";
			}

			text += "\n";

			if(overtime)
			{
				text += "\nThe data reached its intended address, but came too late to be of any use.\nThe revolution was foiled.";
			} else
			{
				text += "\nThe data reached its intended address";
				if(sendData.inputDataUseful && sendData.inputDataEncrypted)
				{
					text += ".\nThe revolution was successfull!";
				} else if(!sendData.inputDataEncrypted)
				{
					text += ", but was read by both sides of the conflict rendering it useless.\nThe revolution was foiled.";
				} else if(!sendData.inputDataUseful)
				{
					text += ", but was of no particular help.\nThe revolution was foiled.";
				}
			}
		} else if(sendData.extraConnectionCount == 3)
		{
			text = "The message eluded prying eyes for long enough. The trace was discovered much later rendering the evidence cold.";

			text += "\n";

			if(overtime)
			{
				text += "\nThe data reached its intended address, but came too late to be of any use.\nThe revolution was foiled.";
			} else
			{
				if(sendData.inputDataUseful)
				{
					text += "\nThe data reached its intended address.";
					text += "\nThe revolution was successfull";

					if(!sendData.inputDataEncrypted)
					{
						text += ", but political instability ensued once the unencrypted message was linked to the coup.";
					}
				} else
				{
					text += "\nThe data reached its intended address.";
					text += "\nUnfortunately the information contained within was of no use.\nThe revolution was foiled!";
					skipPlainTextMessage = true;
				}
			}
		}	

		if(!sendData.inputDataEncrypted && !skipPlainTextMessage && !overtime)
		{
			text += "\n\nNext time try not sending plain text over the pipes.";
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
		onEnd.Invoke();
	}

	public void SetOvertime()
	{
		overtime = true;
	}

	#endregion

	#region events

	#endregion
}
