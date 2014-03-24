using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(Waypoint))]

class WaypointEditor : Editor {
	
	public Waypoint wp;

	public override void OnInspectorGUI() 
	{
		base.OnInspectorGUI();
		wp = (Waypoint)target;
		Waypoint[] allWp = GameObject.FindObjectsOfType(typeof(Waypoint)) as Waypoint[];
		foreach (Waypoint _wp in allWp)
		{
			_wp.EDITOR_Resizer = wp.EDITOR_Resizer;
			_wp.GetComponent<BoxCollider>().size = new Vector3( _wp.EDITOR_Resizer, _wp.EDITOR_Resizer, 50f);
		}
	}
}