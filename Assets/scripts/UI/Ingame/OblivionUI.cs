using UnityEngine;
using System.Collections;

public class OblivionUI : MonoBehaviour {

	
	[HideInInspector] public PlayerData _profile;

	// Use this for initialization
	void Start () {

		if (GameObject.FindGameObjectWithTag("PlayerData") == null)
		{
			GameObject _dataplayer = Instantiate(Resources.Load("Presets/PlayerData")) as GameObject;
			_profile = _dataplayer.GetComponent<PlayerData>();
			_profile.Launch();
		}
		else
		{
			_profile = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
			_profile.Launch();
		}

		if (_profile.SETUP.alphaVersion == true)
		{

		}
		if (_profile.SETUP.demoVersion == true)
		{
			
		}


		TranslateAllInScene();
	
	}
	public void TranslateAllInScene()
	{
		_profile.SETUP.TextSheet.SetupTranslation(_profile.SETUP.ChosenLanguage);
		TextUI[] allTxt = GameObject.FindObjectsOfType(typeof(TextUI)) as TextUI[];
		_profile.SETUP.TextSheet.TranslateAll(ref allTxt);
	}
}
