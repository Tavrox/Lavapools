using UnityEngine;
using System.Collections;

public class Chainsaw : PatrolBrick {

	public void Start () 
	{
		base.Start();

		setupTarget();
		saveWaypoints();
	}
}
