using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(TextUI))]
public class TextEditor : Editor
{
	private TextUI text;
	private DialogSheet Dialogs;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		text = (TextUI)target;
		if (GUILayout.Button("FormatText"))
		{
			text.Format();
		}
		if (GUILayout.Button("TranslateThis"))
		{
			text.TranslateThis();
		}
		if (GUILayout.Button("TranslateAllInScene"))
		{
			text.TranslateAllInScene();
		}



	}

}