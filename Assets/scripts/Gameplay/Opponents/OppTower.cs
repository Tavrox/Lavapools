using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OppTower : LevelBrick {
	
	public List<LevelTools.DirectionList> enabledDirection;

	public void Setup()
	{
		base.Setup();

	}

	public List<LevelTools.DirectionList> setupDirectionList(string _grp)
	{
		enabledDirection.Clear();
		if (_grp.Contains("U"))
		{
			enabledDirection.Add(LevelTools.DirectionList.Up);
		}
		if (_grp.Contains("D"))
		{
			enabledDirection.Add(LevelTools.DirectionList.Down);
		}
		if (_grp.Contains("L"))
		{
			enabledDirection.Add(LevelTools.DirectionList.Left);
		}
		if (_grp.Contains("R"))
		{
			enabledDirection.Add(LevelTools.DirectionList.Right);
		}
		return enabledDirection;
	}
	
	public List<LevelTools.DirectionList> changeDirection(List<LevelTools.DirectionList> _dirList)
	{
		enabledDirection.Clear();
		enabledDirection = _dirList;
		return enabledDirection; // Return result
	}
}
