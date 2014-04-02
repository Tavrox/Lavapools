using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]

public class ProceduralSteps : ScriptableObject {

	public enum Difficulty
	{
		Noobcrab,
		Crabbish,
		Hardcrabore,
		Crabmaster, 
		SuperHexacrab
	}
	public Difficulty levelLabel;
	public enum MusicList
	{
		None,
		Noobcrab,
		Hardcrabore,
		SuperHexacrab
	};
	public MusicList Music_To_Play;
	public AudioClip MusicSource;
	public int stepID;
	public enum conditionEnum
	{
		Score,
		Trigger,
		Timer,
		Other
	};
	public conditionEnum condition;
	public float ScoreCondition;
	public float TimerCondition;
	public float Crab_SpeedMultiplier = 1f;
	public float Enemies_SpeedMultiplier = 1f;
	public List<string> BricksEnabled;
	public List<string> BricksDisabled;
	public List<string> WaypointsToInvert;
	public LevelInfo LevelToUnlock;

	public void Reset()
	{
		LevelToUnlock = null;
		BricksEnabled = null;
		BricksDisabled = null;
		WaypointsToInvert = null;
		ScoreCondition = 1000f;
		TimerCondition = 0f;
		Crab_SpeedMultiplier = 1f;
		Enemies_SpeedMultiplier = 1f;
		Music_To_Play = MusicList.None;
		stepID = int.Parse(name);
	}

	void Update()
	{
		stepID = int.Parse(name);
	}
}
