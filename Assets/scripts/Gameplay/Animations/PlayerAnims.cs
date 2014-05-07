using UnityEngine;
using System.Collections;

public class PlayerAnims : MonoBehaviour {

	private OTAnimation _animations;
	private OTAnimatingSprite _animSprite;

	public OTAnimationFrameset _STATIC;
	public OTAnimationFrameset _WALK;


	// Use this for initialization
	public void Setup () {

		_animations = GameObject.Find("Frameworks/OT/Animations/player").GetComponent<OTAnimation>();
		_animSprite = GetComponentInChildren<OTAnimatingSprite>();
		_STATIC = _animations.GetFrameset("static");
		_WALK = _animations.GetFrameset("walk");
	}

	public void playAnimation(OTAnimationFrameset _anim)
	{
		if (_animSprite.animationFrameset != _anim.name)
		{
			_animSprite.animationFrameset = _anim.name;
		}
	}
}