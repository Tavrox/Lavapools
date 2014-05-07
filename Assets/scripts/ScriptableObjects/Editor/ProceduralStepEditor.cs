using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ProceduralSteps))]
public class ProceduralStepEditor : Editor
{
	private ProceduralSteps step;
	
	public override void OnInspectorGUI()
	{
		step = (ProceduralSteps)target;

		Rect dragArea = GUILayoutUtility.GetRect(0f,25f,GUILayout.ExpandWidth(true));
		GUI.Box (dragArea, "UDLR for Tower Directio" +
			"/n Care for lists if errors, 1 element in each list to fully setup");
		base.OnInspectorGUI();

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		for (int i = 0; i < step.BricksEnabled.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.EnumPopup("", step.BricksEnabled[i]);
//			EditorGUILayout.TextField("", step.BricksEnabled[i]);
			EditorGUILayout.EndHorizontal();
		}



		if (GUILayout.Button("Reset"))
		{
			step = (ProceduralSteps)target;
			step.Reset();
		}
	}
}