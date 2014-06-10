using UnityEngine;
using System.Collections;

public class Chainsaw : PatrolBrick {

	public OTAnimatingSprite Invert;

	public void LateSetup () 
	{
		base.Setup();
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);		
		type = typeList.Chainsaw;
		Invert = FETool.findWithinChildren(gameObject, "Inversion").GetComponentInChildren<OTAnimatingSprite>();
	}

	public void triggerByStep()
	{
		if (brickPath != null)
		{
			brickPath.relatedBrick.Add(this);
		}
		else
		{
			Debug.Log("The path of "+gameObject.name+" is missing.");
		}
		setupPath();
	}

	public void launchInvertAnim()
	{
		Invert.PlayOnce("invert");
		OTTween invertSpeed = new OTTween(this, 1f).Tween("speed", 0f).PingPong();
	}

	public void prepareBrick()
	{
		animSpr.Play();
	}

	void OnDrawGizmosSelected()
	{
		type = typeList.Chainsaw;
	}
}
