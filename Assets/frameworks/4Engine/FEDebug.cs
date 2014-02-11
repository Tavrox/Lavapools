using UnityEngine;
using System.Collections;

public class FEDebug : MonoBehaviour {

	public static bool testMode = false;
	public static bool GodMode = false;
	public bool God;

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
	}

}
