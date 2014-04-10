using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

	public PlayerProfile PROFILE;
	public GameSetup SETUP;
	public InputManager INPUT;
	public float GlobalVolume = 1f;
	public bool globalVolMuted = false;
	public float musicVolume = 1f;
	public bool musicVolMuted = false;

	// Use this for initialization
	public void Launch () 
	{
		PROFILE = Resources.Load("Tuning/PlayerProfile") as PlayerProfile;
		SETUP = Resources.Load("Tuning/GameSetup") as GameSetup;
		INPUT = Resources.Load("Tuning/InputManager") as InputManager;
		DontDestroyOnLoad(this);
	}

	
	public void MuteMusic()
	{
		if (musicVolMuted == true)
		{
			musicVolMuted = false;
			musicVolume = 0f;
			MasterAudio.PlaylistsMuted = false;
		}
		else
		{
			musicVolMuted = true;
			musicVolume = 1f;
			MasterAudio.PlaylistsMuted = true;
		}
		
	}
	
	public void MuteGlobal()
	{
		if (globalVolMuted == true)
		{
			globalVolMuted = false;
			GlobalVolume = 0f;
			MasterAudio.MixerMuted = true;
		}
		else
		{
			globalVolMuted = true;
			GlobalVolume = 1f;
			MasterAudio.MixerMuted = false;
		}
		
		
	}
}
