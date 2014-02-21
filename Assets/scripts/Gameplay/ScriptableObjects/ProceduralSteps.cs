using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralSteps : ScriptableObject {

	public int levelID;
	public enum Difficulty
	{
		Noobcrab,
		Crabbish,
		Hardcrabore,
		Crabmaster, 
		SuperHexacrab
	}
	public Difficulty levelLabel;
	public int stepID;
	public int priority;
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
	public float SpeedMultiplier = 1f;
	public float PlatformValueMultiplier = 1f;
	public List<string> BricksEnabled;
	public List<string> BricksDisabled;
	public List<string> WaypointsToInvert;
	public bool PlayMusic;
}
