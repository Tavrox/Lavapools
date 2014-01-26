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
	public static GameState state = GameState.Live;
	
	public static void TriggerGameStart()
	{
		if(GameStart != null)
		{
			Debug.LogWarning("GAMESTART");
			state = GameState.Live;
			GameStart();
		}
	}

	public static void TriggerGameOver(){
		if(GameOver != null && state != GameState.GameOver)
		{
			Debug.LogWarning("GAMEOVER");
			state = GameState.GameOver;
			GameOver();
		}
	}
	
	public static void TriggerRespawn(){
		if(Respawn != null && state != GameState.Live)
		{
			Debug.LogWarning("RESPAWN");
			state = GameState.Live;
			Respawn();
		}
	}
}
