using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolBrick : LevelBrick {

	[HideInInspector] public string brickPathId;
	[HideInInspector] public Waypoint currentWP;
	public bool debug;
	public WaypointManager brickPath;

	private Waypoint initWp;
	private WaypointManager initPath;

	public ColliderKiller _collKiller;


	// Use this for initialization
	public void Setup () 
	{
		base.Setup();

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		if (_collKiller = GetComponentInChildren<ColliderKiller>())
		{
			_collKiller.Setup(this);
		}
		InvokeRepeating("UpdateMovement", 0f, 0.01f);
	}

	public void saveWaypoints()
	{
		initWp = currentWP;
		initPath = brickPath;
	}

	public void setupTarget()
	{
		if (currentWP != null)
		{
			pos = gameObject.transform.position;
			target = currentWP.nextWP.transform.position;
			direction = Vector3.Normalize(target - pos);
		}
	}

	void UpdateMovement () {
			
		if (GameEventManager.gameOver != true)
		{
			pos = gameObject.transform.position;
			gameObject.transform.position += new Vector3 ( speed * direction.x, speed * direction.y, 0f);
		}
	}
	
	void OnTriggerEnter(Collider _oth)
	{
		/*
		if (_oth.CompareTag("Player") && LevelManager.GAMESTATE != GameEventManager.GameState.MainMenu)
		{
			if (type == typeList.Bird)
			{
				GameEventManager.TriggerGameOver(LevelTools.KillerList.Bird);
			}
			if (type == typeList.Chainsaw)
			{
				GameEventManager.TriggerGameOver(LevelTools.KillerList.Chainsaw);
			}
		}
		*/
//		print (_oth.gameObject.name);
	}

	IEnumerator GoToNext(Waypoint _wp)
	{
		print (Vector2.Distance(_wp.transform.position, gameObject.transform.position));
		yield return new WaitForSeconds((speed /24f) * Vector2.Distance(_wp.transform.position, gameObject.transform.position) );
		GoToWaypoint(_wp);
	}
	
	public void GoToNextWP(Waypoint _wp)
	{
		StartCoroutine (GoToNext(_wp));
	}
	public void GoToWaypoint(Waypoint _wp)
	{
//		if (currentWP != null && pos != null && type != typeList.Fields)
//		{
			currentWP = currentWP.nextWP;
			target = _wp.transform.position;
			direction = Vector3.Normalize(target - pos);
//		}
	}

	public void brickBounce()
	{

	}
	
	private void GameStart()
	{

	}
	
	private void GameOver()
	{

	}
	
	private void Respawn()
	{
		if (this != null && enabled == true)
		{
			currentWP = brickPath.pickRandomWP();
			transform.position = brickPath.findNextWaypoint(currentWP).transform.position;
			initPath = brickPath;
			setupTarget();
		}
	}
}
