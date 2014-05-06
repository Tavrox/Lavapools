using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ProceduralSteps))]
public class ProceduralStepEditor : Editor
{
	private ProceduralSteps step;
	
	public override void OnInspectorGUI()
	{
		Rect dragArea = GUILayoutUtility.GetRect(0f,25f,GUILayout.ExpandWidth(true));
		GUI.Box (dragArea, "UDLR for Tower Directio" +
			"/n Care for lists if errors, 1 element in each list to fully setup");
		base.OnInspectorGUI();
		if (GUILayout.Button("Reset"))
		{
			step = (ProceduralSteps)target;
			step.Reset();
		}
	}
}