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
	[HideInInspector] public LPTuning TuningDocument;
	public Dictionary<LevelBrick.typeList, float> _bricksSpeed = new Dictionary<LevelBrick.typeList, float>();
	public int brickId;
	[HideInInspector] public Player _player;

	public void Start()
	{
		gameObject.name += brickId;

		_player = GameObject.Find("Player").GetComponent<Player>();
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		TuningDocument = _levMan.TuningDocument;
		_bricksSpeed = TuningDocument._dicoBricks;

		speed = getSpeed(this, _bricksSpeed);
	}

	public float getSpeed(LevelBrick _brick, Dictionary<LevelBrick.typeList, float> _dico)
	{
		float res = 0f;
		res = _dico[_brick.type];
		return res;
	}
}
