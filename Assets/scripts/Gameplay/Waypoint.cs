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
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.GetComponent<PatrolBrick>() != null)
		{
			_other.GetComponent<PatrolBrick>().followWaypoints();
		}
	}
}
