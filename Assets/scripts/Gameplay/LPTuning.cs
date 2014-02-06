using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LPTuning : ScriptableObject {
	
	public float Player_Speed = 3f;
	public float Bird_Speed = 3f;
	public float Chainsaw_Speed = 3f;
	public float Fields_Speed = 3f;
	public float Immovable_Speed = 0f;

	public float CapturePoint_Score = 3f;
	public int Leaderboard_Number = 15;
	public Dictionary<LevelBrick.typeList, float> _dicoBricks = new Dictionary<LevelBrick.typeList, float>();


	// Use this for initialization
	public void initScript () {
		_dicoBricks.Add(LevelBrick.typeList.Bird, Bird_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Fields, Fields_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Chainsaw, Chainsaw_Speed);	
		_dicoBricks.Add(LevelBrick.typeList.ImmovableGround, Immovable_Speed);	
	}

	public float setupSpeed(LevelBrick _brick)
	{
		float res = 0f;
		res = _dicoBricks[_brick.type];
		return res;
	}
}
