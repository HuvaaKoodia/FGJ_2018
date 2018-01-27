using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MoveRectToOffset : MonoBehaviour 
{
#region variables
	RectTransform rect;
	Vector2 startPos, offset = new Vector2(1, -1);
#endregion
#region initialization
	private void Start () 
	{
		rect = GetComponent<RectTransform>();
		startPos = rect.anchoredPosition;
	}
#endregion
#region logic
#endregion
#region public interface
	public void MoveToOffset()
	{
		rect.anchoredPosition = startPos + offset;
	}

	public void MoveToOriginal()
	{
		rect.anchoredPosition = startPos;
	}
#endregion
#region private interface
#endregion
#region events
#endregion
}
