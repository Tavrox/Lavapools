using UnityEngine;
using System.Collections;

public class Chainsaw : PatrolBrick {

	public OTAnimatingSprite Invert;

	public void Start () 
	{
		type = typeList.Chainsaw;
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
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
		Invert = FETool.findWithinChildren(gameObject, "Inversion").GetComponentInChildren<OTAnimatingSprite>();
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
}
