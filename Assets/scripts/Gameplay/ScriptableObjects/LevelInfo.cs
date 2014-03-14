using UnityEngine;
using System.Collections;

public class LevelInfo : ScriptableObject {

	public GameSetup.LevelList Name;
	public float player_best_score;
	public float player_best_time;
	public bool unlocked;
}
