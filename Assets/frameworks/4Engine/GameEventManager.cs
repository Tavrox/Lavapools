using UnityEngine;
using System.Collections;

public static class GameEventManager {

	public delegate void GameEvent();
	
	public static event GameEvent GameStart, GameOver, Respawn, EndGame;
	public enum GameState
	{
		Live,
		GameOver,
		MainMenu,
		EndGame
	};
	public static bool gameOver = false;
	
	public static void TriggerGameStart(string _trigger)
	{
		if(GameStart != null)
		{
			Debug.LogWarning("GAMESTART "  + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.MainMenu;
			GameStart();
		}
	}

	public static void TriggerGameOver(LevelTools.KillerList _killer)
	{
		if(GameOver != null && LevelManager.GAMESTATE != GameState.GameOver)
		{
			Debug.LogWarning("GAMEOVER "+ _killer);
			gameOver = true;
			LevelManager.GAMESTATE = GameState.GameOver;
			GameOver();
		}
	}
	
	public static void TriggerRespawn(string _trigger)
	{
		if(Respawn != null)
		{
			Debug.LogWarning("RESPAWN " + _trigger);
			gameOver = false;
			LevelManager.GAMESTATE = GameState.Live;
			Respawn();
		}
	}

	public static void TriggerEndGame()
	{
		if(EndGame != null)
		{
			Debug.LogWarning("TO THE STARS CRABBY ENGAME");
			gameOver = false;
			LevelManager.GAMESTATE = GameState.EndGame;
			EndGame();
		}
	}
}
