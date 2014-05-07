using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour {

	public PlayerProfile PROFILE;
	public GameSetup SETUP;
	public InputManager INPUT;

	public float GlobalVolume = 1f;
	public float musicVolume = 1f;

	public bool globalVolMuted = false;
	public bool musicVolMuted = false;

	public float initMusicVolume = 1f;
	public float initGlobVol = 1f;

	// Use this for initialization
	public void Launch () 
	{
		PROFILE = Resources.Load("Tuning/PlayerProfile") as PlayerProfile;
		SETUP = Resources.Load("Tuning/GameSetup") as GameSetup;
		INPUT = Resources.Load("Tuning/InputManager") as InputManager;
		DontDestroyOnLoad(this);

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GameEventManager.EndGame += EndGame;
	}

	
	public void MuteMusic()
	{
		MasterAudio Master = GameObject.Find("Frameworks/MasterAudio").GetComponent<MasterAudio>();
		if (musicVolMuted == true)
		{
			musicVolMuted = false;
			Master.masterPlaylistVolume = 0f;
			MasterAudio.PauseAllPlaylists();
			musicVolume = 0f;
		}
		else
		{
			musicVolMuted = true;
			MasterAudio.ResumePlaylist();
			Master.masterPlaylistVolume = 1f;
			musicVolume = 1f;
		}
	}
	
	public void MuteGlobal()
	{
		MasterAudio Master = GameObject.Find("Frameworks/MasterAudio").GetComponent<MasterAudio>();
		if (globalVolMuted == true)
		{
			globalVolMuted = false;
			Master.masterAudioVolume = 0f;
			GlobalVolume = 0f;
		}
		else
		{
			globalVolMuted = true;
			Master.masterAudioVolume = 1f;
			GlobalVolume = 1f;
		}
	}



	private void setupSoundEnter()
	{
		MasterAudio Master = GameObject.Find("Frameworks/MasterAudio").GetComponent<MasterAudio>();
		initGlobVol = Master.masterAudioVolume;
		initMusicVolume = Master.masterPlaylistVolume;
		Master.masterAudioVolume = GlobalVolume;
		Master.masterPlaylistVolume = musicVolume;
	}
	
	private void GameStart()
	{
		if (this != null)
		{
			setupSoundEnter();
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{

		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{

		}
	}

	private void EndGame()
	{
		if (this != null)
		{

		}
	}

}
