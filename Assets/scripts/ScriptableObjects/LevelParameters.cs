using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelParameters : ScriptableObject 
{
	public GameSetup.LevelList NAME;
	public enum levelTypeList
	{
		Linear,
		Procedural,
		Maze,
		Debuggin,
		Vertical
	};
	public levelTypeList levelType;
	public int numberOfSteps;
	public float Player_Speed_low;
	public float Player_Speed_med;
	public float Player_Speed_high;
	public float Bird_Speed;
	public float Chainsaw_Speed;
	public float Arrow_Speed;
	public float Arrow_FireRate;
	public float Blade_RotationSpeed;
	public float Gem_SpawnRate;
	public float Carpet_Speed;
	[Range(1,100)] public int Gem_MinimumInLevel;
	[HideInInspector] public Dictionary<LevelBrick.typeList, float> _dicoBricks = new Dictionary<LevelBrick.typeList, float>();

	public LinearLevelSetup 		LinkedLinear;
//	public MazeLevelSetup		 	LinkedMaze;
	public ProceduralLevelSetup 	LinkedProcedural;
	public VerticalLevelSetup 		LinkedVertical;
	
	public void initScript () 
	{
		_dicoBricks.Add(LevelBrick.typeList.Bird, Bird_Speed);
		_dicoBricks.Add(LevelBrick.typeList.Chainsaw, Chainsaw_Speed);
		_dicoBricks.Add(LevelBrick.typeList.ArrowTower, Arrow_FireRate);
		_dicoBricks.Add(LevelBrick.typeList.BladeTower, Blade_RotationSpeed);
		_dicoBricks.Add(LevelBrick.typeList.Carpet, Carpet_Speed);
	}
}
