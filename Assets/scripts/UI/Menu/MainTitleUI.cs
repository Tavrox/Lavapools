using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainTitleUI : MonoBehaviour 
{
	private GameSetup SETUP;
	private List<LevelInfo> levelInformations;
	private LevelChooser Chooser;

	void Awake () {
		SETUP = Resources.Load ("Tuning/GameSetup") as GameSetup;
		Chooser = FETool.findWithinChildren(gameObject, "LevelChooser").GetComponent<LevelChooser>();
		Chooser.Setup ();
		levelInformations = new List<LevelInfo> ();
	}

	public static GameSetup getSetup()
	{
		GameSetup _set = Resources.Load ("Tuning/GameSetup") as GameSetup;
		return _set;
	}
}
