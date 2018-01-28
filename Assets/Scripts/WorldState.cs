using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour {

	public static bool ServerOpen1;
	public static bool ServerOpen2;
	public static bool ServerOpen3;

	public static bool Server1HasEmails = true;

	public const string homeAddress = "127.0.0.1";
	public const string server1_1Address = "65.201.411.771";
	public const string server2_1Address = "edu.wideforest.com";
	public const string server3_1Address = "corp.eztech.biz";
	public const string server4Address = "919.939.989.929";

	public const string server2_userDatabaseAddress = "edu.wideforest.com/3306";
	//public const string server2_DatabaseAddress = "edu.wideforest.com/3306";
}
