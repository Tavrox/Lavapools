using UnityEngine;
using System.Collections;

public class GameOverUI : SubMenu {

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
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		
		_LeaderboardUI = FETool.findWithinChildren(this.gameObject, "Leaderboard").GetComponent<LeaderboardUI>();
		_lb = FETool.findWithinChildren(this.gameObject, "Leaderboard/LB_Content").GetComponent<PhpLeaderboards>();
		_lb.Setup();

		_RespawnUI = FETool.findWithinChildren(this.gameObject, "Respawn").GetComponent<RespawnUI>();
		_RespawnUI.Setup(this);

		lbInitpos = _LeaderboardUI.transform.position;
		lbOutPos = new Vector3 (lbInitpos.x, lbInitpos.y-5f, lbInitpos.z);
		
		respInitpos = _RespawnUI.transform.position;
		respOutPos = new Vector3 (respInitpos.x-20f, respInitpos.y, respInitpos.z);
	}

	public void GameStart()
	{
		if (this != null)
		{
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", respOutPos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", lbOutPos);
		}
	}
	public void GameOver()
	{
		if (this != null)
		{
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", respInitpos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f).Tween("position", lbInitpos);
		}
	}
	public void Respawn()
	{
		if (this != null)
		{
			_lb.GatherScores(LevelManager.CurrentLevelInfo.levelID);
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", respOutPos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", lbOutPos);
		}
	}
}
