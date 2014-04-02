using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelBrick : MonoBehaviour {

	public enum typeList
	{
		Bird, 
		Fields,
		Chainsaw,
	};
	public typeList type;
	[HideInInspector] public float speed;
	[HideInInspector] public float initSpeed;
	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public int brickId;
	[HideInInspector] public Vector3 direction;
	[HideInInspector] public Vector3 target;
	[HideInInspector] public Vector3 pos;
	[HideInInspector] public Vector3 initPos;
	[HideInInspector] public Player _player;
	[HideInInspector] public bool isEnabled = false;
	[HideInInspector] public List<FESound> _soundList = new List<FESound>();
	
	private OTAnimatingSprite animSpr;
	[HideInInspector] public Dictionary<LevelBrick.typeList, float> _bricksSpeed = new Dictionary<LevelBrick.typeList, float>();

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
		disableBrick();
	}

	public float getSpeed(LevelBrick _brick, Dictionary<LevelBrick.typeList, float> _dico)
	{
		float res = 0f;
//		print (_brick);
		res = _dico[_brick.type];
		return res;
	}

	public void enableBrick()
	{
		float initspeed = getSpeed(this, _bricksSpeed);
		new OTTween(this, 0.5f).Tween("speed", initspeed );
		isEnabled = true;
	}

	public void disableBrick()
	{
		isEnabled = false;
		speed = 0;
	}

	private void GameStart()
	{
		if (this != null)
		{
			disableBrick();
			new OTTween(this, 0.5f).Tween("speed", initSpeed );
		}
	}
	
	private void GameOver()
	{

	}

	private void EndGame()
	{
		isEnabled = false;
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			disableBrick();
			if (type != typeList.Fields)
			{
				gameObject.transform.position = initPos;
			}
		}
	}
}
