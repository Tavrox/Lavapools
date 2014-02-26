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

		if (logTimecode == true)
		{
			logTimecodeBool = true;
		}
		else
		{
			logTimecodeBool = false;
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			if (GodMode == true)
			{
				GodMode = false;
			}
			else
			{
				GodMode = true;
			}
		}
	}

}
