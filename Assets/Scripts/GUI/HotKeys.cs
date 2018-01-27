using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HotKeys : MonoBehaviour
{
	#region variables

	#endregion

	#region initialization

	private void Start()
	{

	}

	#endregion

	#region logic

	private void Update()
	{

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}

	}

	#endregion

	#region public interface

	#endregion

	#region private interface

	#endregion

	#region events

	#endregion
}
