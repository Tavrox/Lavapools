using UnityEngine;
using System.Collections;

public class MiscButton : MonoBehaviour {

	public enum buttonList
	{
		None,
		Play,
		Mute,
		Credits,
		Facebook,
		Twitter,
		Close,
		Pad,
		TwitterPublish,
		FacebookPublish,
		Website,
		LevelMenu
	};
	public buttonList buttonType;
	private GameSetup SETUP;
	public GameSetup.LevelList levelToLoad;
	private BoxCollider _coll;

	void Start()
	{
		SETUP = Resources.Load ("Tuning/GameSetup") as GameSetup;
		if (_coll = GetComponent<BoxCollider>())
		{_coll.isTrigger = true;}
	}

	void OnMouseDown()
	{
		switch (buttonType)
		{
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
		case buttonList.Play :
		{
			LevelThumbnail _parent = gameObject.transform.parent.gameObject.GetComponent<LevelThumbnail>();
			levelToLoad = _parent.Info.LvlName;
			if (_parent.Locked == false)
			{
				Application.LoadLevel(levelToLoad.ToString());
			}
			break;
		}
		case buttonList.Close :
		{
			Application.Quit();
			break;
		}
		case buttonList.Website :
		{
			Application.OpenURL(SETUP.website_url);
			break;
		}
		case buttonList.LevelMenu :
		{
			Application.LoadLevel(0);
			break;
		}
		}
	}
}
