using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogSheet : ScriptableObject {

	private GameSetup.languageList currLanguage;
	public Dictionary<string, string> Text_FR;
	public Dictionary<string, string> Text_EN;

	public void SetupTranslation(GameSetup.languageList _lang)
	{
		currLanguage = _lang;
	}

	public void GetTranslation(string _entryToFetch)
	{
		// seek with currLanguage
	}
}
