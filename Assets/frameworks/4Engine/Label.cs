using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class Label : UIThing {

	public string text = "Label";
	public GUISkin skin;
	public int size = 10;
	public Color color;
	public enum type
	{
		Button,
		Label,
		Box
	};
	public type typeList;
	private float offsetX = 100f;
	private float offsetY = -580f;
	public bool isRespawnBtn = false;
	public bool isActivated;
	private int _depth;

	public void Start()
	{
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
	}
	
	void Update()
	{

	}

	void OnGUI()
	{
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
		GUI.skin = skin;
		if (isActivated)
		{
			GUI.depth = Mathf.RoundToInt(transform.position.z);
			switch (typeList)
			{
				case (type.Box) :
				{
					skin.box.normal.textColor = color;
					skin.box.fontSize = size;
					GUI.Box(new Rect(point.x - offsetX, Screen.currentResolution.height - point.y  + offsetY, 200, 200), text);
					break;
				}
				case (type.Label) :
				{
					skin.label.normal.textColor = color;
					this.skin.label.fontSize = size;
					GUI.Label(new Rect(point.x - offsetX, Screen.currentResolution.height - point.y + offsetY , 200, 200), text);
					break;
				}
				case (type.Button) :
				{
					skin.button.normal.textColor = color;
					skin.button.fontSize = size;
					GUI.Button(new Rect(point.x - offsetX, Screen.currentResolution.height - point.y  + offsetY, 200, 200), text);
					break;
				}
			}
		}
	}
		
	public void GameStart()
	{
		
	}
	public void GameOver()
	{
	
	}
	public void Respawn()
	{
		if (isRespawnBtn == true)
		{
			isActivated = false;
		}
	}
}
