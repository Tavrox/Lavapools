using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolBrick : LevelBrick {

	public List<Waypoint> waypoints;
	public Waypoint currentWP;

	private Waypoint initWp;
	private List<Waypoint> initWaypoints;

	public bool logTimecode;

	// Use this for initialization
	public void Start () {

		base.Start();

		if (currentWP == null)
		{
			Debug.Log("The brick "+gameObject.name+" has no wp");
		}
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		InvokeRepeating("StepUpdate", 0f, _levMan.TuningDocument.GLOBAL_speed);

	}
	
	
	public void saveWaypoints()
	{
		initWp = currentWP;
		initWaypoints = waypoints;
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
		if (logTimecode == true)
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
		if (gameObject != null)
		{
			gameObject.transform.position = initPos;
			currentWP = initWp;
			initWaypoints = waypoints;
			setupTarget();
		}
	}
}
