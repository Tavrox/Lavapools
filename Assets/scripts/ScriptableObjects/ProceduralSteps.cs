using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]

public class ProceduralSteps : ScriptableObject {

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
	public enum BrickStack
	{
		Bird_1,
		Bird_2,
		Bird_3,
		Bird_4,
		Bird_5,
		Bird_6,
		Bird_7,
		Bird_8,
		Chainsaw_1,
		Chainsaw_2,
		Chainsaw_3,
		Chainsaw_4,
		Chainsaw_5,
		Chainsaw_6,
		Chainsaw_7,
		Chainsaw_8,
		ArrowTower_1,
		ArrowTower_2,
		ArrowTower_3,
		ArrowTower_4,
		ArrowTower_5,
		ArrowTower_6,
		ArrowTower_7,
		ArrowTower_8,
		FireTower_1,
		FireTower_2,
		FireTower_3,
		FireTower_4,
		FireTower_5,
		FireTower_6,
		FireTower_7,
		FireTower_8,

	};
	public enum PathStack
	{
		Bird_A,
		Bird_B,
		Bird_C,
		Bird_D,
		Bird_E,
		Bird_F,
		Bird_G,
		Bird_H,
		Chainsaw_A,
		Chainsaw_B,
		Chainsaw_C,
		Chainsaw_D,
		Chainsaw_E,
		Chainsaw_F,
		Chainsaw_G,
		Chainsaw_H
	};
	public conditionEnum condition;
	public float ScoreCondition;
	public float TimerCondition;
	public float Crab_SpeedMultiplier = 1f;
	public float Enemies_SpeedMultiplier = 1f;
	public List<BrickStack> BricksEnabled;
	public List<BrickStack> BricksDisabled;
	public List<ProceduralSteps.PathStack> WaypointsToInvert;
	// ARROW TOWERS
	public List<BrickStack> ArrowTowerSetup;
	public List<string> ArrowTowerDirections;
	// FIRE TOWERS
	public List<BrickStack> FireTowerSetup;
	public List<int> FireTowerLength;
	public List<string> FireTowerDirection;
	public List<bool> FireTowerSwapRot;
	// EDITOR TOOLS
	public LevelInfo LevelToUnlock;

	/* Foreach brick added, you must add in the [dictionnary , brick manager , typelist ]*/

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
		stepID = int.Parse(name);
	}

	void Update()
	{
		stepID = int.Parse(name);
	}
}
