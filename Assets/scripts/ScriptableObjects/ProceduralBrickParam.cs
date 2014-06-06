using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralBrickParam : ScriptableObject
{
	[SerializeField] public LevelBrick.typeList Brick;
	[SerializeField] public int stepID = 1;
	[SerializeField] public int ID = 1;
	[SerializeField] public string giveWPM = "A";
	[SerializeField] public bool tryEnable = true;
	[SerializeField] public bool tryDisable = false;
	[SerializeField] public string changeDirections = "UDLR";
	[SerializeField] public int addLength = 4;
	[SerializeField] public bool tryInvert = false;
}
