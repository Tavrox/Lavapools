using UnityEngine;
using System.Collections;

public class ArrowTower : LevelBrick {

	public float FIRE_RATE;
	private Arrow linkedArrow;
	private OTAnimatingSprite _mainSpr;


	// Use this for initialization
	public void Setup () 
	{
		type = typeList.ArrowTower;
		triggerShooting();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		FIRE_RATE = speed;
	
	}

	override public void enableBrick()
	{
		isEnabled = true;
		triggerShooting();
	}

	override public void disableBrick()
	{
		isEnabled = false;
		stopShooting();
	}

	private void ShootArrow()
	{

	}

	public void triggerShooting()
	{
		InvokeRepeating("ShootArrow", 0f, 1f);
	}

	public void stopShooting()
	{
		CancelInvoke("ShootArrow");
	}
}
