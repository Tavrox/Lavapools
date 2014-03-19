using UnityEngine;
using System.Collections;

public class FieldManager : WaypointManager {

	private bool isSpawning = true;

	public Fields respawnField()
	{
		GameObject _newField = Instantiate(Resources.Load("Bricks/Environment/SpawnedField")) as GameObject;
		_newField.transform.parent = GameObject.Find("LevelManager/LevelBricks/Bricks/Fields").gameObject.transform;

		Fields _field = _newField.GetComponentInChildren<Fields>();
//		BigGem _gem = _newField.GetComponentInChildren<BigGem>();
		Waypoint _wp = _levMan.tools.findWpManager(LevelBrick.typeList.Fields).pickRandomWP();

		_field.currentWP = _wp;
		_field.brickPath = _levMan.tools.findWpManager(LevelBrick.typeList.Fields);
		_newField.transform.position = _field.currentWP.transform.position;
//		_gem.Setup(_levMan);

		return (_field);
	}
	
	public Waypoint pickRandomField(WaypointManager _manager)
	{	
		return _manager.pickRandomWP();
	}

}
