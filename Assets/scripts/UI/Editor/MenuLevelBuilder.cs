using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelChooser))]
public class MenuLevelBuilder : Editor
{
	private LevelChooser chooser;

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		if (GUILayout.Button("Build"))
		{
			chooser = (LevelChooser)target;
			chooser.Setup();
		}
	}
}
