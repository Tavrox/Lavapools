using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(LevelParameters))]

public class LevelParametersEditor : Editor {
	
	private LevelParameters param;
	public List<LevelBrick> gameBricks;
	
	public override void OnInspectorGUI()
	{
		param = (LevelParameters)target;
		base.OnInspectorGUI();

		LevelBrick[] listObj = GameObject.FindObjectsOfType(typeof(LevelBrick)) as LevelBrick[];
		EditorGUILayout.HelpBox("Game bricks" +
			"\n for Arrow Tower, only initial fire rate", MessageType.Info);
		displayHeader();
		foreach (LevelBrick obj in listObj)
		{
			EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
			EditorGUILayout.ObjectField("", obj, typeof(LevelBrick),  true, GUILayout.ExpandWidth(true));
			obj.speed =  EditorGUILayout.FloatField("", obj.speed, GUILayout.ExpandWidth(true));
			EditorUtility.SetDirty(obj);
			EditorGUILayout.EndHorizontal();
		}

		EditorUtility.SetDirty(param);
	}

	private void displayHeader()
	{
		EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
		GUILayout.Box("GameBrick",GUILayout.ExpandWidth(true));
		GUILayout.Box("Speed",GUILayout.ExpandWidth(true));
		EditorGUILayout.EndHorizontal();
	}
}
