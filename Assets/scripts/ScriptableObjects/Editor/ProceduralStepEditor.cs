using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ProceduralSteps))]
public class ProceduralStepEditor : Editor
{
	private ProceduralSteps step;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Reset"))
		{
			step = (ProceduralSteps)target;
			step.Reset();
		}
	}
}