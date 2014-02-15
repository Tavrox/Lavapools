using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralSteps : ScriptableObject {

	public int levelID;
	public enum Difficulty
	{
		Noobcrab,
		Crabbish,
		Crabstructor,
		Hardcrabore
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
	public List<int> BricksToEnable;
	public List<int> BricksToDisable;
}
