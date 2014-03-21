using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelSetup))]
public class LevelSetupEditor : Editor
{
	private LevelSetup lvl;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Copy"))
		{
			lvl = (LevelSetup)target;
			if (lvl._lvlToCopy != null)
			{
			lvl.CopySetup();
			}
		}
	}
}