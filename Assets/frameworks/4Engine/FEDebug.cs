using UnityEngine;
using System.Collections;

public class FEDebug : MonoBehaviour {

	public static bool GodMode = false;
	public static bool spawnsFieldsBool = true;
	public static bool logTimecodeBool = false;

	public bool God;
	public bool spawnsFields;
	public bool logTimecode;

	void OnGUI()
	{
		if (GUI.Toggle(new Rect(100f,100f,100f,100f), GodMode ,"GodMode"))
		{
			GodMode = true;
		}
		else
		{
			GodMode = false;
		}
		           
	}

}
