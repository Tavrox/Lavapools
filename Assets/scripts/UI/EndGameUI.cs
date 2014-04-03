using UnityEngine;
using System.Collections;

public class EndGameUI : SubMenu {

	private GameObject BackOne;
	private GameObject BackTwo;
	private GameObject BackOneIn;
	private GameObject BackTwoIn;
	private GameObject Crabby;
	private TextUI Succeed;

	public LeaderboardUI _LeaderboardUI;
	public RespawnUI _RespawnUI;
	public PhpLeaderboards _lb;

	private Vector3 lbInitpos;
	private Vector3 lbOutPos;
	
	private Vector3 respInitpos;
	private Vector3 respOutPos;
	
	// Use this for initialization
	public void Setup ()
	{		
		GameEventManager.GameStart += GameStart;
		GameEventManager.EndGame += EndGame;
		subMenuList = submenus.EndGame;

		_LeaderboardUI = FETool.findWithinChildren(this.gameObject, "Leaderboard").GetComponent<LeaderboardUI>();
		_lb = FETool.findWithinChildren(this.gameObject, "Leaderboard/LB_Content").GetComponent<PhpLeaderboards>();
		_RespawnUI = FETool.findWithinChildren(this.gameObject, "Respawn").GetComponent<RespawnUI>();
		_RespawnUI.Setup();
		lbInitpos = _LeaderboardUI.transform.position;
		lbOutPos = new Vector3 (lbInitpos.x, lbInitpos.y-5f, lbInitpos.z);
		respInitpos = _RespawnUI.transform.position;
		respOutPos = new Vector3 (respInitpos.x-20f, respInitpos.y, respInitpos.z);
		BackOne = FETool.findWithinChildren(gameObject, "Background/p1");
		BackTwo = FETool.findWithinChildren(gameObject, "Background/p2");
		BackOneIn = FETool.findWithinChildren(gameObject, "Background/newPosP1");
		BackTwoIn = FETool.findWithinChildren(gameObject, "Background/newPosP2");
		Succeed = FETool.findWithinChildren(gameObject, "SUCCEED").GetComponent<TextUI>();


		InvokeRepeating("UpdateScore", 0f, 0.3f);
	}
	
	public void UpdateScore()
	{
		_RespawnUI.UpdateScore(_menuMan._levman.score);
	}

	void Update()
	{
		if (LevelManager.GAMESTATE == GameEventManager.GameState.EndGame)
		{
			if (Input.GetKeyDown(KeyCode.Return))
			{
				LevelInfo _lvlIn = _menuMan._levman._profile.PROFILE.ActivatedLevels[_menuMan._levman._profile.PROFILE.ActivatedLevels.FindIndex(lvl => lvl.LvlName == _menuMan._levman.NAME)+1];
				_menuMan._levman._profile.globalVolMuted = true;
				Application.LoadLevel(_lvlIn.LvlName.ToString());
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
		}
	}
	
	public void EndGame()
	{
		if (this != null)
		{
			print ("trigg");
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", respInitpos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", lbInitpos);
			new OTTween(BackOne.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", BackOneIn.transform.position);
			new OTTween(BackTwo.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", BackTwoIn.transform.position);
			new OTTween(Succeed, 0.3f, OTEasing.QuadIn ).Tween("color", Color.white);
		}
	}
}
