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
	private string OwnerID;

	void Start()
	{
		GameObject.Find("LevelManager/LevelBricks/Bricks" + BrickType.ToString()+ OwnerID);
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.GetComponent<PatrolBrick>() != null)
		{
			if (_other.GetComponent<LevelBrick>().type == BrickType)
			{
				_other.GetComponent<PatrolBrick>().followWaypoints();
				_other.GetComponent<PatrolBrick>().brickBounce();
				if (_other.GetComponent<LevelBrick>().type == LevelBrick.typeList.Fields)
				{
//					Destroy(_other.gameObject);
				}
			}
		}
	}
}
