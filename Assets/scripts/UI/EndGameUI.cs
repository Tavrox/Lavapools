using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndGameUI : SubMenu {

	private GameObject BackOne;
	private GameObject BackTwo;
	private GameObject BackOneIn;
	private GameObject BackTwoIn;
	private OTSprite CrabbySpr;
	private OTSprite StrokeSpr;
	private OTSprite VortexSpr;
	private TextUI Succeed;
	private TextUI CurrLvl;

	public LeaderboardUI _LeaderboardUI;
	public RespawnUI _RespawnUI;
	public PhpLeaderboards _lb;

	public Vector3 lbInitpos;
	public Vector3 lbOutPos;
	
	public Vector3 respInitpos;
	public Vector3 respOutPos;
	
	// Use this for initialization
	public void Setup ()
	{		
		GameEventManager.GameStart += GameStart;
		GameEventManager.EndGame += EndGame;
		subMenuList = submenus.EndGame;

		_LeaderboardUI = transform.parent.GetComponentInChildren<LeaderboardUI>();
		_lb = transform.parent.GetComponentInChildren<PhpLeaderboards>();
		_RespawnUI = transform.parent.GetComponentInChildren<RespawnUI>();

		lbInitpos = _LeaderboardUI.transform.localPosition;
		lbOutPos = new Vector3 (lbInitpos.x, lbInitpos.y-5f, lbInitpos.z);

		respInitpos = _RespawnUI.transform.localPosition;
		respOutPos = new Vector3 (respInitpos.x-20f, respInitpos.y, respInitpos.z);

		BackOne = FETool.findWithinChildren(gameObject, "Background/p1");
		BackTwo = FETool.findWithinChildren(gameObject, "Background/p2");
		BackOneIn = FETool.findWithinChildren(gameObject, "Background/newPosP1");
		BackTwoIn = FETool.findWithinChildren(gameObject, "Background/newPosP2");
		Succeed = FETool.findWithinChildren(gameObject, "SUCCEED").GetComponent<TextUI>();
		CurrLvl = FETool.findWithinChildren(gameObject, "CURRLVL").GetComponent<TextUI>();
		CurrLvl.text = LevelManager.CurrentLevelInfo.LvlName.ToString();

		CrabbySpr = FETool.findWithinChildren(gameObject, "Crab").GetComponentInChildren<OTSprite>();
		StrokeSpr = FETool.findWithinChildren(gameObject, "Door/Stroke").GetComponentInChildren<OTSprite>();
		VortexSpr = FETool.findWithinChildren(gameObject, "Door/Vortex").GetComponentInChildren<OTSprite>();
	}

	void Update()
	{
		if (LevelManager.GAMESTATE == GameEventManager.GameState.EndGame)
		{
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(_menuMan._levman._profile.INPUT.EnterButton))
			{
				LevelInfo _lvlIn = _menuMan._levman._profile.PROFILE.ActivatedLevels[_menuMan._levman._profile.PROFILE.ActivatedLevels.FindIndex(lvl => lvl.LvlName == _menuMan._levman.NAME)+1];
				if (_menuMan._levman._profile.SETUP.GameType == GameSetup.versionType.Demo && _lvlIn.availableDemo == true)
				{
					Application.LoadLevel(_lvlIn.LvlName.ToString());
				}
				else if (_menuMan._levman._profile.SETUP.GameType == GameSetup.versionType.Alpha)
				{
					Application.LoadLevel(_lvlIn.LvlName.ToString());
					LevelInfo lvl = Resources.Load("Tuning/Levels/"+_lvlIn.LvlName.ToString()) as LevelInfo;
					lvl.locked = false;
				}
				else
				{
					Application.LoadLevel(GameSetup.LevelList.Oblivion.ToString());
				}
			}
		}

	}
	
	public void GameStart()
	{
		if (this != null)
		{
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", respOutPos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", lbOutPos);
			new OTTween(Succeed, 0.3f, OTEasing.QuadIn ).Tween("color", Color.clear);
			new OTTween(CurrLvl, 0.3f, OTEasing.QuadIn ).Tween("color", Color.clear);
		}
	}
	
	public void EndGame()
	{
		if (this != null)
		{
			_RespawnUI.RespawnTextCmd.TranslateThis("PLAY_CMD");
			_RespawnUI.RespawnTextHead.TranslateThis("PLAY_NEXT");
			_RespawnUI._playerScore.text = _menuMan._levman.score.ToString();
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", respInitpos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", lbInitpos);
			new OTTween(BackOne.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", BackOneIn.transform.position);
			new OTTween(BackTwo.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", BackTwoIn.transform.position);
			new OTTween(Succeed, 0.3f, OTEasing.QuadIn ).Tween("color", Color.white);
			new OTTween(CurrLvl, 0.3f, OTEasing.QuadIn ).Tween("color", CurrLvl.initColor);
			new OTTween(CrabbySpr, 0.3f ).Tween("alpha", 1f);
			new OTTween(StrokeSpr, 0.3f ).Tween("alpha", 1f);
			new OTTween(VortexSpr, 0.3f ).Tween("alpha", 1f);
			new OTTween(BackOne.GetComponentInChildren<OTSprite>(), 0.3f ).Tween("alpha", 1f);
			new OTTween(BackTwo.GetComponentInChildren<OTSprite>(), 0.3f ).Tween("alpha", 1f);
		}
	}
}
