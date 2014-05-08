using UnityEngine;
using System.Collections;

[System.Serializable]
public class BrickStepParam 
{
	[SerializeField] public LevelBrick.typeList Brick;
	[SerializeField] public int stepID = 1;
	[SerializeField] public int ID = 1;
	[SerializeField] public string WaypointsAttributed = "A";
	[SerializeField] public bool Enable = true;
	[SerializeField] public bool Disable = false;
	[SerializeField] public string Directions = "UDLR";
	[SerializeField] public int TowerLength = 4;
	[SerializeField] public bool Invert = false;
}
