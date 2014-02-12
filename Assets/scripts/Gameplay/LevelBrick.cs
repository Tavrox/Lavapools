using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBrick : MonoBehaviour {

	public enum typeList
	{
		Bird, 
		Fields,
		Chainsaw,
		ImmovableGround
	};
	public typeList type;
	public float speed;

	[HideInInspector] public Vector3 direction;
	[HideInInspector] public Vector3 target;
	[HideInInspector] public Vector3 pos;
	[HideInInspector] public Vector3 initPos;
	[HideInInspector] public LevelManager _levMan;
	public Dictionary<LevelBrick.typeList, float> _bricksSpeed = new Dictionary<LevelBrick.typeList, float>();
	[HideInInspector] public int brickId;
	[HideInInspector] public Player _player;

	public void Start()
	{
		if (gameObject.name.Contains("/"))
		{
			brickId = int.Parse(gameObject.name.Split('/')[1]);
			print (brickId);
		}

		if (GameObject.Find("Player").GetComponent<Player>() != null)
		{
			_player = GameObject.Find("Player").GetComponent<Player>();
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_bricksSpeed = _levMan.TuningDocument._dicoBricks;

		initPos = gameObject.transform.position;

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		speed = getSpeed(this, _bricksSpeed);
	}

	public float getSpeed(LevelBrick _brick, Dictionary<LevelBrick.typeList, float> _dico)
	{
		float res = 0f;
		res = _dico[_brick.type];
		return res;
	}

	private void GameStart()
	{
//		initPos = gameObject.transform.position;
//		enabled = true;
	}
	
	private void GameOver()
	{
//		enabled = false;
	}
	
	private void Respawn()
	{
		gameObject.transform.position = initPos;
//		print (initPos);
//		enabled = true;
	}
}
