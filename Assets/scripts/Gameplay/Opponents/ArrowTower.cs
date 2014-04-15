using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowTower : LevelBrick {

	private Arrow linkedArrow;
	private OTAnimatingSprite _mainSpr;
	public List<LevelTools.DirectionList> enabledDirection;
	
	// Use this for initialization
	public void Setup () 
	{
		type = typeList.ArrowTower;
		triggerShooting();
	}

	override public void enableBrick()
	{
		isEnabled = true;
		triggerShooting();
	}
	
	public void triggerShooting()
	{
		InvokeRepeating("ShootArrow", 0f, speed);
	}

	override public void disableBrick()
	{
		isEnabled = false;
		stopShooting();
		print ("boum");
	}

	private void ShootArrow()
	{
		print ("bing");
		if (enabledDirection.Count > 0)
		{
			foreach (LevelTools.DirectionList dir in enabledDirection)
			{
				GameObject _arrow = Instantiate(Resources.Load("Bricks/Opponent/SingleArrow")) as GameObject;
				linkedArrow = _arrow.GetComponent<Arrow>();
				linkedArrow.speed = LevelManager.LocalTuning.Arrow_Speed;
				linkedArrow.giveDirection(dir);
			}
		}
	}

	public void stopShooting()
	{
		CancelInvoke("ShootArrow");
		print ("stop dat");
	}
}
