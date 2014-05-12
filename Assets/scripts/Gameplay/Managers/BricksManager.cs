using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BricksManager : MonoBehaviour {
	
	public List<LevelBrick> BricksList = new List<LevelBrick>();
	
	// Use this for initialization
	public void Setup () {
		LevelBrick[] bricksArray = GetComponentsInChildren<LevelBrick>();
		BricksList.Clear();
		foreach (LevelBrick _brick in bricksArray)
		{
			BricksList.Add(_brick);
			if (_brick.GetComponent<Bird>() != null)
			{
				_brick.GetComponent<Bird>().Setup();
			}
			if (_brick.GetComponent<Chainsaw>() != null)
			{
				_brick.GetComponent<Chainsaw>().Setup();
			}
			if (_brick.GetComponent<ArrowTower>() != null)
			{
				_brick.GetComponent<ArrowTower>().Setup();
			}
			if (_brick.GetComponent<BladeTower>() != null)
			{
				_brick.GetComponent<BladeTower>().Setup();
			}
			if (_brick.GetComponent<Carpet>() != null)
			{
				_brick.GetComponent<Carpet>().Setup();
			}
			_brick.disableBrick();
		}
	}

	public void findBrick()
	{

	}
}


