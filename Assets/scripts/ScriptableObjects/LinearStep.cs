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
	public List<BrickStepParam> LinkedParam = new List<BrickStepParam>();

	// EDITOR TOOLS
	public LevelInfo LevelToUnlock;
	/* Foreach brick added, you must add in the [dictionnary , brick manager , typelist ]*/

	void Update()
	{
		stepID = int.Parse(name);
	}

	public void addParam()
	{

	}
}
