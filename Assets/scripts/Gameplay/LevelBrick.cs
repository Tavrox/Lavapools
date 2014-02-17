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
	public LevelManager _levMan;
	public int brickId;
	[HideInInspector] public Player _player;
	[HideInInspector] public List<FESound> _soundList = new List<FESound>();

	public Dictionary<LevelBrick.typeList, float> _bricksSpeed = new Dictionary<LevelBrick.typeList, float>();

	public void Setup()
	{
		if (gameObject.name.Contains("/"))
		{
			brickId = int.Parse(gameObject.name.Split('/')[1]);
		}
		if (GameObject.Find("Player") != null)
		{
			_player = GameObject.Find("Player").GetComponent<Player>();
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_bricksSpeed = _levMan.TuningDocument._dicoBricks;

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

		speed = getSpeed(this, _bricksSpeed);
	}

	public float getSpeed(LevelBrick _brick, Dictionary<LevelBrick.typeList, float> _dico)
	{
		float res = 0f;
		res = _dico[_brick.type];
		return res;
	}

	public void enableBrick()
	{
		Debug.Log("Enabled "+ gameObject.name);
		float initspeed = getSpeed(this, _bricksSpeed);
		new OTTween(this, 0.5f).Tween("speed", initspeed );
//		enabled = true;
	}

	public void disableBrick()
	{
		Debug.Log("Disabled "+ gameObject.name);
		new OTTween(this, 0.5f).Tween("speed", 0f);
//		enabled = false;
	}

	private void GameStart()
	{

	}
	
	private void GameOver()
	{

	}
	
	private void Respawn()
	{
		if (type != typeList.Fields)
		{
			gameObject.transform.position = initPos;
		}
	}
}
