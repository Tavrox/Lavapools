using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public Waypoint nextWP;
	public bool activated = true;
	
	private WaypointManager linkedManager;

	public void Setup()
	{
		linkedManager = transform.parent.GetComponent<WaypointManager>();
		name = transform.parent.name + "/ID/" + name;
		// nextWP = linkedManager.findNextWaypoint(this); SHOULD WORK DAFUCK.
	}

	void OnTriggerEnter(Collider _other)
	{
		if (activated && _other.GetComponent<PatrolBrick>() != null)
		{
			PatrolBrick _collBrick = _other.GetComponent<PatrolBrick>();
			if (_collBrick.type != LevelBrick.typeList.Fields)
			{
				if (_collBrick.debug)
				{
					Debug.Log("Touched/" + _collBrick.brickPathId +"/"+ linkedManager.id);
				}
				if (_collBrick.type == linkedManager.relatedBrick.type && _collBrick.brickPathId == linkedManager.id)
				{
					_collBrick.brickBounce();
					_collBrick.GoToWaypoint(linkedManager.findNextWaypoint(this));
				}
			}
		}
	}
}
