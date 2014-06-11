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
	public GUIEditorSkin skin;
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
	public bool Triggered;

	// PROCEDURAL PARAMETERS
	public List<ProceduralBrickParam> LinkedParam = new List<ProceduralBrickParam>();
	[HideInInspector] public bool editorDisplayParam;
	public int triggerSum;
	public enum procTrigger
	{
		BrickByBrick,
		Mixed
	};
	public procTrigger procType;
	public int nbBricksToTrigger;
	public bool allowRetrigger = true;

	// EDITOR TOOLS
	public LevelInfo LevelToUnlock;

	public static void createNewLevel(string _path)
	{

	}
}
