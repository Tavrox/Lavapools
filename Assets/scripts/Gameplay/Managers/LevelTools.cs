using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTools : MonoBehaviour {

	public LevelManager _levMan;
	
	public void disableBrick (LevelBrick _brickToEnable)
	{
		
	}
	
	public void disableBirck (LevelBrick.typeList _type)
	{
		
	}
	
	public void disableBrick (List<LevelBrick> _brickList)
	{
		
	}
	
	public void enableTypeOfBrick(LevelBrick.typeList _type)
	{
		
	}
	
	public void enableBrick (LevelBrick _brickToEnable)
	{
		
	}

	public void respawnField()
	{
		GameObject _newField = Instantiate(Resources.Load("Bricks/Fields")) as GameObject;
		_newField.transform.parent = GameObject.Find("LevelManager/LevelBricks/Bricks").gameObject.transform;
		Fields _field = _newField.GetComponent<Fields>();
		Waypoint _wp = pickRandomLoc(_levMan.locationList);
		_field.currentWP = _wp;
		_field.transform.position = _field.currentWP.transform.position;
	}
	
	public Waypoint pickRandomLoc(List<Waypoint> _list)
	{	
		int rand = Random.Range(0, 3);
		return _list[rand];
	}
}
