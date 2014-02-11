using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class UserInput : MonoBehaviour {

	public string playerName;
	private float offsetX = 100f;
	private float offsetY = -580f;
	public GUISkin skin;
	public Color color;

	void OnGUI()
	{
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
		GUI.skin = skin;
		skin.textField.normal.textColor = color;
		playerName = GUI.TextField(new Rect(point.x - offsetX, Screen.currentResolution.height - point.y  + offsetY, 200, 200), playerName);
	}
}
