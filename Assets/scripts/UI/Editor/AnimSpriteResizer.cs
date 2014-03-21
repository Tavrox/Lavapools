using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(OTAnimatingSprite))]
public class AnimSpriteResizer : Editor
{
	private OTAnimatingSprite sprite;
	
	public override void OnInspectorGUI()
	{
		/*
		base.OnInspectorGUI();
		if (GUILayout.Button("Resize"))
		{
			sprite = (OTAnimatingSprite)target;
			sprite.ResizeOT();
		}
		*/
	}
}