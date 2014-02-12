using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public enum wpTypeList
	{
		DeadEnd,
		Crossroad,
		Loop
	}
	public wpTypeList wpType;
	public Waypoint nextWP;
	public LevelBrick.typeList BrickType;
//	public LevelBrick.type BrickType;
	public string OwnerID;

	void Start()
	{
		GameObject.Find("LevelManager/LevelBricks/Bricks" + BrickType.ToString()+ OwnerID);
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.GetComponent<PatrolBrick>() != null)
		{
			_other.GetComponent<PatrolBrick>().followWaypoints();
		}
	}
}
