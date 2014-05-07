using UnityEngine;
using System.Collections;

public class Carpet : PatrolBrick {
	
	public void Start () 
	{
		base.Setup();
		type = typeList.Carpet;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		if (brickPath != null)
		{
			brickPath.relatedBrick = this;
			brickPathId = brickPath.id;
		}
		else
		{
			Debug.Log("The path of "+gameObject.name+" is missing.");
		}

		setupPath();
	}

	private void GameStart()
	{
		if (this != null)
		{

		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{

		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{

		}
	}
}
