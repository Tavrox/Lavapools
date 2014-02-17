using UnityEngine;
using System.Collections;

public class GameOverUI : SubMenu {

	public LeaderboardUI _LeaderboardUI;
	public RespawnUI _RespawnUI;
	public PhpLeaderboards _lb;

	// Use this for initialization
	public void Setup ()
	{		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		
		_LeaderboardUI = FETool.findWithinChildren(this.gameObject, "Leaderboard").GetComponent<LeaderboardUI>();
		_lb = FETool.findWithinChildren(this.gameObject, "Leaderboard/LB_Content").GetComponent<PhpLeaderboards>();
		_RespawnUI = FETool.findWithinChildren(this.gameObject, "Respawn").GetComponent<RespawnUI>();
	}

	public void GameStart()
	{
		_RespawnUI.gameObject.SetActive(false);
	}
	public void GameOver()
	{
		_RespawnUI.gameObject.SetActive(true);
	}
	public void Respawn()
	{
		_RespawnUI.gameObject.SetActive(false);
	}
}
