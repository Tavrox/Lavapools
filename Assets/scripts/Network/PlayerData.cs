using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

	public PlayerProfile PROFILE;
	public GameSetup SETUP;

	// Use this for initialization
	void Awake () 
	{
		PROFILE = Resources.Load("Tuning/PlayerProfile") as PlayerProfile;
		SETUP = Resources.Load("Tuning/GameSetup") as GameSetup;
		DontDestroyOnLoad(this);
	}
}
