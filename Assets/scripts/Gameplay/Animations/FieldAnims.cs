using UnityEngine;
using System.Collections;

public class FieldAnims : FEAnims {
	
	public OTAnimationFrameset _UNCAPTURED;
	public OTAnimationFrameset _CAPTURED;
	public OTAnimationFrameset _CAPTURING;

	// Use this for initialization
	public void Start () {

		_animations = GameObject.Find("Frameworks/OT/Animations/plateform").GetComponent<OTAnimation>();
		_animSprite = GetComponentInChildren<OTAnimatingSprite>();

		_UNCAPTURED = _animations.GetFrameset("uncaptured");
		_CAPTURED = _animations.GetFrameset("captured");
		_CAPTURING = _animations.GetFrameset("capturing");
	
	}
}
