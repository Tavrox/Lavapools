using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public static LPTuning GlobTuning;
	public static LevelParameters LocalTuning;
	public static LevelInfo CurrentLevelInfo;
	public static InputManager InputMan;
	public static GameEventManager.GameState GAMESTATE;

	public GameEventManager.GameState _EditorState ;
	public GameSetup.LevelList NAME;
	public LevelParameters.levelTypeList GAMETYPE;

	[HideInInspector] public bool GemHasSpawned = false;
	public float score = 0f;
	[HideInInspector] public float bestScore = 0f;
	[HideInInspector] public float collecSum = 0f;
	[HideInInspector] public int gemCounter = 0;
	[HideInInspector] public int centSecondsElapsed;
	[HideInInspector] public int SecondsElapsed;
	[HideInInspector] public int OvertimeScoreElapsed;
	[HideInInspector] public int bestTime;
	[HideInInspector] public string timeString;
	[HideInInspector] public BricksManager bricksMan;
	[HideInInspector] public WaypointDirector wpDirector;
	[HideInInspector] public List<Collectible> CollectibleGathered = new List<Collectible>();
	[HideInInspector] public List<CollectiblePlaces> collecPlaces = new List<CollectiblePlaces>();
	[HideInInspector] public LevelTools tools;
	[HideInInspector] public LevelTools.KillerList killer;
	[HideInInspector] public MainMenu menuManager;
	[HideInInspector] public PlayerData _profile;
	[HideInInspector] public Player _player;
	[HideInInspector] public SpaceGate Gate;
	[HideInInspector] public GameObject OuterSpawn;
	public Lootstack Loot;

	// GAME MODES
	[HideInInspector] public LinearStepTrigger linearTrigger;
	[HideInInspector] public VerticalScroller VerticalManager;
	

	// Use this for initialization
	public void Awake () {

		if (GameObject.Find("Frameworks") == null)
		{
			GameObject fmObj = Instantiate(Resources.Load("Presets/Frameworks")) as GameObject;
			fmObj.name = "Frameworks";
		}

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

		if (_profile.SETUP.GameType == GameSetup.versionType.Demo)
		{
//			Screen.SetResolution(800,600, false);
//			GameObject.Find("Frameworks/OT/View").GetComponent<OTView>().pixelPerfectResolution = new Vector2(800f,600f);
//			GameObject.Find("Frameworks/OT/View").GetComponent<OTView>().alwaysPixelPerfect = true;
//			GameObject.Find("Frameworks/OT/View").GetComponent<OTView>().customSize = 4;
		}


		GAMESTATE = _EditorState;
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		GlobTuning = Instantiate(Resources.Load("Tuning/Global")) as LPTuning;
		LocalTuning = Instantiate(Resources.Load("Linear/" + NAME + "/Setup")) as LevelParameters;
		LocalTuning.initScript();
		InputMan = Instantiate(Resources.Load("Tuning/InputManager")) as InputManager;
		CurrentLevelInfo = Instantiate(Resources.Load("Tuning/Levels/" + NAME)) as LevelInfo;
		GAMETYPE = LocalTuning.levelType;

		switch (GAMETYPE)
		{
			case LevelParameters.levelTypeList.Debuggin :
			{
			linearTrigger = gameObject.AddComponent<LinearStepTrigger>();
			linearTrigger.Setup(this);
			break;
			}
			case LevelParameters.levelTypeList.Linear :
			{
			linearTrigger = gameObject.AddComponent<LinearStepTrigger>();
			linearTrigger.Setup(this);
			break;
			}
			case LevelParameters.levelTypeList.Maze :
			{
			
			break;
			}
			case LevelParameters.levelTypeList.Procedural :
			{
			
			break;
			}
			case LevelParameters.levelTypeList.Vertical :
			{
			VerticalManager = gameObject.AddComponent<VerticalScroller>();
			VerticalManager.Setup(this);
			break;
			}
		}


		
		GameObject OutSpw = new GameObject("OuterSpawn");
		OuterSpawn = OutSpw;
		OuterSpawn.transform.parent = FETool.findWithinChildren(this.gameObject, "Enviro").transform;
		OuterSpawn.transform.position = new Vector3(0f, -7.5f, 0f);

		tools = gameObject.AddComponent<LevelTools>();
		tools._levMan = this;
		TranslateAllInScene();


		CollectiblePlaces[] collecPla = FETool.findWithinChildren(this.gameObject, "Enviro/CollectiblePlaces").GetComponentsInChildren<CollectiblePlaces>();
		foreach (CollectiblePlaces cpl in collecPla)
		{
			collecPlaces.Add(cpl);
		}
		Gate = GameObject.FindGameObjectWithTag("SpaceGate").GetComponent<SpaceGate>();
		Gate.Setup(this);

		wpDirector = GetComponentInChildren<WaypointDirector>();
		wpDirector.Setup(this);

		bricksMan = FETool.findWithinChildren(this.gameObject, "LevelBricks/Bricks").GetComponent<BricksManager>();
		bricksMan.Setup();

		if (LocalTuning.levelType != LevelParameters.levelTypeList.Debuggin)
		{
			if (GameObject.Find("UI") == null)
			{
				GameObject uiman = Instantiate(Resources.Load("Presets/UI")) as GameObject;
				uiman.name = "UI";
				menuManager = uiman.GetComponent<MainMenu>();
			}
			else
			{
				menuManager = GameObject.Find("UI").GetComponent<MainMenu>();
			}
		}
		if (menuManager != null)
		{
			menuManager.Setup(this);
		}
		managerChecker();
//		proc.triggerStep(proc._listSteps[0]);
		_player.Setup();
		Setup();
	}

	void Setup()
	{
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GameEventManager.EndGame += EndGame;

		if (GAMESTATE == GameEventManager.GameState.MainMenu)
		{
			GameEventManager.TriggerGameStart("LM");
		}
		Loot = tools.createStack();
		Loot.Setup(this);

		InvokeRepeating("updateTime", 0f, 0.01f);
		InvokeRepeating("UpdateScoreOverTime", 0f, 0.1f);
	}

	// Update is called once per frame
	void Update () 
	{
		updateScore();
		if (Input.GetKey(InputMan.KeyEnter) || Input.GetButton(InputMan.EnterButton))
		{
			respawnPlayer("levman");
		}
		if (menuManager != null)
		{
			menuManager._IngameUI.Score.text = score.ToString();
			
			if (score > bestScore)
			{
				bestScore = score;
				menuManager._IngameUI.BestScore.text = bestScore.ToString();
			}
			_player.playerName = menuManager._GameOverUI._RespawnUI._playerInput.text;
		}
		if (LocalTuning.levelType == LevelParameters.levelTypeList.Debuggin)
		{
			if (Input.GetKeyDown(LevelManager.InputMan.EnterButton) || Input.GetKeyDown(LevelManager.InputMan.KeyEnter) ) 
			{
				GameEventManager.TriggerRespawn("Enter Game");
			}
		}

	}

	public void respawnPlayer(string way)
	{
		if (GAMESTATE == GameEventManager.GameState.GameOver)
		{
			if (score == bestScore && score != 0)
			{
				string name = _player.playerName.Replace("%0d", "");
				if (menuManager._GameOverUI != null)
				{
					menuManager._GameOverUI._lb.SendScore(name, score, CurrentLevelInfo.levelID);
				}
			}
			GameEventManager.TriggerRespawn(way);
		}
	}
	
	public void updateScore()
	{
		if (menuManager != null)
		{
			score = collecSum;
		}
	}

	private void UpdateScoreOverTime()
	{
		OvertimeScoreElapsed += 1;
	}

	public void updateTime()
	{
		if (GAMESTATE == GameEventManager.GameState.Live)
		{
			centSecondsElapsed += 1;
			if (centSecondsElapsed == 100)
			{
				centSecondsElapsed = 0;
				SecondsElapsed +=1;
			}
			if (SecondsElapsed > bestTime)
			{
				bestTime = SecondsElapsed;
			}
		}
	}


	public void TranslateAllInScene()
	{
		_profile.SETUP.TextSheet.SetupTranslation(_profile.SETUP.ChosenLanguage);
		TextUI[] allTxt = GameObject.FindObjectsOfType(typeof(TextUI)) as TextUI[];
		_profile.SETUP.TextSheet.TranslateAll(ref allTxt);
	}

	public void triggerSpawnGem(CollectiblePlaces _origin)
	{
		List<CollectiblePlaces> AllPlacesToSpawn = new List<CollectiblePlaces>();
		CollectiblePlaces _chosen;
		foreach (CollectiblePlaces pl in collecPlaces)
		{
			AllPlacesToSpawn.Add(pl);
			AllPlacesToSpawn.Remove(_origin);
		}
		_chosen = tools.calculateFarSpawnPlace(ref AllPlacesToSpawn, _player);
		if (LevelManager.GAMESTATE != GameEventManager.GameState.GameOver && _chosen != null)
		{
			StartCoroutine(delayedSpawnGem(_chosen));
		}
	}

	IEnumerator delayedSpawnGem(CollectiblePlaces _placeToSpawn)
	{
		yield return new WaitForSeconds(LocalTuning.Gem_SpawnRate);
		if (LevelManager.GAMESTATE != GameEventManager.GameState.GameOver)
		{
			_placeToSpawn.Spawn(this);
		}
	}

	private void managerChecker()
	{
		if (Application.loadedLevelName != NAME.ToString() || wpDirector.waypointsMan == null || _player == null)
		{
			Debug.Log("LevMan bad setup");
		}
		InvokeRepeating("managerCheckerInvoke", 0f, 1f);
	}
	private void managerCheckerInvoke()
	{
//		Debug.Log("Current Lvl State" + GAMESTATE);
	}

	#region Events
	private void GameStart()
	{


	}
	
	private void GameOver()
	{
		if (this != null)
		{
			tools.modifyStack(ref Loot);
			CancelInvoke("updateTime");
			CancelInvoke("UpdateScoreOverTime");
			StopCoroutine("delayedSpawnGem");
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			OvertimeScoreElapsed = 0;
			InvokeRepeating("UpdateScoreOverTime", 0f, 0.1f);
			InvokeRepeating("updateTime", 0f, 0.01f);
			score = 0f;
			centSecondsElapsed = 0;
			SecondsElapsed = 0;
			collecSum = 0;
			CollectibleGathered.Clear();
			foreach (CollectiblePlaces cpl in collecPlaces)
			{
				cpl.Spawn(this);
			}
		}
	}

	private void EndGame()
	{
		if (this != null)
		{
			MasterAudio.PlaySound("teleport");
		}
	}

	#endregion
}
