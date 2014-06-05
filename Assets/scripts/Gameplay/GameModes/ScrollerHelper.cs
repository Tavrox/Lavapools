using UnityEngine;
using System.Collections;

public class ScrollerHelper : MonoBehaviour
{
	private VerticalScroller Scrollr;
	public bool helpScrolling;

	public void Setup(VerticalScroller _scr)
	{
		Scrollr = _scr;
		helpScrolling = LevelManager.LocalTuning.LinkedVertical.ScrollerHelper;
	}

	void OnTriggerEnter(Collider oth)
	{
		if (oth.CompareTag("Player") == true && helpScrolling == true)
		{
			Scrollr.currSlideSpeed = Scrollr.boostSlideSpeed;
		}
	}

	void OnTriggerExit(Collider oth)
	{
		if (oth.CompareTag("Player") == true && helpScrolling == true)
		{
			Scrollr.currSlideSpeed = Scrollr.initSlideSpeed;
		}
	}
}
