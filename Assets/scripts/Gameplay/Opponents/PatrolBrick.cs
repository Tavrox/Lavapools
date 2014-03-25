using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolBrick : LevelBrick {

	[HideInInspector] public string brickPathId;
	public Waypoint currentWP;
	public bool debug;
	[HideInInspector] public WaypointManager brickPath;

	public Waypoint initWp;
	private WaypointManager initPath;

	[HideInInspector] public ColliderKiller _collKiller;


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
		InvokeRepeating("RecalculateTarget", 0f, 0.01f);
//		InvokeRepeating("Move", 0f, 0.1f);
	}

	public void setupPath()
	{
		currentWP = brickPath.pickRandomWP();
//		transform.position = brickPath.findNextWaypoint(currentWP).transform.position;
		transform.position = currentWP.transform.position;
		initWp = currentWP;
		initPath = brickPath;
		setupTarget();
	}

	public void setupTarget()
	{
		pos = gameObject.transform.position;
		target = currentWP.nextWP.transform.position;
		direction = Vector3.Normalize(target - pos);
	}

	public void RecalculateTarget()
	{
		direction = Vector3.Normalize(target - pos);
	}

	void Update () {
			
		if (GameEventManager.gameOver != true)
		{
			pos = gameObject.transform.position;
			Debug.DrawLine(pos,  (pos + new Vector3 ( (speed * direction.x) * 50f, (speed * direction.y) * 50f, 0f)), Color.blue);
//			gameObject.transform.Translate(new Vector3 ( (speed * direction.x) * Time.deltaTime, (speed * direction.y) * Time.deltaTime, 0f));
			gameObject.transform.position += new Vector3 ( (speed * direction.x) * Time.deltaTime, (speed * direction.y) * Time.deltaTime, 0f);
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
		currentWP = _wp.nextWP;
		target = _wp.transform.position;
		direction = Vector3.Normalize(target - pos);
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
		setupPath();
	}
}
