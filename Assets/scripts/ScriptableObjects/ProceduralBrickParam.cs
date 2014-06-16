using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralBrickParam : ScriptableObject
{
	public bool forceTrigger;
	[Range (0, 100)] public int chanceToTrigger = 50;
	public LevelBrick.typeList Brick;
	[Range (0, 20)] public int ID = 1;
	[Range (0, 20)] public int stepID = 1;
	public string giveWPM = "A";
	public bool tryEnable = false;
	public bool tryDisable = false;
	public bool Toggle = true;
	public string changeDirections = "UDLR";
	[Range (0, 15)] public int addLength = 4;
	public int maxLength = 15;
	public bool tryInvert = false;
	public bool hasbeenInverted = false;

	public enum paramTypeList
	{
		EarlySetup,
		MiddleTrigger,
		LateRandom
	};
	public paramTypeList paramType;

	public bool isTriggered;
}
