using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(LevelBuilder))]

class LevelBuilder : Editor {

//	public string MapToBuild;

	public override void OnInspectorGUI() 
	{
		if(GUILayout.Button("Test"))
		{
			Debug.Log("It's alive: " + target.name);
		}
	}
}
