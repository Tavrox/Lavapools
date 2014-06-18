using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointManager : MonoBehaviour {
	
	[HideInInspector] public LevelManager _levMan;

	public string id;
	public List<LevelBrick> relatedBrick = new List<LevelBrick>();
	public List<Waypoint> relatedWaypoints = new List<Waypoint>();
	[HideInInspector] public Waypoint lastWp;
	public bool inverted = false;
	[HideInInspector] public bool debug = false;
	public bool hasBreakpoint = false;

	public void Setup(LevelManager man)
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		relatedWaypoints = GetWpList();
		id = gameObject.name.Split('_')[1];
		_levMan = man;

		if (hasBreakpoint == false)
		{
			Debug.LogWarning("This waypoint manager had no breakpoint ! Random attribution.");
			Waypoint wp = relatedWaypoints[Random.Range(0, relatedWaypoints.Count)];
			wp.WPType = Waypoint.TypeList.Breakpoint;
		}
	}

	public Waypoint findNextWaypoint( Waypoint _wpSource)
	{
		Waypoint res = null;
		lastWp = relatedWaypoints[relatedWaypoints.Count-1];
		if (_wpSource.id != lastWp.id)
		{
			int nextWp = relatedWaypoints.FindIndex(wp => wp.id == _wpSource.id);
			res = relatedWaypoints[nextWp+1];
		}
		else
		{
			res = relatedWaypoints[0];
		}
		if (res == null)
		{
			Debug.LogError("A point hasn't been found");
		}
		return res;
	}
	public Waypoint findPreviousWaypoints(Waypoint _wpSource)
	{
		Waypoint res = null;
		if (_wpSource.id != relatedWaypoints[0].id)
		{

			res = relatedWaypoints.Find(wp => wp.id == _wpSource.id-1);
		}
		else
		{
			res = lastWp;
		}
		if (res == null)
		{
			Debug.LogError("A point hasn't been found");
		}
		return res;
	}

	public void sortDescendant()
	{
		inverted = true;
		relatedWaypoints.Sort(delegate (Waypoint x, Waypoint y)
		{
			if (x.id > y.id) return -1;
			if (x.id < y.id) return 1;
			else return 0;
		});
		foreach (Waypoint wp in relatedWaypoints)
		{
			wp.lateSetup();
		}
		if (relatedBrick != null)
		{
//			relatedBrick.GetComponent<PatrolBrick>().setupTarget();
		}
//		Debug.Log ("The waypoints of " + gameObject.name + "have been inverted");
	}

	public void sortAscendant()
	{
		inverted = false;
		relatedWaypoints.Sort(delegate (Waypoint x, Waypoint y)
		                      {
			if (x.id < y.id) return -1;
			if (x.id > y.id) return 1;
			else return 0;
		});
		foreach (Waypoint wp in relatedWaypoints)
		{
			wp.lateSetup();
		}
//		Debug.Log ("The waypoints of " + gameObject.name + "have been inverted");
	}

	public List<Waypoint> GetWpList()
	{
		relatedWaypoints.Clear();
		Waypoint[] _childWP = GetComponentsInChildren<Waypoint>();
		foreach (Waypoint wp in _childWP)
		{
			relatedWaypoints.Add(wp);
			wp.Setup();
		}
		lastWp = relatedWaypoints[relatedWaypoints.Count-1];
		sortAscendant();
		return (relatedWaypoints);
	}

	public bool invertWaypoints()
	{
//		relatedBrick.GetComponent<PatrolBrick>().set

		List<LevelBrick> brk = relatedBrick.FindAll((LevelBrick obj) => obj.type == LevelBrick.typeList.Chainsaw);
		if (brk.Count > 0)
		{
			foreach (LevelBrick brick in brk)
			{
				brick.GetComponent<Chainsaw>().launchInvertAnim();
			}
		}

		
		if (inverted == true)
		{
			Debug.Log("InvertDESC" + gameObject.name);
			sortAscendant();
		}
		else if (inverted == false)
		{
			Debug.Log("InvertASC" + gameObject.name);
			sortDescendant();
		}
		return inverted;
	}

	private LevelBrick.typeList parseWpmName(string _st)
	{
		if (_st == "Chainsaw")
		{
			return LevelBrick.typeList.Chainsaw;
		}
		else if (_st == "Bird")
		{
			return LevelBrick.typeList.Bird;
		}
		else if (_st == "ArrowTower")
		{
			return LevelBrick.typeList.ArrowTower;
			
		}
		else if (_st == "BladeTower")
		{
			return LevelBrick.typeList.BladeTower;
		}
		else
		{
			return LevelBrick.typeList.Carpet;
		}
	}

	public Waypoint pickRandomWP()
	{
		return relatedWaypoints[Random.Range(0,relatedWaypoints.Count)];
	}	

	void OnDrawGizmosSelected()
	{
		GetWpList();
		id = gameObject.name.Split('_')[1];
	}
}
