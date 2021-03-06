﻿using UnityEngine;
using System.Collections;

public class UIThing : MonoBehaviour {

	private OTSprite spr;

	private TextUI lab;

	private OTAnimatingSprite animSpr;
	
	void Start()
	{
		if (GetComponentInChildren<OTSprite>() != null)
		{
			spr = GetComponentInChildren<OTSprite>();
		}
		if (GetComponentInChildren<OTAnimatingSprite>() != null)
		{
			animSpr = GetComponentInChildren<OTAnimatingSprite>();
		}
		if (GetComponent<TextUI>() != null)
		{
			lab = GetComponentInChildren<TextUI>();
		}
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.S))
		{
			fadeSprite(0f,2f);
		}
	}
	
	public void fadeSprite(float _alpha = 1f, float _time = 2f)
	{
		if (spr != null)
		{
			new OTTween(spr, _time).Tween("alpha", _alpha);
		}
		if (animSpr != null)
		{
			new OTTween(spr, _time).Tween("alpha", _alpha);
		}
	}
}
