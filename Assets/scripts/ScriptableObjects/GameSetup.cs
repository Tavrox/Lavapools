using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSetup : ScriptableObject {

	public enum LevelList
	{
		Grensdalur,
		Etna,
		Vesuvio,
		None
	};
	public enum languageList
	{
		Francais,
		English
	};
	public Vector2 GameSize;
	public float OrthelloSize;
	public string gameversion;
	public DialogSheet Dialogs;
	public string twitter_url;
	public string facebook_url;
	public string website_url;
}
