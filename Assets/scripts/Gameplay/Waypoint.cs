using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public Waypoint nextWP;
	public LevelBrick.typeList BrickType;
	public bool activated = true;
	private string OwnerID;
	private WaypointManager linkedManager;
	public bool logTimeCode;

	void Start()
	{
		OwnerID = transform.parent.gameObject.name.Split('/')[1];
		linkedManager = transform.parent.GetComponent<WaypointManager>();
		name = transform.parent.name + "/ID/" + name;
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (activated)
		{
			if (_other.GetComponent<PatrolBrick>() != null)
			{
				if (_other.GetComponent<LevelBrick>().type == BrickType)
				{
					_other.GetComponent<PatrolBrick>().GoToWaypoint(linkedManager.findNextWaypoint(this));
					_other.GetComponent<PatrolBrick>().brickBounce();
					if (_other.GetComponent<LevelBrick>().type == LevelBrick.typeList.Fields)
					{
	//					Destroy(_other.gameObject);
					}
					if (logTimeCode)
					{
//						Debug.Log (_other.name +"||||||"+ GameObject.Find("LevelManager").GetComponent<LevelManager>().centSecondsElapsed);
					}
				}
			}
		}
	}
}
