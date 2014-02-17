using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BricksManager : MonoBehaviour {
	
	public List<LevelBrick> BricksList = new List<LevelBrick>();
	
	// Use this for initialization
	public void Setup () {
		LevelBrick[] bricksArray = GetComponentsInChildren<LevelBrick>();
		foreach (LevelBrick _brick in bricksArray)
		{
			BricksList.Add(_brick);
			if (_brick.GetComponent<PatrolBrick>() != null)
			{
				_brick.GetComponent<PatrolBrick>().Setup();
			}
		}
	}

	public void findBrick()
	{

	}

}
