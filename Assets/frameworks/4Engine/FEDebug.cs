using UnityEngine;
using System.Collections;

public class FEDebug : MonoBehaviour {

	public static bool testMode = false;
	public static bool GodMode = false;
	public bool God;
	public static bool spawnsFieldsBool;
	public bool spawnsFields;

	void OnGUI()
	{
		if (God == true)
		{
			GodMode = true;
		}
		else
		{
			GodMode = false;
		}

		if (spawnsFields == true)
		{
			spawnsFieldsBool = true;
		}
		else
		{
			spawnsFieldsBool = false;
		}
	}

}
