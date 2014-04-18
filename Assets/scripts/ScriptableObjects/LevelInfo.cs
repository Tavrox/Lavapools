using UnityEngine;
using System.Collections;

public class LevelInfo : ScriptableObject {
	
	public int levelID;
	public GameSetup.LevelList LvlName;
	public float player_best_score;
	public bool availableDemo;
	public bool locked;
	public bool specialLevel = false;

}
