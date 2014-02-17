using UnityEngine;
using System.Collections;

public class EntryUI : SubMenu {

	// Use this for initialization
	public void Start () 
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
