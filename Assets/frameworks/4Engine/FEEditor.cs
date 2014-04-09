using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[ExecuteInEditMode]

public class FEEditor : MonoBehaviour {


	public List<WaypointManager> WPM = new List<WaypointManager>();
	public List<Color> cols = new List<Color>();
//	public List<GameObject> listGO = new List<GameObject>();

	[ContextMenu ("SetupColors")]
	void SetupColors()
	{
		foreach (WaypointManager _wpm in WPM)
		{
			cols.Add(Color.red);
		}
	}


	void OnDrawGizmos () 
	{
		/*
		Debug.Log("CAREFUL : GIZMO PREVENT INVERTING WAYPOINTS");
		if (WPM != null && cols != null)
		{
			for (int j = 0; j < WPM.Count-1; j++)
			{
				WPM[j].relatedWaypoints = WPM[j].GetWpList();
				Gizmos.color = cols[j];
				for (int i = 0; i < WPM[j].relatedWaypoints.Count-1; i++)
				{
					Gizmos.DrawLine(WPM[j].relatedWaypoints[i].transform.position, WPM[j].relatedWaypoints[i+1].transform.position);
					if (WPM[j].relatedWaypoints[i].id == WPM[j].lastWp.id)
					{
						Gizmos.DrawLine(WPM[j].relatedWaypoints[i].transform.position, WPM[j].relatedWaypoints[0].transform.position);
					}
				}
			}
		}
		*/
	}



}
