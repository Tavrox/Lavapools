using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class TextUI : MonoBehaviour {

	[HideInInspector] public GameSetup SETUP;
	[HideInInspector] public TextMesh _mesh;
	public string DIALOG_ID;
	public string text;
	[HideInInspector] public Color initColor;
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
		text = text.Replace("/n", "\n");
	}
	public void TranslateThis()
	{
		SETUP.TextSheet.SetupTranslation(SETUP.ChosenLanguage);
		text = SETUP.TextSheet.TranslateSingle(DIALOG_ID);
	}
	public void TranslateAllInScene()
	{
		SETUP.TextSheet.SetupTranslation(SETUP.ChosenLanguage);
		TextUI[] allTxt = GameObject.FindObjectsOfType(typeof(TextUI)) as TextUI[];
		SETUP.TextSheet.TranslateAll(ref allTxt);
	}
	public void textParser()
	{
//		$OBJ$

	}
}
