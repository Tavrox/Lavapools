using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : ParentMenu {
	
	private List<GameObject> listMenus;
	public LevelManager _levman;

	[HideInInspector] public IngameUI _IngameUI;
	[HideInInspector] public GameOverUI _GameOverUI;
	[HideInInspector] public EntryUI _EntryUI;
	[HideInInspector] public EndGameUI _EndGameUI;

	public Vector3 IngamePlaceDemo = new Vector3(0f,1.37f, 0f);
	public Vector3 LeaderboardPlaceDemo = new Vector3(-3.8f, -4.71f, -10f);

	// Use this for initialization
	public void Setup (LevelManager _lm) 
	{
		name = "UI";
		_levman = _lm;
		transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -100f);
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		_IngameUI = FETool.findWithinChildren(this.gameObject, "Ingame").GetComponent<IngameUI>();
		_GameOverUI = FETool.findWithinChildren(this.gameObject, "GameOver").GetComponent<GameOverUI>();
		_EntryUI = FETool.findWithinChildren(this.gameObject, "EntryMenu").GetComponent<EntryUI>();
		_EndGameUI = FETool.findWithinChildren(this.gameObject, "EndGame").GetComponent<EndGameUI>();

		_IngameUI.SetupSub(this);
		_IngameUI.Setup();
		_GameOverUI.SetupSub(this);
		_GameOverUI.Setup();
		_EntryUI.SetupSub(this);
		_EntryUI.Setup();
		_EndGameUI.SetupSub(this);
		_EndGameUI.Setup();
		
		if (_lm._profile.SETUP.GameType == GameSetup.versionType.Demo)
		{
			_IngameUI.initPos = IngamePlaceDemo;
			_GameOverUI.lbInitpos = LeaderboardPlaceDemo;
			_EndGameUI.lbInitpos = LeaderboardPlaceDemo;
		}

	}


	private List<GameObject> buildChildrenList()
	{

		List<GameObject> childrenList = new List<GameObject>();
		/***** TO FIX ****
		GameObject[] listCompo = GetComponentsInChildren<GameObject>();
		foreach (GameObject _compo in listCompo)
		{

			if (_compo.tag != "Submenu")
			{
				childrenList.Add(_compo.gameObject);
				Debug.Log(_compo.gameObject.name);
			}
			Debug.Log(_compo.name);
		}
		*/
		return childrenList;
	}

	public void MakeFadeOut(UIThing _thing)
	{
		StartCoroutine(CoFadeOut(_thing));
	}

	IEnumerator CoFadeOut(UIThing _thing)
	{
		yield return new WaitForSeconds(2f);
		_thing.gameObject.SetActive(false);
	}

	public void changeLevelLabel(LinearStep _step)
	{
		_IngameUI.LevelTxt.text = "" + _step.levelLabel + "";
		_IngameUI.TriggerLvText();

	}

	private void GameStart()
	{

	}
	
	private void GameOver()
	{

	}
	
	private void Respawn()
	{

	}
}