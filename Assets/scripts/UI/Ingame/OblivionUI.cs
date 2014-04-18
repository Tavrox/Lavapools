using UnityEngine;
using System.Collections;

public class OblivionUI : ParentMenu {

	public MiscButton BuyBtn;
	public MiscButton FeedBtn;
	public MiscButton ReturnBtn;
	public TextUI EndAlpha;
	public TextUI EndDemo;
	[HideInInspector] public PlayerData _profile;

	// Use this for initialization
	void Start () {

		if (GameObject.FindGameObjectWithTag("PlayerData") == null)
		{
			GameObject _dataplayer = Instantiate(Resources.Load("Presets/PlayerData")) as GameObject;
			_profile = _dataplayer.GetComponent<PlayerData>();
			_profile.Launch();
		}
		else
		{
			_profile = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
			_profile.Launch();
		}

		BuyBtn = FETool.findWithinChildren(gameObject, "Buy").GetComponent<MiscButton>();
		FeedBtn = FETool.findWithinChildren(gameObject, "Feedback").GetComponent<MiscButton>();
		ReturnBtn = FETool.findWithinChildren(gameObject, "ReturnHome").GetComponent<MiscButton>();
		EndAlpha = FETool.findWithinChildren(gameObject, "END_ALPHA").GetComponent<TextUI>();
		EndDemo = FETool.findWithinChildren(gameObject, "END_DEMO").GetComponent<TextUI>();


		if (_profile.SETUP.GameType == GameSetup.versionType.Alpha)
		{
			EndDemo.color = Color.clear;
		}
		if (_profile.SETUP.GameType == GameSetup.versionType.Demo)
		{
			EndAlpha.color = Color.clear;
			
		}
		SubMenu _sub = GetComponent<SubMenu>();
		_sub.SetupSub(this);
		_sub.setupBtn();
		currentActiveMenu = _sub;
		currFocusedbtn = _sub.menuButtons[0];
		_sub.menuButtons[0].giveFocus(true);
		InvokeRepeating("checkPadMenu", 0f, 0.5f);

		TranslateAllInScene();
	
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
	}

	public void TranslateAllInScene()
	{
		_profile.SETUP.TextSheet.SetupTranslation(_profile.SETUP.ChosenLanguage);
		TextUI[] allTxt = GameObject.FindObjectsOfType(typeof(TextUI)) as TextUI[];
		_profile.SETUP.TextSheet.TranslateAll(ref allTxt);
	}
}
