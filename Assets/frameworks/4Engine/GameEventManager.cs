using UnityEngine;
using System.Collections;

public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver, Respawn;
	public enum GameState
	{
		Live,
		GameOver,
		MainMenu
	};
	public static bool gameOver = false;
	
	public static void TriggerGameStart(string _trigger, bool forced = false)
	{
		if(GameStart != null)
		{
//			Debug.LogWarning("GAMESTART "  + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.MainMenu;
			GameStart();
		}
	}

	public static void TriggerGameOver(string _killer, bool forced = false)
	{
		if(GameOver != null && LevelManager.GAMESTATE != GameState.GameOver && FEDebug.GodMode != true)
		{
//			Debug.LogWarning("GAMEOVER "+ _killer);
			gameOver = true;
			LevelManager.GAMESTATE = GameState.GameOver;
			GameOver();
		}
	}
	
	public static void TriggerRespawn(string _trigger, bool forced = false){
		if(Respawn != null)
		{
//			Debug.LogWarning("RESPAWN " + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.Live;
			Respawn();
		}
	}
}
