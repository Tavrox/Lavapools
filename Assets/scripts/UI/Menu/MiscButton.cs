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
		BackHome
	};
	public GameSetup SETUP;
	public GameSetup.LevelList levelToLoad;
	public BoxCollider _coll;
	public MainTitleUI mainUi;
	public LevelChooser chooser;

	public bool locked = false;
	public buttonList buttonType;

	void Start()
	{
		SETUP = Resources.Load ("Tuning/GameSetup") as GameSetup;
		if (GameObject.Find("TitleMenu") != null)
		{
			mainUi = GameObject.Find("TitleMenu").GetComponent<MainTitleUI>();
		}
		if (_coll = GetComponent<BoxCollider>())
		{_coll.isTrigger = true;}
	}

	void OnMouseDown()
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
				LevelThumbnail _parent = gameObject.transform.parent.gameObject.GetComponent<LevelThumbnail>();
				levelToLoad = _parent.Info.LvlName;
				if (_parent.Locked == false)
				{
					StartCoroutine(delayLevelTrigger(levelToLoad.ToString()));
				}
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
				mainUi.makeTransition( mainUi.Options);
				break;
			}
			case buttonList.OpenLevel :
			{
				MasterAudio.PlaySound("click");
				mainUi.makeTransition( mainUi.LevelChooser);
				break;
			}
			case buttonList.OpenCredits :
			{
				MasterAudio.PlaySound("click");
				mainUi.makeTransition( mainUi.Credits);
				break;
			}
			case buttonList.BackHome :
			{
				MasterAudio.PlaySound("click");
				mainUi.backHome();
				break;
			}
			}
//			print ("clicked" + buttonType);
		}
	}

	IEnumerator delayLevelTrigger(string lvlName)
	{
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(lvlName);
	}

	public void LockButtons()
	{
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
