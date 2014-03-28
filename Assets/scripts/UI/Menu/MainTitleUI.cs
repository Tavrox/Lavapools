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
		Chooser.Setup ();
		SETUP.startTranslate(SETUP.ChosenLanguage);
		levelInformations = new List<LevelInfo> ();
		PLAYERDAT = GameObject.Find("PlayerData").GetComponent<PlayerData>();

		if (GameObject.Find("Frameworks") == null)
		{
			GameObject fmObj = Instantiate(Resources.Load("Presets/Frameworks")) as GameObject;
			fmObj.name = "Frameworks";
		}
		
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
		print ("entered here bro");
		new OTTween(Landing.transform, 1f).Tween("position", awayPlace.transform.position);
		new OTTween(_thingIn.transform, 1f).Tween("position", frontPlace.transform.position);
	}

	public void backHome()
	{
		new OTTween(Credits.transform, 1f).Tween("position", awayPlace.transform.position);
		new OTTween(LevelChooser.transform, 1f).Tween("position", awayPlace.transform.position);
		new OTTween(Options.transform, 1f).Tween("position", awayPlace.transform.position);

		new OTTween(Landing.transform, 1f).Tween("position", frontPlace.transform.position);

	}
}
