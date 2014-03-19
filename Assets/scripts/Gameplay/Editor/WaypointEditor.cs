using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(Waypoint))]

class WaypointEditor : Editor {
	
	public Waypoint wp;
	public Vector3 resize;

	public override void OnInspectorGUI() 
	{
		GUILayout.BeginHorizontal();
		EditorGUILayout.Vector3Field( "Resizer" , resize );
		GUILayout.EndHorizontal();
		if(GUILayout.Button("Resize Collider"))
		{
			wp = (Waypoint)target;
			wp.setupCollider(resize);
		}
	}
}
