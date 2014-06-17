using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(WaypointManager))]

public class WaypointManagerEditor : Editor 
{
	private WaypointManager manager;
	
	public override void OnInspectorGUI()
	{
		manager = (WaypointManager)target;

		Waypoint[] wpm = manager.GetComponentsInChildren<Waypoint>();
		foreach (Waypoint wp in wpm)
		{
			manager.hasBreakpoint = (wp.WPType == Waypoint.TypeList.Breakpoint) ? true : false ;
		}
		EditorGUILayout.HelpBox ("This waypoint has a breakpoint :" + manager.hasBreakpoint, MessageType.Info);
		base.OnInspectorGUI();

		
	}
}