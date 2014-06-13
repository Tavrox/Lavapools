using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaypointDirector : MonoBehaviour {


	private LevelManager _lm;
	public List<WaypointManager> waypointsMan = new List<WaypointManager>();

	// Use this for initialization
	public void Setup (LevelManager _levMan)
	{
		_lm = _levMan;
		WaypointManager[] waypointsManagers = GetComponentsInChildren<WaypointManager>();
		foreach (WaypointManager wpm in waypointsManagers)
		{
			waypointsMan.Add(wpm);
			wpm.Setup(_lm);
		}
	}

	public void affectRelatedBricks(List<LevelBrick> _listBricks)
	{
		foreach (WaypointManager mana in waypointsMan)
		{
			mana.relatedBrick = _listBricks.FindAll(delegate(LevelBrick obj) 
	        {
			if (obj.GetComponent<PatrolBrick>() != null && obj.GetComponent<PatrolBrick>().brickPath == mana)
			{
				return obj;
			}
				else
				{
					return false;
				}
			});
		}
	}
}
