using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainTitleUI : ParentMenu 
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

	[HideInInspector] public GameObject awayPlace;
	[HideInInspector] public GameObject frontPlace;
	[HideInInspector] public TextUI versionDisplay;
	[HideInInspector] public SubMenu Credits;
	[HideInInspector] public SubMenu Landing;
	[HideInInspector] public SubMenu LevelChooser;
	[HideInInspector] public SubMenu Options;

	
	void Awake () 
	{
		Screen.SetResolution(800,600, false);
		name = "TitleMenu";
		base.Setup();
		SETUP = Resources.Load ("Tuning/GameSetup") as GameSetup;
		Chooser = FETool.findWithinChildren(gameObject, "LevelChooser").GetComponent<LevelChooser>();
		SETUP.startTranslate(SETUP.ChosenLanguage);
		SETUP.translateSceneText();	
		levelInformations = new List<LevelInfo> ();

		if (GameObject.Find("Frameworks") == null)
		{
			GameObject fmObj = Instantiate(Resources.Load("Presets/Frameworks")) as GameObject;
			fmObj.name = "Frameworks";
		}
		
		Chooser.Setup ();
		awayPlace = FETool.findWithinChildren(gameObject, "AwayPlace");
		frontPlace = FETool.findWithinChildren(gameObject, "FrontPlace");
		Credits = FETool.findWithinChildren(gameObject, "Credits").GetComponent<SubMenu>();
		Landing = FETool.findWithinChildren(gameObject, "Landing").GetComponent<SubMenu>();
		LevelChooser = FETool.findWithinChildren(gameObject, "LevelChooser").GetComponent<SubMenu>();
		Options = FETool.findWithinChildren(gameObject, "Options").GetComponent<SubMenu>();
		versionDisplay = FETool.findWithinChildren(gameObject, "Landing/Underpanel/GAME_VERSION").GetComponent<TextUI>();

		SubMenu[] subMn = GetComponentsInChildren<SubMenu>();
		foreach (SubMenu sub in subMn)
		{
			sub.SetupSub(this);
			sub.setupBtn();
		}
		if (Input.GetJoystickNames().Length > 0)
		{
			padEntered = true;
			changeState(MenuStates.Start);
		}
		TranslateAllInScene();
		versionDisplay.TranslateThis();
		InvokeRepeating("checkPadMenu", 0f, 0.5f);

		if (SETUP.GameType == GameSetup.versionType.Demo)
		{
			frontPlace.transform.position = new Vector3(0f,-0.6f,-1.625f);
			FETool.findWithinChildren(gameObject, "Title").transform.position = new Vector3(0f,3f, 0.16f);
		}

//		StartCoroutine("DelayMusic");
	}

	void checkPadMenu()
	{
		if (currFocusedbtn != null)
		{
			if(Input.GetAxisRaw("X axis") > PLAYERDAT.INPUT.X_AxisPos_Btn)
			{

			}
			if(Input.GetAxisRaw("X axis") < PLAYERDAT.INPUT.X_AxisNeg_Btn )
			{

			}
			
			if(Input.GetAxisRaw("Y axis") > PLAYERDAT.INPUT.Y_AxisPos_Btn)
			{
				currFocusedbtn.giveFocus(false);
				currFocusedbtn = currentActiveMenu.findPrevBtn(currFocusedbtn);
				currFocusedbtn.giveFocus(true);
			}
			if(Input.GetAxisRaw("Y axis") < PLAYERDAT.INPUT.Y_AxisNeg_Btn)
			{
				currFocusedbtn.giveFocus(false);
				currFocusedbtn = currentActiveMenu.findNextBtn(currFocusedbtn);
				currFocusedbtn.giveFocus(true);
			}
		}

		if (CurrentState == MenuStates.LevelChooser)
		{
			if (Input.GetButton(PLAYERDAT.INPUT.TriggerLeftButton))
			{
				Chooser._btnLeft.triggerDirBtn();
			}
			if (Input.GetButton(PLAYERDAT.INPUT.TriggerRightButton))
			{
				Chooser._btnRight.triggerDirBtn();
			}
			if (Input.GetButton(PLAYERDAT.INPUT.EnterButton))
			{
				Chooser.currThumb.linkedPlayBtn.playCurrLvlThumb();
			}
			if (Input.GetButton(PLAYERDAT.INPUT.BackButton))
			{
				backHome();
			}
		}
	}

	public void changeState(MenuStates _st)
	{
		CurrentState = _st;
		switch (CurrentState)
		{
		case MenuStates.Credits :
		{
			currentActiveMenu = Credits;
			currFocusedbtn = currentActiveMenu.GetComponent<SubMenu>().menuButtons[0];
			currFocusedbtn.giveFocus(true);
			break;
		}
		case MenuStates.LevelChooser :
		{
			currentActiveMenu = LevelChooser;
			currFocusedbtn = null;
			break;
		}	
		case MenuStates.Options :
		{
			currentActiveMenu = Options;
			currFocusedbtn = currentActiveMenu.GetComponent<SubMenu>().menuButtons[0];
			currFocusedbtn.giveFocus(true);
			break;
		}
		case MenuStates.Start :
		{
			currentActiveMenu = Landing;
			currFocusedbtn = currentActiveMenu.GetComponent<SubMenu>().menuButtons[0];
			currFocusedbtn.giveFocus(true);
			break;
		}
		}
	}

	public void TranslateAllInScene()
	{
		SETUP.TextSheet.SetupTranslation(SETUP.ChosenLanguage);
		TextUI[] allTxt = GameObject.FindObjectsOfType(typeof(TextUI)) as TextUI[];
		SETUP.TextSheet.TranslateAll(ref allTxt);
	}

	public static GameSetup getSetup()
	{
		GameSetup _set = Resources.Load ("Tuning/GameSetup") as GameSetup;
		return _set;
	}

	public void makeTransition (SubMenu _thingIn)
	{
		new OTTween(Landing.transform,1f, OTEasing.QuadInOut).Tween("position", awayPlace.transform.position);
		new OTTween(_thingIn.gameObject.transform, 1f, OTEasing.QuadInOut).Tween("position", frontPlace.transform.position);
	}

	public void backHome()
	{
		new OTTween(Credits.transform, 1f).Tween("position", awayPlace.transform.position);
		new OTTween(LevelChooser.transform, 1f).Tween("position", awayPlace.transform.position);
		new OTTween(Options.transform, 1f).Tween("position", awayPlace.transform.position);

		new OTTween(Landing.transform, 1f).Tween("position", frontPlace.transform.position);
		changeState(MenuStates.Start);

	}
}
