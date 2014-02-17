using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTools : MonoBehaviour {

	public LevelManager _levMan;
	
	public void disableBrick (LevelBrick _brickToEnable)
	{
		
	}
	
	public void disableBirck (LevelBrick.typeList _type)
	{
		
	}
	
	public void disableBrick (List<LevelBrick> _brickList)
	{
		
	}
	
	public void enableTypeOfBrick(LevelBrick.typeList _type)
	{
		
	}
	
	public void enableBrick (LevelBrick _brickToEnable)
	{
		
	}

	public void fetchWaypoints(LevelBrick.typeList _type)
	{

	}

	public WaypointManager findWpManager(LevelBrick.typeList _type)
	{
		WaypointManager res = null;
		res = _levMan.waypointsMan.Find( delegate(WaypointManager obj) 
		{
			if (_type == LevelBrick.typeList.Fields)
			{
				return obj.GetComponent<FieldManager>() == true;
			}
			else
			{
				return obj.relatedBrick.type == _type;
			}
		});
		return res;
	}
}
