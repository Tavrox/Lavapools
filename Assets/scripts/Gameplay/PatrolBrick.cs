using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolBrick : LevelBrick {

	public List<Waypoint> waypoints;
	public Waypoint currentWP;

	private Waypoint initWp;
	private List<Waypoint> initWaypoints;

	// Use this for initialization
	public void Start () {

		base.Start();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

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
	void Update () {
		
		if (GameEventManager.gameOver != true)
		{
			pos = gameObject.transform.position;
			if (pos.x == target.x && pos.y == target.y)
			{
				followWaypoints();
			}
			gameObject.transform.position += new Vector3 ( speed * direction.x * Time.deltaTime, speed * direction.y* Time.deltaTime, 0f);
		}
	}
	
	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player"))
		{
			GameEventManager.TriggerGameOver(gameObject.name);
		}
	}
	
	public void followWaypoints()
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
		_soundList[0].playSound();
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
