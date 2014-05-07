using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LinearStep))]
public class LinearStepEditor : Editor
{
	private LinearStep step;
	
	public override void OnInspectorGUI()
	{
		step = (LinearStep)target;

		Rect dragArea = GUILayoutUtility.GetRect(0f,25f,GUILayout.ExpandWidth(true));
		GUI.Box (dragArea, "UDLR for Tower Directio" +
			"/n Care for lists if errors, 1 element in each list to fully setup");
		base.OnInspectorGUI();

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		for (int i = 0; i < step.NumberBricksModifier ; i++)
		{
//			BrickStepParam brkPar = 
			EditorGUILayout.BeginHorizontal();
//			EditorGUILayout.EnumPopup("", step.BricksEnabled[i]);
//			EditorGUILayout.TextField("", step.BricksEnabled[i]);
			EditorGUILayout.EndHorizontal();
		}



		if (GUILayout.Button("Reset"))
		{
			step = (LinearStep)target;
			step.Reset();
		}
	}
}