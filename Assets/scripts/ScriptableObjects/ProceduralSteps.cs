﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public int stepID;
//	public int priority;
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
	[HideInInspector] public float PlatformValueMultiplier = 1f;
	public List<string> BricksEnabled;
	public List<string> BricksDisabled;
	public List<string> BrickToRandomlySwapWPM;
	public List<string> WaypointsToInvert;
	public LevelInfo LevelToUnlock;
	[HideInInspector] public bool beenTriggered = false;

	public void Reset()
	{
		LevelToUnlock = null;
		BricksEnabled = null;
		BricksDisabled = null;
		BrickToRandomlySwapWPM = null;
		WaypointsToInvert = null;
		ScoreCondition = 0f;
		TimerCondition = 0f;
		SpeedMultiplier = 0f;
		Music_To_Play = MusicList.None;
		stepID = int.Parse(name);
	}
}
