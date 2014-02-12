using UnityEngine;
using System.Collections;

public class PlayerAnims : MonoBehaviour {

	private OTAnimation _animations;
	private OTAnimatingSprite _animSprite;

	private OTAnimationFrameset _STATIC;
	private OTAnimationFrameset _WALK;


	// Use this for initialization
	void Start () {

		_animations = GameObject.Find("Frameworks/OT/Animations/player").GetComponent<OTAnimation>();
		_animSprite = GetComponentInChildren<OTAnimatingSprite>();
		_STATIC = _animations.GetFrameset("static");
		_WALK = _animations.GetFrameset("walk");
	}

	public void playAnimation(string _anim)
	{
		if (_animSprite.animationFrameset != _anim)
		{
			_animSprite.animationFrameset = _anim;
		}

	}
}
