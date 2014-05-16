using UnityEngine;
using System.Collections;

public class PlayerAnims : MonoBehaviour {

	private OTAnimation _animations;
	private OTAnimatingSprite _animSprite;

	public string _STATIC;
	public string _WALK;
	public string _MAXWALK;
	public string _CURR;


	// Use this for initialization
	public void Setup () 
	{
		_animations = GameObject.Find("Frameworks/OT/Animations/player").GetComponent<OTAnimation>();
		_animSprite = GetComponentInChildren<OTAnimatingSprite>();
		_STATIC = "static";
		_WALK = "walk";
		_MAXWALK = "maxwalk";
		_CURR = "walk";
	}

	public void playAnimation(string _anim)
	{
		if (_animSprite.animationFrameset != _anim)
		{
			_animSprite.Play (_anim);
		}
	}

	public void changeAnimSpeed(string _anim, float _speed)
	{
		if (_animSprite.animationFrameset != _anim)
		{
			_animSprite.speed = _speed;
		}
	}
}