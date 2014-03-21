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
			_wp.Resizer = wp.Resizer;
			_wp.GetComponent<BoxCollider>().size = new Vector3( _wp.Resizer, _wp.Resizer, 50f);
		}
	}
}