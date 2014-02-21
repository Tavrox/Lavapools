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
	public float initSpeed;
	public LevelManager _levMan;
	public int brickId;

	private OTAnimatingSprite animSpr;
	private BoxCollider coll;
	public bool invisible = false;

	[HideInInspector] public Vector3 direction;
	[HideInInspector] public Vector3 target;
	[HideInInspector] public Vector3 pos;
	[HideInInspector] public Vector3 initPos;
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
		_bricksSpeed = LevelManager.TuningDocument._dicoBricks;

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
		initSpeed = speed;

		if (GetComponentInChildren<OTAnimatingSprite>() != null)
		{
			animSpr = GetComponentInChildren<OTAnimatingSprite>();
		}
	}

	public float getSpeed(LevelBrick _brick, Dictionary<LevelBrick.typeList, float> _dico)
	{
		float res = 0f;
		res = _dico[_brick.type];
		return res;
	}

	public void enableBrick()
	{
		float initspeed = getSpeed(this, _bricksSpeed);
//		speed = initspeed;
//		enabled = true;
		new OTTween(this, 0.5f).Tween("speed", initspeed );
//		new OTTween(animSpr, _levMan.TuningDocument.timeBeforeActivation).Tween("alpha", 1f );
//		StartCoroutine("triggerCollider");
	}

	public void disableBrick()
	{
		speed = 0;
//		animSpr.alpha = 0f;
//		invisible = true;
	}

	IEnumerator triggerCollider()
	{
		yield return new WaitForSeconds(LevelManager.TuningDocument.timeBeforeActivation);
		invisible = false;
	}

	private void GameStart()
	{
		speed = 0f;
		new OTTween(this, 0.5f).Tween("speed", initSpeed );
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
