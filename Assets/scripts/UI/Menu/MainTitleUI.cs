using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainTitleUI : MonoBehaviour 
{
	private GameSetup SETUP;
	private List<LevelInfo> levelInformations;
	private LevelChooser Chooser;
	public enum MenuStates
	{
		Start,
		Options,
		Credits,
		LevelChooser
	};
	public MenuStates CurrentState;
	public PlayerData PLAYERDAT;

	public GameObject awayPlace;
	public GameObject frontPlace;
	public GameObject Credits;
	public GameObject Landing;
	public GameObject LevelChooser;
	public GameObject Options;
	
	void Awake () 
	{

		SETUP = Resources.Load ("Tuning/GameSetup") as GameSetup;
		Chooser = FETool.findWithinChildren(gameObject, "LevelChooser").GetComponent<LevelChooser>();
		SETUP.startTranslate(SETUP.ChosenLanguage);
		levelInformations = new List<LevelInfo> ();

		if (GameObject.Find("PlayerData") == null)
		{
			Instantiate(Resources.Load("Presets/PlayerData") as GameObject);
		}
		else
		{
			PLAYERDAT = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
		}

		if (GameObject.Find("Frameworks") == null)
		{
			GameObject fmObj = Instantiate(Resources.Load("Presets/Frameworks")) as GameObject;
			fmObj.name = "Frameworks";
		}
		
		Chooser.Setup ();
		awayPlace = FETool.findWithinChildren(gameObject, "AwayPlace");
		frontPlace = FETool.findWithinChildren(gameObject, "FrontPlace");
		Credits = FETool.findWithinChildren(gameObject, "Credits");
		Landing = FETool.findWithinChildren(gameObject, "Landing");
		LevelChooser = FETool.findWithinChildren(gameObject, "LevelChooser");
		Options = FETool.findWithinChildren(gameObject, "Options");
	}

	public static GameSetup getSetup()
	{
		GameSetup _set = Resources.Load ("Tuning/GameSetup") as GameSetup;
		return _set;
	}

	public void makeTransition (GameObject _thingIn)
	{
		new OTTween(Landing.transform,1f, OTEasing.QuadInOut).Tween("position", awayPlace.transform.position);
		new OTTween(_thingIn.transform, 1f, OTEasing.QuadInOut).Tween("position", frontPlace.transform.position);
	}

	public void backHome()
	{
		new OTTween(Credits.transform, 1f).Tween("position", awayPlace.transform.position);
		new OTTween(LevelChooser.transform, 1f).Tween("position", awayPlace.transform.position);
		new OTTween(Options.transform, 1f).Tween("position", awayPlace.transform.position);

		new OTTween(Landing.transform, 1f).Tween("position", frontPlace.transform.position);

	}
}
