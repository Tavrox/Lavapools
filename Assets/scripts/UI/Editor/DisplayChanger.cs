using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OTView))]

public class DisplayChanger : Editor {

	private OTView viewer;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Change To 800x600"))
	    {
			viewer = (OTView)target;
			viewer.changeToDemoRes();
		}
		if (GUILayout.Button("Change To 1366x768"))
		{
			viewer = (OTView)target;
			viewer.changeToFullRes();
		}


	}
}
