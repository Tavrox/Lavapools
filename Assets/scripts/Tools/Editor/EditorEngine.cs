using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(FEEditor))]

public class EditorEngine : Editor 
{
	private FEEditor editor;

	public override void OnInspectorGUI()
	{
		editor = (FEEditor)target;
		base.OnInspectorGUI();

		if (GUILayout.Button("Draw Rays", GUILayout.ExpandWidth(true)))
		{
			editor.drawWaypointRays();
		}
		if (GUILayout.Button("SetupWPM", GUILayout.ExpandWidth(true)))
		{
			editor.setupWPM();
		}
	}
}
