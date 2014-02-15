using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour {

	public List<Waypoint> relatedWaypoints = new List<Waypoint>();
	public Waypoint lastWp;
	private LevelBrick.typeList typeOfBrick;

	public void Start()
	{
		Waypoint[] _childWP = GetComponentsInChildren<Waypoint>();
		foreach (Waypoint wp in _childWP)
		{
			relatedWaypoints.Add(wp);
		}
		lastWp = relatedWaypoints[relatedWaypoints.Count-1];
		typeOfBrick = lastWp.BrickType;

	}

	public Waypoint findNextWaypoint( Waypoint _wpSource)
	{
		Waypoint res = null;
		int currentWpIndex = relatedWaypoints.FindIndex(wp => wp == _wpSource) ;
		if (currentWpIndex + 1 < relatedWaypoints.Count)
		{
			res = relatedWaypoints[currentWpIndex+1];
		}
		else
		{
			res = relatedWaypoints[0];
		}
		return res;
	}

	public void invertWaypoints()
	{


	}
	
}
