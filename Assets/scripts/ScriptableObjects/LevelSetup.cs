using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelSetup : ScriptableObject 
{
	public GameSetup.LevelList NAME;
	public int numberOfSteps;
	public float Player_Speed;
	public float Bird_Speed;
	public float Chainsaw_Speed;
	public float Gem_SpawnRate;
	public float Gem_MinimumInLevel;
	public LevelSetup _lvlToCopy;
	
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
	}

	public void CopySetup()
	{
		numberOfSteps = _lvlToCopy.numberOfSteps;
		Player_Speed = _lvlToCopy.Player_Speed;
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
		}
	}
}