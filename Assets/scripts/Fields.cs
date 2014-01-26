﻿using UnityEngine;
using System.Collections;

public class Fields : MonoBehaviour {

	private LevelManager _levMan;
	private Vector3 randomTarget = new Vector3(0f,0f,0f);
	private int capScore;
	public enum captureType
	{
		CapturePoint,
		None,
		Static
	};
	public captureType capPoint = captureType.None;
	public bool isCaptured;
	public bool countCaptured;
	private OTSprite spr;
	private bool isDestroying;
	public float speed = 0.015f;
	private Vector3 direction;
	private Vector3 target;
	private Vector3 pos;
	private Vector3 pierce;
	public Waypoint spawnWP;
	public Waypoint nextWP;
	private Player _player;


	// Use this for initialization
	void Start () {
		pos = this.gameObject.transform.position;
		spr = GetComponentInChildren<OTSprite>();
		if (GameObject.FindGameObjectWithTag("Player") != null)
		{
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		}
		if (spawnWP != null && nextWP != null)
		{
			target = nextWP.transform.position;
			direction = (target - pos).normalized;
			Destroy(gameObject, 8f);
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		//InvokeRepeating("changeTarget", 3f, 6f);
	}
	
	// Update is called once per frame
	void Update () {
		pos = gameObject.transform.position;
		pierce = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+10);
	
		Vector3 movingVec = new Vector3(0f,0f,0f);
	
		if (spr!=null && capPoint == captureType.CapturePoint && isCaptured == false)
		{
			spr.frameName = "uncaptured";
		}
		if (spr!=null && capPoint == captureType.None)
		{
			spr.frameName = "field";
		}
		if (spr!=null && capPoint == captureType.CapturePoint && isCaptured)
		{
			spr.frameName = "captured";
		}
		if (this.gameObject.transform.position != randomTarget)
		{
			gameObject.transform.position += new Vector3 ( speed * direction.x, speed * direction.y, 0f);
			//Debug.Log(direction);
			//Debug.Log(speed);
		}
		if (capScore == 80 && countCaptured == false)
		{
			isCaptured = true;
			countCaptured = true;
			_levMan.fieldsCaptured += 1;
			spr.alpha = 1f;
		}
	}
	
	public void changeTarget()
	{
		randomTarget = _levMan.pickRandomLoc().gameObject.transform.position;
		direction = (gameObject.transform.position - randomTarget).normalized;
	}
	
	public void destroyField()
	{
		new OTTween(spr, 3f).Tween("size",  new Vector2(0f,0f));
		Destroy(this.gameObject, 3f);
		_levMan.respawnField();
	}
	
	public void OnTriggerStay(Collider _other)
	{
		if (_other.CompareTag("Player") && capPoint == captureType.CapturePoint)
		{
			if (capScore < 100)
			{
				spr.frameName = "capturing";
				capScore += 1;			
			}
		}
	}
	public void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Rock"))
		{
			destroyField();
		}
		if (_other.CompareTag("Player"))
		{
			_player.OnPlatforms += 1;
		}
	}
	public void OnTriggerExit(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			_player.OnPlatforms -= 1;
		}
	}
}