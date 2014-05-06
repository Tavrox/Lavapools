using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OppTower : LevelBrick {
	
	public List<LevelTools.DirectionList> enabledDirection;
	public OTSprite dirUp;
	public OTSprite dirLeft;
	public OTSprite dirDown;
	public OTSprite dirRight;

	public void Setup()
	{
		base.Setup();
		
		dirUp = FETool.findWithinChildren(gameObject, "Direction/up").GetComponentInChildren<OTSprite>();
		dirLeft = FETool.findWithinChildren(gameObject, "Direction/left").GetComponentInChildren<OTSprite>();
		dirDown = FETool.findWithinChildren(gameObject, "Direction/down").GetComponentInChildren<OTSprite>();
		dirRight = FETool.findWithinChildren(gameObject, "Direction/right").GetComponentInChildren<OTSprite>();

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
