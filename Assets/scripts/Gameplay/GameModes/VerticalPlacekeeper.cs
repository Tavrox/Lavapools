using UnityEngine;
using System.Collections;

public class VerticalPlacekeeper : MonoBehaviour {

	private VerticalScroller Scrollr;
	public bool stopsPlayer;

	// Use this for initialization
	public void Setup (VerticalScroller _scr) 
	{
		Scrollr = _scr;
		stopsPlayer = LevelManager.LocalTuning.LinkedVertical.ScrollerStopper;
	}

	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player") == true && stopsPlayer == true)
		{
			Scrollr.stopScroll();
		}
	}
}
