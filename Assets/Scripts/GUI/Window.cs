using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Events;

public class Window : MonoBehaviour 
{
#region variables
	public string title = "NONE";
	public Text titleElement;
	public UnityEvent onWindowOpen, onWindowClose;
#endregion
#region initialization
	private void Start () 
	{
		titleElement.text = title;
	}
#endregion
#region logic
#endregion
#region public interface
	public void Open()
	{
		gameObject.SetActive(true);
		onWindowOpen.Invoke();
	}

	public void Close()
	{
		gameObject.SetActive(false);
		onWindowClose.Invoke();
	}
#endregion
#region private interface
#endregion
#region events
#endregion
}
