using UnityEngine;
using System.Collections;

public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver, Respawn;
	public enum GameState
	{
		Live,
		GameOver,
		Pause
	};
	public static bool gameOver = false;
	
	public static void TriggerGameStart(string _trigger)
	{
		if(GameStart != null && LevelManager.GAMESTATE != GameState.Live)
		{
			Debug.LogWarning("GAMESTART"  + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.Live;
			GameStart();
		}
	}

	public static void TriggerGameOver(string _killer)
	{
		if(GameOver != null && LevelManager.GAMESTATE != GameState.GameOver && FEDebug.GodMode != true)
		{
			Debug.LogWarning("GAMEOVER "+ _killer);
			gameOver = true;
			LevelManager.GAMESTATE = GameState.GameOver;
			GameOver();
		}
	}
	
	public static void TriggerRespawn(string _trigger){
		if(Respawn != null && LevelManager.GAMESTATE != GameState.Live)
		{
			Debug.LogWarning("RESPAWN" + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.Live;
			Respawn();
		}
	}
}
