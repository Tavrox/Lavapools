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
		if (GUILayout.Button("FormatText"))
		{
			text = (TextUI)target;
			text.Format();
		}
		if (GUILayout.Button("FindTranslation"))
		{
			text = (TextUI)target;
		}
	}

}