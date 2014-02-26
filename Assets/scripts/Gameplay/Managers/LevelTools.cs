using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTools : MonoBehaviour {

	public LevelManager _levMan;
	
	public void disableBrick (LevelBrick _brickToEnable)
	{
		
	}
	
	public void disableBrick (string _brickToEnable)
	{
		
	}

	public void enableBrick (LevelBrick _brickToEnable)
	{
		
	}

	public void enableBrick (string _brickToEnable)
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

	public void CollectObject(Collectible _thing)
	{
		_levMan.CollectibleGathered.Add(_thing);
		_levMan.collecSum += _thing.value;
		_levMan._player.triggerNotification(_thing.value);
	}

	public void UnlockLevel(LevelInfo _lvl)
	{

	}
}
