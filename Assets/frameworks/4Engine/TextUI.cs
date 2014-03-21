using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class TextUI : MonoBehaviour {

	public GameSetup SETUP;
	public TextMesh _mesh;
	public string text;
	public Color initColor;
	public Color color;

	public void Awake()
	{
		SETUP = Resources.Load("Tuning/GameSetup") as GameSetup;
		_mesh = GetComponent<TextMesh>();
		initColor = color;
	}
	
	void Update()
	{
		text = text.Replace("/n", "\n");
		_mesh.text = text;
		_mesh.color = color;
	}

	public void makeFadeOut()
	{
		new OTTween(this, 0.4f).Tween("color", Color.clear);
	}

	public void makeFadeIn()
	{
		new OTTween(this, 0.4f).Tween("color", initColor);
	}

	public void Format()
	{
		print ("fu");
		text = text.Replace("/n", "\n");
	}
}
