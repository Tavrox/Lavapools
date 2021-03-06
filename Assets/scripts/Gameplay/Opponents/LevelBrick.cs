﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBrick : MonoBehaviour {

	public enum typeList
	{
		Bird, 
		Chainsaw,
		ArrowTower,
		BladeTower,
		Carpet
	};
	public typeList type;
	public float speed;
	[HideInInspector] public float initSpeed;
	[HideInInspector] public LevelManager _levMan;
	public int brickId;
	[HideInInspector] public Vector3 direction;
	[HideInInspector] public Vector3 target;
	[HideInInspector] public Vector3 pos;
	[HideInInspector] public Vector3 initPos;
	[HideInInspector] public Player _player;
	public bool isEnabled = false;
	[HideInInspector] public List<FESound> _soundList = new List<FESound>();
	
	[HideInInspector] public OTAnimatingSprite animSpr;
	[HideInInspector] public Dictionary<LevelBrick.typeList, float> _bricksSpeed = new Dictionary<LevelBrick.typeList, float>();

	public void Setup()
	{
		if (gameObject.name.Contains("_"))
		{
			brickId = int.Parse(gameObject.name.Split('_')[1]);
		}
		if (GameObject.Find("Player") != null)
		{
			_player = GameObject.Find("Player").GetComponent<Player>();
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_bricksSpeed = LevelManager.LocalTuning._dicoBricks;

		initPos = gameObject.transform.position;

		if (GetComponentsInChildren<FESound>() != null)
		{
			FESound[] _arraySounds = GetComponentsInChildren<FESound>();
			foreach (FESound _sd in _arraySounds)
			{
				_soundList.Add(_sd);
			}
		}

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GameEventManager.EndGame += EndGame;


		speed = getSpeed(this, _bricksSpeed);
		initSpeed = speed;

		if (GetComponentInChildren<OTAnimatingSprite>() != null)
		{
			animSpr = GetComponentInChildren<OTAnimatingSprite>();
		}
	}

	public float getSpeed(LevelBrick _brick, Dictionary<LevelBrick.typeList, float> _dico)
	{
		float res = 0f;
		if (speed == 0)
		{
			if (_dico.ContainsKey(_brick.type) == false)
			{
				Debug.LogError("Couldn't find " + type.ToString() + " speed");
				Debug.Break();
			}
			res = _dico[_brick.type];
			if (res == 0)
			{
				Debug.Log("Might be an error with" + type.ToString() + "speed");
			}
		}
		else
		{
			res = speed;
		}
		return res;
	}

	virtual public void enableBrick()
	{
		float initspeed = getSpeed(this, _bricksSpeed);
		new OTTween(this, 0.5f).Tween("speed", initspeed );
		if (animSpr != null)
		{
			animSpr.Play();
		}
		isEnabled = true;
	}

	virtual public void disableBrick()
	{
		isEnabled = false;
	}

	private void GameStart()
	{
		if (this != null)
		{
			if (animSpr != null)
			{
				animSpr.Play();
			}
			new OTTween(this, 0.5f).Tween("speed", initSpeed );
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{

		}
	}

	private void EndGame()
	{
		if (this != null)
		{
			isEnabled = false;
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			gameObject.transform.position = initPos;
			disableBrick();
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (gameObject.name.Contains("_"))
		{
			brickId = int.Parse(gameObject.name.Split('_')[1]);
		}
	}
}
