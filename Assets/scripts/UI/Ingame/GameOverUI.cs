using UnityEngine;
using System.Collections;

public class GameOverUI : SubMenu {

	public LeaderboardUI _LeaderboardUI;
	public RespawnUI _RespawnUI;
	public PhpLeaderboards _lb;

	public Vector3 lbInitpos;
	public Vector3 lbOutPos;

	private Vector3 respInitpos;
	private Vector3 respOutPos;

	// Use this for initialization
	public void Setup ()
	{		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		
		_LeaderboardUI = transform.parent.GetComponentInChildren<LeaderboardUI>();
		_lb = transform.parent.GetComponentInChildren<PhpLeaderboards>();
		_lb.Setup();

		_RespawnUI = transform.parent.GetComponentInChildren<RespawnUI>();
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
