using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

	public PlayerProfile PROFILE;
	public GameSetup SETUP;
	public float GlobalVolume = 1f;
	public bool globalVolMuted = false;
	public float musicVolume = 1f;
	public bool musicVolMuted = false;

	// Use this for initialization
	void Awake () 
	{
		PROFILE = Resources.Load("Tuning/PlayerProfile") as PlayerProfile;
		SETUP = Resources.Load("Tuning/GameSetup") as GameSetup;
		name = "PlayerData";
		DontDestroyOnLoad(this);
	}

	
	public void MuteMusic()
	{
		if (musicVolMuted == true)
		{
			print ("mutemuzik");
			musicVolMuted = false;
			musicVolume = 0f;
		}
		else
		{
			print ("unmutemuzik");
			musicVolMuted = true;
			musicVolume = 1f;
		}
		
	}
	
	public void MuteGlobal()
	{
		if (globalVolMuted == true)
		{
			print ("muteglob");
			globalVolMuted = false;
			GlobalVolume = 0f;
		}
		else
		{
			print ("unmuteglob");
			globalVolMuted = true;
			GlobalVolume = 1f;
		}
		
		
	}
}
