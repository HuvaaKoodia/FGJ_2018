using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Window : MonoBehaviour 
{
#region variables
	public string title = "NONE";
	public Text titleElement;
#endregion
#region initialization
	private void Start () 
	{
		titleElement.text = title;
	}
#endregion
#region logic
	private void Update () 
	{
	
	}
#endregion
#region public interface
	public void Open()
	{
		gameObject.SetActive(true);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}
#endregion
#region private interface
#endregion
#region events
#endregion
}
