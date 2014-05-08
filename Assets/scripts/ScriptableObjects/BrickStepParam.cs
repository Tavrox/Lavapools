using UnityEngine;
using System.Collections;

[System.Serializable]
public class BrickStepParam 
{
	public LevelBrick.typeList Brick;
	public int ID = 1;
	public string WaypointsAttributed = "A";
	public bool Enable = true;
	public bool Disable = false;
	public string Directions = "UDLR";
	public int TowerLength = 4;
	public bool TowerSwapRot = false;
}
