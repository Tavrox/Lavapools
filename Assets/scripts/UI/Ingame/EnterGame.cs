﻿using UnityEngine;
using System.Collections;

public class EnterGame : MonoBehaviour {

	private TextUI _lvlNameDisp;


	public void Setup(LevelInfo _currLvl)
	{
		_lvlNameDisp = FETool.findWithinChildren(gameObject, "LEVEL_NAME").GetComponent<TextUI>();
		_lvlNameDisp.text = _currLvl.LvlName.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this != null)
		{
			if (Input.GetKeyDown(LevelManager.InputMan.EnterButton) || Input.GetKeyDown(LevelManager.InputMan.KeyEnter) ) 
			{
				GameEventManager.TriggerRespawn("Enter Game");
			}
		}
	}
}
