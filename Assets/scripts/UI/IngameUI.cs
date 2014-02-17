using UnityEngine;
using System.Collections;

public class IngameUI : SubMenu {

	private Label Score;
	private Label Score_Best_txt;

	public void Start()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
	}

	public void GameStart()
	{
		
	}
	public void GameOver()
	{
		
	}
	public void Respawn()
	{
		
	}

}
