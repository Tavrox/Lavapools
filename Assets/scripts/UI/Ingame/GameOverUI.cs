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

		lbInitpos = _LeaderboardUI.transform.localPosition;
		lbOutPos = new Vector3 (lbInitpos.x, lbInitpos.y-5f, lbInitpos.z);
		
		respInitpos = _RespawnUI.transform.localPosition;
		respOutPos = new Vector3 (respInitpos.x-20f, respInitpos.y, respInitpos.z);
	}

	public void GameStart()
	{
		if (this != null)
		{
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", respOutPos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", lbOutPos);
		}
	}
	public void GameOver()
	{
		if (this != null)
		{
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", respInitpos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f).Tween("localPosition", lbInitpos);
		}
	}
	public void Respawn()
	{
		if (this != null)
		{
			_lb.GatherScores(LevelManager.CurrentLevelInfo.levelID);
			new OTTween(_RespawnUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", respOutPos);
			new OTTween(_LeaderboardUI.gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("localPosition", lbOutPos);
		}
	}
}
