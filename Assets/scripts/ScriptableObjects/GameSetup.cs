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
	public Vector2 GameSize;
	public float OrthelloSize;
	public List<LevelList> ActivatedLevels = new List<LevelList>();
	public string gameversion;
	public DialogSheet Dialogs;
}
