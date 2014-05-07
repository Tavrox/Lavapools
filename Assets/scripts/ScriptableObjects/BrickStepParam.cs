using UnityEngine;
using System.Collections;

public class BrickStepParam : ScriptableObject 
{
	public LevelBrick.typeList Brick;
	public string WaypointsAttributed;
	public bool Enable;
	public bool Disable;
	public string Directions;
	public int TowerLength;
	public bool TowerSwapRot;
}
