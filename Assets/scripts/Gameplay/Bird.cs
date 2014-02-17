using UnityEngine;
using System.Collections;

public class Bird : PatrolBrick {

	public void Start () 
	{
		base.Setup();
		
		setupTarget();
		saveWaypoints();
		
	}

	public void turnToward(GameObject _target)
	{

	}
}
