using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolBrick : LevelBrick 
{

	[HideInInspector] public bool debug;
	public Waypoint currentWP;

	private WaypointManager initPath;
	public WaypointManager brickPath;

	public bool isRandomSpawn = true;
	public Waypoint initWp;

	[HideInInspector] public ColliderKiller _collKiller;


	// Use this for initialization
	public void Setup () 
	{
		base.Setup();

		if (brickPath == null)
		{
			Debug.Log("The path on " +name+ " is missing");
			Debug.Break();
		}

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		if (_collKiller = GetComponentInChildren<ColliderKiller>())
		{
			_collKiller.Setup(this);
		}
		if (isRandomSpawn == false && initWp == null)
		{
			Debug.Break();
			Debug.LogError("Non-Random Brick hasn't his init WP setupped" + "[" + gameObject.name  +"]");
		}
		InvokeRepeating("RecalculateTarget", 0f, 0.01f);
	}

	public void setupPath()
	{
		if (isRandomSpawn == false && initWp != null)
		{
			currentWP = initWp;
		}
		else
		{
			currentWP = brickPath.pickRandomWP();
		}

		if (type == typeList.Chainsaw && isRandomSpawn == true)
		{
			gameObject.transform.position = currentWP.transform.position;
		}

		if (type == typeList.Bird)
		{
			GameObject spw = GameObject.Find("LevelManager/Enviro/OuterSpawn");
			gameObject.transform.position = new Vector3(spw.transform.position.x, spw.transform.position.y, 0f);
		}

		initPath = brickPath;
		setupTarget();
	}

	public void assignPath(WaypointManager wpm)
	{
		brickPath = wpm;
	}

	public void setupTarget()
	{
		pos = gameObject.transform.position;
		target = currentWP.nextWP.transform.position;
		direction = Vector3.Normalize(target - pos);
	}

	public void RecalculateTarget()
	{
		pos = gameObject.transform.position;
		direction = Vector3.Normalize(target - pos);
	}

	void Update () {
			
		if (GameEventManager.gameOver != true && isEnabled == true)
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
		if (this != null)
		{

		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			setupPath();
//			animSpr.Stop();
		}
	}

//	void OnDrawGizmos()
//	{
//		if (brickPath != null)
//		{
//			if (initWp != null)
//			{
////				Gizmos.color = Color.yellow;
////				Gizmos.DrawLine( gameObject.transform.position, initWp.transform.position);
//			}
//			else
//			{
////				Gizmos.color = Color.blue;
////				Gizmos.DrawLine( gameObject.transform.position, brickPath.relatedWaypoints[0].transform.position);
//			}
//		}
//	}
}
