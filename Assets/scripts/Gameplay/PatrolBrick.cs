using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolBrick : LevelBrick {

	public WaypointManager brickPath;
	public string brickPathId;
	public Waypoint currentWP;

	private Waypoint initWp;
	private WaypointManager initPath;

	public bool debug;

	// Use this for initialization
	public void Setup () {

		base.Setup();
		if (currentWP == null)
		{
			Debug.Log("The brick "+gameObject.name+" has no wp");
		}

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

		InvokeRepeating("StepUpdate", 0f, _levMan.TuningDocument.GLOBAL_speed);

	}
	
	
	public void saveWaypoints()
	{
		initWp = currentWP;
		initPath = brickPath;
	}

	public void setupTarget()
	{
		pos = gameObject.transform.position;
		target = currentWP.nextWP.transform.position;
		direction = (target - pos).normalized;
	}
	
	// Update is called once per frame
	void StepUpdate () {
			
		if (GameEventManager.gameOver != true)
		{
			pos = gameObject.transform.position;
			gameObject.transform.position += new Vector3 ( speed * FETool.Round( direction.x, 2), speed * FETool.Round( direction.y, 2) , 0f);
		}
	}
	
	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player"))
		{
			GameEventManager.TriggerGameOver(gameObject.name);
		}
//		print (_oth.gameObject.name);
	}
	
	public void GoToWaypoint(Waypoint _wp)
	{
		if (currentWP != null && pos != null && type != typeList.Fields)
		{
			currentWP = currentWP.nextWP;
			target = currentWP.nextWP.transform.position;
			direction = (target - pos).normalized;
		}
	}

	public void brickBounce()
	{
		if (type != typeList.Fields)
		{
			_soundList[0].playSound(brickId);
		}
	}
	
	private void GameStart()
	{

	}
	
	private void GameOver()
	{

	}
	
	private void Respawn()
	{
		if (gameObject != null && enabled == true)
		{
			gameObject.transform.position = initPos;
			currentWP = initWp;
			initPath = brickPath;
			setupTarget();
		}
	}
}
