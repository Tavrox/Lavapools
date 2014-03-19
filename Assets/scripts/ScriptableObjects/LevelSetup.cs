using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSetup : ScriptableObject 
{
	public int numberOfSteps;
	public float Player_Speed;
	public float Bird_Speed;
	public float Chainsaw_Speed;
	public float Fields_Speed;
	public float Fields_FrequencySpawn;
	public float Fields_DelaySpawn;
	public float Gem_SpawnRate;
	public Dictionary<LevelBrick.typeList, float> _dicoBricks = new Dictionary<LevelBrick.typeList, float>();

	// Use this for initialization
	public void initScript () {
		_dicoBricks.Add(LevelBrick.typeList.Bird, Bird_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Fields, Fields_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Chainsaw, Chainsaw_Speed);
	}
}