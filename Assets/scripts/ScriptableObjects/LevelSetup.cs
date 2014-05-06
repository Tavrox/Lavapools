using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSetup : ScriptableObject 
{
	public GameSetup.LevelList NAME;
	public int numberOfSteps;
	public float Player_Speed_low;
	public float Player_Speed_med;
	public float Player_Speed_high;
	public float Bird_Speed;
	public float Chainsaw_Speed;
	public float Arrow_Speed;
	public float Arrow_FireRate;
	public float Fire_RotationSpeed;
	public float Gem_SpawnRate;
	[Range(1,10)] public int Gem_MinimumInLevel;
	public LevelSetup _lvlToCopy;
	public bool OblivionLevel;
	
	[HideInInspector] public float Fields_Speed;
	[HideInInspector] public float Fields_FrequencySpawn;
	[HideInInspector] public float Fields_DelaySpawn;
	[HideInInspector] public Dictionary<LevelBrick.typeList, float> _dicoBricks = new Dictionary<LevelBrick.typeList, float>();
	[HideInInspector] public List<ProceduralSteps> Procedural_Steps;

	// Use this for initialization
	public void initScript () {
		_dicoBricks.Add(LevelBrick.typeList.Bird, Bird_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Fields, Fields_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Chainsaw, Chainsaw_Speed);
		_dicoBricks.Add(LevelBrick.typeList.ArrowTower, Arrow_FireRate);
		_dicoBricks.Add(LevelBrick.typeList.FireTower, Fire_RotationSpeed);
	}

	public void CopySetup()
	{
		numberOfSteps = _lvlToCopy.numberOfSteps;
		Player_Speed_low = _lvlToCopy.Player_Speed_low;
		Player_Speed_med = _lvlToCopy.Player_Speed_med;
		Player_Speed_high = _lvlToCopy.Player_Speed_high;
		Bird_Speed = _lvlToCopy.Bird_Speed;
		Chainsaw_Speed = _lvlToCopy.Chainsaw_Speed;
		Fields_Speed = _lvlToCopy.Fields_Speed;
		Fields_FrequencySpawn = _lvlToCopy.Fields_FrequencySpawn;
		Fields_DelaySpawn = _lvlToCopy.Fields_DelaySpawn;
		Gem_SpawnRate = _lvlToCopy.Gem_SpawnRate;
	}
	public void ResetAllSteps()
	{
		string path = "Procedural/" + NAME + "/";
		for (int i = 1; i <= numberOfSteps ; i++)
		{
			ProceduralSteps _stp = Resources.Load( path + i) as ProceduralSteps;
			_stp.Reset();
			_stp.ScoreCondition = (_stp.stepID * 5) - 5;
		}
	}
}