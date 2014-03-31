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
					Application.LoadLevel(levelToLoad.ToString());
				}
				break;
			}
			case buttonList.MuteGlobal :
			{
				mainUi = GameObject.Find("TitleMenu").GetComponent<MainTitleUI>();
				mainUi.PLAYERDAT.MuteGlobal();
				break;
			}
			case buttonList.MuteMusic :
			{
				mainUi = GameObject.Find("TitleMenu").GetComponent<MainTitleUI>();
				mainUi.PLAYERDAT.MuteMusic();
				break;
			}
			case buttonList.Twitter :
			{
				Application.OpenURL(SETUP.twitter_url);
				break;
			}
			case buttonList.Facebook :
			{
				Application.OpenURL(SETUP.facebook_url);
				break;
			}
			case buttonList.TwitterPublish :
			{
				
				break;
			}
			case buttonList.FacebookPublish :
			{
				
				break;
			}
			case buttonList.Website :
			{
				Application.OpenURL(SETUP.website_url);
				break;
			}
			case buttonList.GoHome :
			{
				Application.LoadLevel(0);
				break;
			}
			case buttonList.CloseGame :
			{
				Application.Quit();
				break;
			}
			case buttonList.OpenOptions :
			{
				mainUi.makeTransition( mainUi.Options);
				break;
			}
			case buttonList.OpenLevel :
			{
				mainUi.makeTransition( mainUi.LevelChooser);
				break;
			}
			case buttonList.OpenCredits :
			{
				mainUi.makeTransition( mainUi.Credits);
				break;
			}
			case buttonList.BackHome :
			{
				mainUi.backHome();
				break;
			}
			}
//			print ("clicked" + buttonType);
		}
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
