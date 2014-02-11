﻿using UnityEngine;
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


		initWp = currentWP;
		initWaypoints = waypoints;

		setupTarget();
//		followWaypoints();
	}

	private void setupTarget()
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
		if (currentWP != null && pos != null)
		{
			currentWP = currentWP.nextWP;
			target = currentWP.nextWP.transform.position;
			direction = (target - pos).normalized;
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
		gameObject.transform.position = initPos;
		currentWP = initWp;
		initWaypoints = waypoints;
		setupTarget();
		print ("resp");
	}
}
