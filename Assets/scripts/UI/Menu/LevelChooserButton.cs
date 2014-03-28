using UnityEngine;
using System.Collections;

public class LevelChooserButton : MiscButton {

	public enum DirectionList
	{
		Right,
		Left
	};
	public DirectionList direction;
	private bool trigger = true;
	private OTSprite _spr;
	public float twDuration = 0.8f;

	// Use this for initialization
	public void Setup (LevelChooser _ch, DirectionList _direc) 
	{
		chooser = _ch;
		direction = _direc;
		_spr = GetComponentInChildren<OTSprite>();
	}

	void OnMouseDown()
	{
		if (locked == false && trigger == true)
		{
			base.LockButtons();
			chooser.SwipeThumbnail(this);
		}
	}

	public void TriggerBtn (bool _state) 
	{
		trigger = _state;
		if (trigger == false)
		{
			_spr.alpha = 0f;
			GetComponent<BoxCollider>().isTrigger = false;
		}
		else
		{
			_spr.alpha = 1f;
			GetComponent<BoxCollider>().isTrigger = true;
		}
	}
}
