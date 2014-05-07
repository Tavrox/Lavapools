using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]

public class LinearStep : ScriptableObject {

	public enum Difficulty
	{
		Crabbish,
		CuteCrab,
		CasualCrab,
		NinjaCrab,
		SuperCrab,
		LordCrab,
		Crabmaster,
		JediCrab,
		CrabKing,
		MegaCrab,
		Hardcrabore,
		HolyCrab,
		Crabocalypse,
		Crabbageddon,
		SuperHexacrab,
		CrabLike
	}
	public Difficulty levelLabel;
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

	public List<BrickStepParam> ListParams = new List<BrickStepParam>();
	public int NumberBricksModifier;

	/*
	public List<BrickStack> BricksEnabled;
	public List<BrickStack> BricksDisabled;
	public List<LinearStep.PathStack> WaypointsToInvert;
	// ARROW TOWERS
	public List<BrickStack> ArrowTowerSetup;
	public List<string> ArrowTowerDirections;
	// FIRE TOWERS
	public List<BrickStack> FireTowerSetup;
	public List<int> FireTowerLength;
	public List<string> FireTowerDirection;
	public List<bool> FireTowerSwapRot;
	*/

	// EDITOR TOOLS
	public LevelInfo LevelToUnlock;

	/* Foreach brick added, you must add in the [dictionnary , brick manager , typelist ]*/

	public void Reset()
	{
//		LevelToUnlock = null;
//		BricksEnabled = null;
//		BricksDisabled = null;
//		WaypointsToInvert = null;
//		ScoreCondition = 1000f;
//		TimerCondition = 0f;
//		Crab_SpeedMultiplier = 1f;
//		Enemies_SpeedMultiplier = 1f;
//		stepID = int.Parse(name);
	}

	void Update()
	{
		stepID = int.Parse(name);
	}
}
