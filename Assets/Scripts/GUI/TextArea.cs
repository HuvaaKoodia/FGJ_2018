using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;

public class TextArea : MonoBehaviour 
{
#region variables
	public Window window;
	public InputField inputField;
	public RectTransform resetToZero;
	public Text text;
	[TextArea(1,200)]
	public string startText;

	public bool missionTextHack = false;
#endregion
#region initialization
	private void Start () 
	{
		resetToZero.anchoredPosition = Vector2.zero;

		if(inputField)
			text.GetComponent<LayoutElement>().minWidth = window.GetComponent<RectTransform>().sizeDelta.x - 30;

		if(inputField && startText != "")
		{
			if(missionTextHack)
			{
				startText = string.Format(startText, WorldState.serverAddress4, System.DateTime.Now.Add(new System.TimeSpan(0, 30, 0)).ToString("hh:mm tt"));
			}
			inputField.text = startText;
		}
	}
#endregion
#region logic

#endregion
#region public interface
#endregion
#region private interface
#endregion
#region events
#endregion
}
