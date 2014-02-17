﻿using UnityEngine;
using System.Collections;

public class FieldManager : WaypointManager {

	private bool isSpawning = true;

	public void respawnField()
	{
		GameObject _newField = Instantiate(Resources.Load("Bricks/Fields")) as GameObject;
		_newField.transform.parent = GameObject.Find("LevelManager/LevelBricks/Bricks/Fields").gameObject.transform;
		Fields _field = _newField.GetComponent<Fields>();
		Waypoint _wp = _levMan.tools.findWpManager(LevelBrick.typeList.Fields).pickRandomWP();
		_field.currentWP = _wp;
		_field.brickPath = _levMan.tools.findWpManager(LevelBrick.typeList.Fields);
		_field.transform.position = _field.currentWP.transform.position;
	}
	
	public Waypoint pickRandomField(WaypointManager _manager)
	{	
		return _manager.pickRandomWP();
	}
}
