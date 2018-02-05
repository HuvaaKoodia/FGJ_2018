using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Database
{
	public string serverAddress;
	public List<Table> tables;

	[System.Serializable]
	public struct Table
	{
		public string name;
		public List<Row> rows;
	}

	[System.Serializable]
	public struct Row
	{
		public List<string> values;
	}
}

public class MySQL : MonoBehaviour
{
	public Window window;
	public Button requestButton;
	public InputField serverInput, output;
	public Dropdown tableSelection;
	public List<Database> databases = new List<Database>();
	int serverIndex;

	void Awake()
	{
		//change database addresses to match world state
		databases[0].serverAddress = WorldState.server2userDatabaseAddress;

		window.onWindowOpen.AddListener(OnOpen);
	}

	void OnOpen()
	{
		tableSelection.interactable = false;
		tableSelection.captionText.text = "";
		requestButton.interactable = false;
	}

	public void ConnectToServer()
	{
		for(int i = 0; i < databases.Count; i++)
		{
			if(databases[i].serverAddress == serverInput.text.Trim())
			{
				output.text = "Connected to server: " + databases[i].serverAddress;
				serverIndex = i;
				for(int j = 0; j < databases[serverIndex].tables.Count; j++)
				{
					tableSelection.options[j].text = databases[serverIndex].tables[j].name;
				}
				tableSelection.RefreshShownValue();
				tableSelection.interactable = true;
				requestButton.interactable = true;


				return;
			} else
			{
				output.text = "Server " + serverInput.text.Trim() + " not found";
			}
		}
	}


	public void RequestTable()
	{
		ClearOutputField();
		Database db = databases[serverIndex];
		int tableIndex = tableSelection.value;

		//output.text += "+----+----------+-----------------------------+" + "\n";
		output.text += string.Format("{0,-10}{1,-10}{2,-10}\n\n", "id", "name", "password");
		//output.text += "+----+----------+-----------------------------+" + "\n";

		for(int i = 0; i < db.tables[tableIndex].rows.Count; i++)
		{
			string line = "";
			for(int j = 0; j < db.tables[tableIndex].rows[i].values.Count; j++)
			{

				line += string.Format("{0,-10}", db.tables[tableIndex].rows[i].values[j]);
			}
			output.text += line + "\n";
		}
	}

	void ClearOutputField()
	{
		output.text = "";
	}

}
