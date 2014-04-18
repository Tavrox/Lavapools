using UnityEngine;
using System.Collections;

public class MiscButton : MonoBehaviour {

	public enum buttonList
	{
		None,
		PlayLevel,
		MuteGlobal,
		MuteMusic,
		Facebook,
		Twitter,
		TwitterPublish,
		FacebookPublish,
		Website,
		GoHome,
		CloseGame,
		OpenOptions,
		OpenLevel,
		OpenCredits,
		BackHome,
		RespawnBtn,
		ChangeLang,
		GoLink
	};
	[HideInInspector] public GameSetup SETUP;
	public GameSetup.LevelList levelToLoad;
	[HideInInspector] public BoxCollider _coll;
	[HideInInspector] public MainTitleUI mainUi;
	[HideInInspector] public ParentMenu parentUi;
	[HideInInspector] public LevelChooser chooser;
	private OTSprite spr;
	private string stdFrame;
	private string altFrame;

	public bool locked = false;
	public bool hasFocus = false;
	public buttonList buttonType;
	public string GoLink;

	public void Setup(ParentMenu _menu)
	{
		parentUi = _menu;
		if (_menu.GetComponent<MainTitleUI>() != null)
		{
			mainUi = _menu.GetComponent<MainTitleUI>();
		}
		SETUP = parentUi.PLAYERDAT.SETUP;

		if (_coll = GetComponent<BoxCollider>())
		{_coll.isTrigger = true;}
		if (GetComponentInChildren<OTSprite>() != null)
		{
			spr = GetComponentInChildren<OTSprite>();
			stdFrame = spr.frameName;
			altFrame = spr.frameName +"Alt";
		}
	}

	void Update()
	{
		if (hasFocus == true && Input.GetKey(parentUi.PLAYERDAT.INPUT.EnterButton))
	    {
			checkAction();
		}
	}

	void OnMouseDown()
	{
		checkAction();
	}

	private void checkAction()
	{
		if (locked == false)
		{
			LockButtons();
			switch (buttonType)
			{
			case buttonList.None :
			{
				print ("this button do nothing bro" + gameObject.name);
				break;
			}
			case buttonList.PlayLevel :
			{
				playCurrLvlThumb();
				break;
			}
			case buttonList.MuteGlobal :
			{
				MasterAudio.PlaySound("click");
				mainUi = GameObject.Find("TitleMenu").GetComponent<MainTitleUI>();
				mainUi.PLAYERDAT.MuteGlobal();
				break;
			}
			case buttonList.MuteMusic :
			{
				MasterAudio.PlaySound("click");
				mainUi = GameObject.Find("TitleMenu").GetComponent<MainTitleUI>();
				mainUi.PLAYERDAT.MuteMusic();
				break;
			}
			case buttonList.Twitter :
			{
				MasterAudio.PlaySound("click");
				Application.OpenURL(SETUP.twitter_url);
				break;
			}
			case buttonList.Facebook :
			{
				MasterAudio.PlaySound("click");
				Application.OpenURL(SETUP.facebook_url);
				break;
			}
			case buttonList.TwitterPublish :
			{
				
				MasterAudio.PlaySound("click");
				break;
			}
			case buttonList.FacebookPublish :
			{
				
				MasterAudio.PlaySound("click");
				break;
			}
			case buttonList.Website :
			{
				MasterAudio.PlaySound("click");
				Application.OpenURL(SETUP.website_url);
				break;
			}
			case buttonList.GoHome :
			{
				MasterAudio.PlaySound("click");
				Application.LoadLevel(0);
				break;
			}
			case buttonList.CloseGame :
			{
				MasterAudio.PlaySound("click");
				Application.Quit();
				break;
			}
			case buttonList.OpenOptions :
			{
				MasterAudio.PlaySound("click");
				mainUi.changeState(MainTitleUI.MenuStates.Options);
				mainUi.makeTransition( mainUi.Options);
				break;
			}
			case buttonList.OpenLevel :
			{
				MasterAudio.PlaySound("click");
				mainUi.makeTransition( mainUi.LevelChooser);
				mainUi.changeState(MainTitleUI.MenuStates.LevelChooser);
				break;
			}
			case buttonList.OpenCredits :
			{
				MasterAudio.PlaySound("click");
				mainUi.changeState(MainTitleUI.MenuStates.Credits);
				mainUi.makeTransition( mainUi.Credits);
				break;
			}
			case buttonList.BackHome :
			{
				print ("back home");
				MasterAudio.PlaySound("click");
				mainUi.changeState(MainTitleUI.MenuStates.Start);
				mainUi.backHome();
				break;
			}
			case buttonList.RespawnBtn :
			{
				GameObject.Find("LevelManager").GetComponent<LevelManager>().respawnPlayer("RespawnBtn");
				print ("wat");
				break;
			}
			case buttonList.ChangeLang :
			{
				if (SETUP.ChosenLanguage == GameSetup.languageList.french)
				{
					SETUP.ChosenLanguage = GameSetup.languageList.english;
				}
				else if (SETUP.ChosenLanguage == GameSetup.languageList.english)
				{
					SETUP.ChosenLanguage = GameSetup.languageList.french;	
				}
				mainUi.TranslateAllInScene();
				break;
			}
			case buttonList.GoLink :
			{
				if (GoLink != null && buttonType == buttonList.GoLink)
				{
					Application.OpenURL(GoLink);
				}
				else if (buttonType == buttonList.GoLink)
				{
					Debug.LogError("Link button without url");
					Debug.Break();
				}
				break;
			}

			}
			//			print ("clicked" + buttonType);
		}
	}

	public void playCurrLvlThumb()
	{
		LevelThumbnail _parent = gameObject.transform.parent.gameObject.GetComponent<LevelThumbnail>();
		levelToLoad = _parent.Info.LvlName;
		if (_parent.Locked == false)
		{
			StartCoroutine(delayLevelTrigger(levelToLoad.ToString()));
		}
	}

	IEnumerator delayLevelTrigger(string lvlName)
	{
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel(lvlName);
	}

	public void LockButtons()
	{
		print ("lock btn");
		lockEveryButton();
		StartCoroutine("unlockEveryButton");
	}

	public void lockEveryButton()
	{
		MiscButton[] allBtn = GameObject.FindObjectsOfType(typeof(MiscButton)) as MiscButton[];
		foreach (MiscButton _spr in allBtn)
		{
			_spr.locked = true;
		}
	}

	public void giveFocus(bool state)
	{
		if (parentUi != null && parentUi.padEntered == true)
		{
			hasFocus = state;		
			if (hasFocus == true)
			{
				if (spr != null)
				{
					spr.frameName = altFrame;
				}
			}
			else
			{
				if (spr != null)
				{
					spr.frameName = stdFrame;
				}
			}
		}
	}

	IEnumerator unlockEveryButton()
	{
		yield return new WaitForSeconds(1.1f);
		MiscButton[] allBtn = GameObject.FindObjectsOfType(typeof(MiscButton)) as MiscButton[];
		foreach (MiscButton _spr in allBtn)
		{
			_spr.locked = false;
		}
	}
}
