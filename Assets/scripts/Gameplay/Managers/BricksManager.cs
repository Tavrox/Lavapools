using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BricksManager : MonoBehaviour {
	
	[HideInInspector] public List<LevelBrick> BricksList = new List<LevelBrick>();
	
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
		}
	}

	public void findBrick()
	{

	}
}


