using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BricksManager : MonoBehaviour {
	
	public List<LevelBrick> BricksList = new List<LevelBrick>();


	// Use this for initialization
	void Start () {

		LevelBrick[] bricksArray = GetComponentsInChildren<LevelBrick>();
		foreach (LevelBrick _brick in bricksArray)
		{
			BricksList.Add(_brick);
		}
	
	}

}
