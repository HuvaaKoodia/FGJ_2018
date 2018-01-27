using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveRect : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerUpHandler, IEndDragHandler, IPointerDownHandler
{
#region variables
	public RectTransform parentRect;
	Vector2 offset;
	bool dragging = false;
	public UnityEvent onButtonPress;
#endregion
#region initialization
#endregion
#region logic
#endregion
#region public interface
#endregion
#region private interface
#endregion
#region events
	public void OnBeginDrag(PointerEventData eventData)
	{
		var parentPos = parentRect.anchoredPosition;
		offset = eventData.position - parentPos;
		print(offset);
		dragging = true;
	}

	public void OnDrag(PointerEventData eventData)
	{
		parentRect.anchoredPosition = eventData.position - offset;

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		dragging = false;
		Rect r = new Rect(0, 38, Screen.width, Screen.height);
		var size = parentRect.sizeDelta;

		bool topInside = r.Contains(parentRect.anchoredPosition);
		bool topRightInside = r.Contains(parentRect.anchoredPosition+ Vector2.right * size.x);
		bool bottomInside = r.Contains(parentRect.anchoredPosition + Vector2.down * size.y);
		bool bottomRightInside = r.Contains(parentRect.anchoredPosition + new Vector2(size.x, -size.y));

		if(
			(!topInside && !topRightInside) ||
			(!topInside && !topRightInside && !bottomInside && !bottomRightInside)
		)
		{
			parentRect.anchoredPosition = r.center;
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if(!dragging)
		{
			onButtonPress.Invoke();
		}
		print("U");

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		print("D");
	}

#endregion
}
