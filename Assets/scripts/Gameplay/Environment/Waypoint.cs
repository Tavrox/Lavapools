using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public enum TypeList
	{
		Normal,
		Initial,
		Switchpoint
	};
	public TypeList WPType;
//	[HideInInspector]
	public int id;
//	[HideInInspector]s
	public bool activated = true;
//	[HideInInspector]
	public Waypoint nextWP;
	public WaypointManager linkedManager;

	public void Setup()
	{
		linkedManager = transform.parent.GetComponent<WaypointManager>();
		id = int.Parse(name);
	}
	public void lateSetup()
	{
		if (linkedManager.type != LevelBrick.typeList.Fields)
		{
			nextWP = linkedManager.findNextWaypoint(this);
		}
	}

	void OnTriggerEnter(Collider _other)
	{
		if (activated && _other.GetComponent<PatrolBrick>() != null)
		{
			PatrolBrick _collBrick = _other.GetComponent<PatrolBrick>();
//			print ("[" + _collBrick.type + " VS " + LevelBrick.typeList.Fields + "]");

			if (_collBrick.type != LevelBrick.typeList.Fields && linkedManager.relatedBrick != null)
			{
//				print ("[" + linkedManager.relatedBrick + " VS " + _collBrick + "]");
//				if (linkedManager.relatedBrick != _collBrick)
//				{
					if (_collBrick.type == linkedManager.relatedBrick.type && _collBrick.brickPathId == linkedManager.id)
					{
						_collBrick.GoToWaypoint(linkedManager.findNextWaypoint(this));
					}
//				}
			}
		}
	}
}
