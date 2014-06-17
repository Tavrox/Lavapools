using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(LevelManager))]

public class LevelManagerEditor : Editor 
{
	private LevelManager levMan;
	
	public override void OnInspectorGUI()
	{
		levMan = (LevelManager)target;
		base.OnInspectorGUI();

	}
}