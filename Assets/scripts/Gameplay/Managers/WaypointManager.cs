using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour {
	
	[HideInInspector] public string id;
	public List<Waypoint> relatedWaypoints = new List<Waypoint>();
	[HideInInspector] public Waypoint lastWp;
	[HideInInspector] public LevelBrick relatedBrick;
	[HideInInspector] public LevelManager _levMan;

	public void Setup(LevelManager man)
	{
		if (relatedWaypoints.Count == 0)
		{
			relatedWaypoints = GetWpList();
		}
		lastWp = relatedWaypoints[relatedWaypoints.Count-1];
		id = gameObject.name.Split('/')[1];
		_levMan = man;
	}

	public Waypoint findNextWaypoint( Waypoint _wpSource)
	{
		Waypoint res = null;
		int currentWpIndex = relatedWaypoints.FindIndex(wp => wp == _wpSource);
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

	public List<Waypoint> GetWpList()
	{
		Waypoint[] _childWP = GetComponentsInChildren<Waypoint>();
		foreach (Waypoint wp in _childWP)
		{
			relatedWaypoints.Add(wp);
			wp.Setup();
		}
		relatedWaypoints.Sort(delegate (Waypoint x, Waypoint y)
		{
			if (x.id < y.id) return -1;
			if (x.id > y.id) return 1;
			else return 0;
		});
		return (relatedWaypoints);
	}

	public Waypoint pickRandomWP()
	{
		return relatedWaypoints[Random.Range(0,relatedWaypoints.Count)];
	}	
}
