using UnityEngine;
using System.Collections;

[System.Serializable]
public class BrickStepParam 
{
	[SerializeField] public LevelBrick.typeList Brick;
	[SerializeField] public int ID;
	[SerializeField] public string WaypointsAttributed;
	[SerializeField] public bool Enable;
	[SerializeField] public bool Disable;
	[SerializeField] public string Directions;
	[SerializeField] public int TowerLength;
	[SerializeField] public bool TowerSwapRot;
}
