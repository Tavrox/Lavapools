﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public InputManager InputMan;
	public int OnPlatforms;
	
	public playerState _state;
	[HideInInspector] public float lowSpeed;
	[HideInInspector] public float medSpeed;
	[HideInInspector] public float highSpeed;
	[HideInInspector] public float initLowSpeed;
	[HideInInspector] public float initMedSpeed;
	[HideInInspector] public float initHighSpeed;
	[HideInInspector] public float currSpeed;

	public enum speedList
	{
		Low,
		Med,
		High
	};
	[HideInInspector] public speedList currSp;
	private RaycastHit hit;
	private Vector3 pos;
	private int layer;
	private int layerMask;
	private Bounds rect;
	private Vector3 startPos;
	private Vector3 friction;
	private Vector3 playerSteps;
	private Vector3 mod = new Vector3(0f,0f,0f);
	private OTSprite spr;
	private Vector2 originalSize;
	private Notification _notif;
	private PlayerAnims _anims;
	private float speedStack = 0f;

	[HideInInspector] public enum playerState
	{
		Alive,
		Dead
	};

	private UserLeaderboard _playerSheet;

	[HideInInspector] public string playerName;
	
	private RaycastHit hitInfo; //infos de collision
	private Ray detectTargetLeft, detectTargetRight; //point de départ, direction
	
	// Use this for initialization
	public void Setup () {
		
		if (GetComponentInChildren<OTSprite>() != null)
		{
			spr = GetComponentInChildren<OTSprite>();
			spr.alpha = 1f;
			originalSize = spr.size;
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_playerSheet = ScriptableObject.CreateInstance("UserLeaderboard") as UserLeaderboard;
		_playerSheet.name = playerName;

		lowSpeed = LevelManager.LocalTuning.Player_Speed_low;
		medSpeed = LevelManager.LocalTuning.Player_Speed_med;
		highSpeed = LevelManager.LocalTuning.Player_Speed_high;
		initLowSpeed = lowSpeed;
		initMedSpeed = medSpeed;
		initHighSpeed = highSpeed;
		playerSteps = LevelManager.GlobTuning.PlayerSteps;
		InputMan = Resources.Load("Tuning/InputManager") as InputManager;
		 
		_anims = gameObject.AddComponent<PlayerAnims>() as PlayerAnims;
		_anims.Setup();
		_notif = GetComponentInChildren<Notification>();
		_notif.color = Color.white;

		startPos = gameObject.transform.position;
		friction.x = LevelManager.GlobTuning.Player_Friction.x;
		friction.y = LevelManager.GlobTuning.Player_Friction.y;

		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GameEventManager.EndGame += EndGame;
	}

	void Update () 
	{	
		if (_state == playerState.Alive)
		{
			pos = gameObject.transform.position;
			if (LevelManager.GAMESTATE == GameEventManager.GameState.Live )
			{
				if (OnPlatforms <= 0)
				{
					_levMan.tools.tryDeath(LevelTools.KillerList.Lava);
				}
				moveInput();
				mod.x *= friction.x;
				mod.y *= friction.y;
				this.gameObject.transform.position += mod * Time.deltaTime;
			}
		}
	}

	public void lootStack(int _stk)
	{
		triggerNotification(_stk * 1f);
	}


	private void moveInput()
	{
		if (this != null)
		{
			if (speedStack > playerSteps.x && speedStack < playerSteps.y )
			{
				currSpeed = lowSpeed;
				_anims._CURR = _anims._WALK;
			}
			if (speedStack > playerSteps.y  && speedStack < playerSteps.z)
			{
				currSpeed = medSpeed;
				_anims._CURR = _anims._WALK;
			}
			if (speedStack > playerSteps.z)
			{
				currSpeed = highSpeed;
				_anims._CURR = _anims._MAXWALK;
			}

			if (Input.GetKey (InputMan.KeyRight) || Input.GetAxisRaw("X axis") < (InputMan.BigAxis * -1) || Input.GetAxisRaw("6th axis") < (InputMan.SmallAxis * -1)) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.x += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetKeyUp (InputMan.KeyRight)) 
			{
				StartCoroutine("StartCheckReset");
				mod.x = 10f;
				_anims.playAnimation(_anims._STATIC);
			}
			
			if (Input.GetKey (InputMan.KeyUp) || Input.GetAxisRaw("Y axis") > InputMan.BigAxis  || Input.GetAxisRaw("7th axis") > InputMan.SmallAxis) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.y += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetKeyUp (InputMan.KeyUp)) 
			{
				StartCoroutine("StartCheckReset");
				mod.y = 10f;
				_anims.playAnimation(_anims._STATIC);
			}

			if (Input.GetKey (InputMan.KeyLeft) || Input.GetAxisRaw("X axis") > InputMan.BigAxis  || Input.GetAxisRaw("6th axis") > InputMan.SmallAxis ) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				mod.x -= currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetKeyUp (InputMan.KeyLeft)) 
			{
				StartCoroutine("StartCheckReset");
				mod.x = -10f;
				_anims.playAnimation(_anims._STATIC);
			}
			
			if (Input.GetKey (InputMan.KeyDown) || Input.GetAxisRaw("Y axis") < (InputMan.BigAxis * -1)  || Input.GetAxisRaw("7th axis") < (InputMan.SmallAxis * -1)  ) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				mod.y -= currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetKeyUp (InputMan.KeyDown)) 
			{
				StartCoroutine("StartCheckReset");
				mod.y = -10f;
				_anims.playAnimation(_anims._STATIC);
			}
		}
	}

	private bool checkDeadZone(float _input)
	{
		if (_input < 0.1f && _input > -0.1f)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	IEnumerator StartCheckReset()
	{
		yield return new WaitForSeconds(LevelManager.GlobTuning.PlayerSpeedReset);
		speedStack = 0f;
	}

	public void triggerNotification(float _value)
	{
		_notif.text = "+" + _value.ToString();
//		_notif.makeFadeIn();
//		StartCoroutine(WaitFadeSec(2f));
	}

	IEnumerator WaitFadeSec(float _time)
	{
		yield return new WaitForSeconds(_time);
		_notif.makeFadeOut();
	}

	private void GameStart()
	{
		if (this != null)
		{
			_state = playerState.Alive;
			_anims.playAnimation(_anims._STATIC);
			lowSpeed = initLowSpeed;
			medSpeed = initMedSpeed;
			highSpeed = initHighSpeed;
			currSpeed = initLowSpeed;
			speedStack = 0f;
			new OTTween(spr, 0.5f).Tween("alpha", 1f);
			new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
			_notif.makeFadeOut();
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			_state = playerState.Dead;
			_anims.playAnimation(_anims._STATIC);
			lowSpeed = initLowSpeed;
			medSpeed = initMedSpeed;
			highSpeed = initHighSpeed;
			currSpeed = initLowSpeed;
			speedStack = 0f;
			new OTTween(spr, 0.5f).Tween("alpha", 0f);
			new OTTween(spr, 0.5f).Tween("size", new Vector2(0.25f,0.25f));
			_notif.makeFadeOut();
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			_state = playerState.Alive;
			_anims.playAnimation(_anims._STATIC);
			lowSpeed = initLowSpeed;
			medSpeed = initMedSpeed;
			highSpeed = initHighSpeed;
			currSpeed = initLowSpeed;
			speedStack = 0f;
			gameObject.transform.position = startPos;
			new OTTween(spr, 0.5f).Tween("alpha", 1f);
			new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
			_notif.makeFadeOut();
		}
	}

	private void EndGame()
	{
//		transform.position = new Vector3(transform.position.x, transform.position.y, 200f);
	}
}
