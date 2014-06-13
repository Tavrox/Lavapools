using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(Chainsaw))]
public class ChainsawEditor : Editor
{
	private Chainsaw brick;

	public override void OnInspectorGUI()
	{
		brick = (Chainsaw)target;


		if (brick.initWp != null)
		{
			EditorGUILayout.HelpBox("Init Waypoints is linked to manager" + brick.initWp.linkedManager.name, MessageType.Warning);
			brick.brickPath = brick.initWp.linkedManager;
		}
		base.OnInspectorGUI();
	}
}