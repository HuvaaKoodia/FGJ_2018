using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextArea : MonoBehaviour 
{
#region variables
	public Window window;
	public InputField inputField;
	public RectTransform resetToZero;
	public Text text;
	[TextArea(1,200)]
	public string startText;
#endregion
#region initialization
	private void Start () 
	{
		resetToZero.anchoredPosition = Vector2.zero;

		if(inputField)
			text.GetComponent<LayoutElement>().minWidth = window.GetComponent<RectTransform>().sizeDelta.x - 30;

		if (inputField && startText != "")
			inputField.text = startText;
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
