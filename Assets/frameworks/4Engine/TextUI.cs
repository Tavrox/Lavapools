﻿using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]

public class TextUI : MonoBehaviour {

	public TextMesh _mesh;
	public string text;
	public Color initColor;
	public Color color;

	public void Awake()
	{
		_mesh = GetComponent<TextMesh>();
		initColor = color;
	}
	
	void Update()
	{
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
}
