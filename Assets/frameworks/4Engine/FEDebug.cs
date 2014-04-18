using UnityEngine;
using System.Collections;

public class FEDebug : MonoBehaviour {

	public static bool GodMode = false;
	public static bool spawnsFieldsBool = true;
	public static bool logTimecodeBool = false;
	public SpaceGate gate;

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

		if (Input.GetKeyDown(KeyCode.A))
		{
//			print ("add score");
			LevelManager _mana = GameObject.Find("LevelManager").GetComponent<LevelManager>() ;
			_mana.collecSum += 1f;
			_mana.tools.checkLevelCompletion();
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			gate = GameObject.FindGameObjectWithTag("SpaceGate").GetComponent<SpaceGate>();
			gate.triggTransition(1);
			gate.triggTransition(2);
			gate.triggTransition(3);
		}
		           
	}

}
