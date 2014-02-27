using UnityEngine;
using System.Collections;

public class Bird : PatrolBrick {

	public void Start () 
	{
		base.Setup();
		if (brickPath != null)
		{
			brickPath.relatedBrick = this;
			brickPathId = brickPath.id;
		}
		else
		{
			Debug.Log("The path of "+gameObject.name+" is missing.");
		}
		currentWP = brickPath.pickRandomWP();
		transform.position = brickPath.findNextWaypoint(currentWP).transform.position;
		setupTarget();
		saveWaypoints();

		
	}

	public void turnToward(GameObject _target)
	{

	}
}
