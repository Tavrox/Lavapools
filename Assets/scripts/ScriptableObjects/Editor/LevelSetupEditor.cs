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
		lvl = (LevelSetup)target;
		if (lvl._lvlToCopy != null)
		{
			if (GUILayout.Button("Copy ["+ lvl._lvlToCopy.NAME +"] Setup"))
			{
				lvl.CopySetup();
			}
		}
		if (GUILayout.Button("Reset all " + lvl.numberOfSteps.ToString() + " steps of [" + lvl.NAME +"]"))
		{
			lvl.ResetAllSteps();
		}
	}
}