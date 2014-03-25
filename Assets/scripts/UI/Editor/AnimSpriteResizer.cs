using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(OTAnimatingSprite))]
public class AnimSpriteResizer : Editor
{
	private OTAnimatingSprite sprite;
	
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
	}
}