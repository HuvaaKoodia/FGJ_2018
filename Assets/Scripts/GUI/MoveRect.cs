using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class MoveRect : MonoBehaviour, IDragHandler, IBeginDragHandler, IPointerUpHandler, IEndDragHandler, IPointerDownHandler, IPointerClickHandler
{
#region variables
	public bool canMove = true, canMoveToFront = true;
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
		if(!canMove) return;

		var parentPos = parentRect.anchoredPosition;
		offset = eventData.position - parentPos;
		print(offset);
		dragging = true;

		if(!canMoveToFront) return;
		parentRect.SetAsLastSibling();
	}

	public void OnDrag(PointerEventData eventData)
	{
		if(!canMove) return;

		parentRect.anchoredPosition = eventData.position - offset;

	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if(!canMove) return;

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
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		//needed for drag events to work
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		if(!canMoveToFront) return;

		parentRect.SetAsLastSibling();
	}

#endregion
}
