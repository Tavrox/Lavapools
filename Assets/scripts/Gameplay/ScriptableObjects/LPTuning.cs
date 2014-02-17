using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LPTuning : ScriptableObject {

	public float GLOBAL_speed = 0.0125f;
	
	public float Player_Speed = 3f;
	public float Bird_Speed = 3f;
	public float Chainsaw_Speed = 3f;
	public float Fields_Speed = 3f;
	public float Immovable_Speed = 0f;
	
	public float CapturePoint_Score = 600f;
	public float CaptureSpeed = 1f;
	public float ScoreOverTime = 5f;
	public float SpawnFrequency = 5f;
	public float DelayBeforeSpawn = 5f;
	public int Leaderboard_Number = 15;
	public Dictionary<LevelBrick.typeList, float> _dicoBricks = new Dictionary<LevelBrick.typeList, float>();

	public Color ColPlayer;
	public Color ColRank;
	public Color ColScore;

	// Use this for initialization
	public void initScript () {
		_dicoBricks.Add(LevelBrick.typeList.Bird, Bird_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Fields, Fields_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Chainsaw, Chainsaw_Speed);	
		_dicoBricks.Add(LevelBrick.typeList.ImmovableGround, Immovable_Speed);

		ColPlayer = Color.cyan;
		ColRank = Color.white;
		ColScore = Color.yellow;
	}

	public float setupSpeed(LevelBrick _brick)
	{
		float res = 0f;
		res = _dicoBricks[_brick.type];
		return res;
	}
}
