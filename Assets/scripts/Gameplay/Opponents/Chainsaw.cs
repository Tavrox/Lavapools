using UnityEngine;
using System.Collections;

public class Chainsaw : PatrolBrick {

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
		setupPath();
	}
}
