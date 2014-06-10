using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralBrickParam : ScriptableObject
{
	public bool forceTrigger;
	[Range (0f, 100f)] public int chanceToTrigger;
	public LevelBrick Brick;
	public int stepID = 1;
	public WaypointManager giveWPM;
	public bool tryEnable = true;
	public bool tryDisable = false;
	public bool Toggle;
	public string changeDirections = "UDLR";
	public int addLength = 4;
	public int maxLength;
	public bool tryInvert = false;
	public bool hasbeenInverted = false;

	public bool isTriggered;
}
