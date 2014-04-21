using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public static LPTuning GlobTuning;
	public static LevelSetup LocalTuning;
	public static LevelInfo CurrentLevelInfo;
	public static InputManager InputMan;
	public static GameEventManager.GameState GAMESTATE;

	public GameEventManager.GameState _EditorState ;
	public GameSetup.LevelList NAME;
	public GameSetup.LevelList NEXT_LEVEL;

	[HideInInspector] public bool GemHasSpawned = false;
	public float score = 0f;
	[HideInInspector] public float bestScore = 0f;
	[HideInInspector] public float collecSum = 0f;
	[HideInInspector] public int gemCounter = 0;
	[HideInInspector] public int fieldsCaptured = 0;
	[HideInInspector] public int centSecondsElapsed;
	[HideInInspector] public int SecondsElapsed;
	[HideInInspector] public int OvertimeScoreElapsed;
	[HideInInspector] public int bestTime;
	[HideInInspector] public string timeString;
	[HideInInspector] public BricksManager bricksMan;
	[HideInInspector] public List<WaypointManager> waypointsMan = new List<WaypointManager>();
	[HideInInspector] public List<Collectible> CollectibleGathered = new List<Collectible>();
	public List<CollectiblePlaces> collecPlaces = new List<CollectiblePlaces>();
	[HideInInspector] public LevelTools tools;
	[HideInInspector] public LevelTools.KillerList killer;
	[HideInInspector] public Procedural proc;
	[HideInInspector] public MainMenu menuManager;
	[HideInInspector] public PlayerData _profile;
	[HideInInspector] public Player _player;
	[HideInInspector] public SpaceGate Gate;
	[HideInInspector] public GameObject OuterSpawn;

	
	private FieldManager fieldMan;
	private Fields spawningField;
	

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
		LocalTuning = Instantiate(Resources.Load("Procedural/" + NAME + "/Setup")) as LevelSetup;
		LocalTuning.initScript();
		InputMan = Resources.Load("Tuning/InputManager") as InputManager;
		CurrentLevelInfo = Instantiate(Resources.Load("Tuning/Levels/" + NAME)) as LevelInfo;
		proc = gameObject.AddComponent<Procedural>();
		proc._levMan = this;
		proc.Setup();

		
		GameObject OutSpw = new GameObject("OuterSpawn");
		OuterSpawn = OutSpw;
		OuterSpawn.transform.parent = FETool.findWithinChildren(this.gameObject, "Enviro").transform;
		OuterSpawn.transform.position = new Vector3(0f, -7.5f, 0f);

		tools = gameObject.AddComponent<LevelTools>();
		tools._levMan = this;
		TranslateAllInScene();

		WaypointManager[] waypointsManagers = FETool.findWithinChildren(this.gameObject, "LevelBricks/Waypoints").GetComponentsInChildren<WaypointManager>();
		foreach (WaypointManager wpm in waypointsManagers)
		{
			waypointsMan.Add(wpm);
			wpm.Setup(this);
			if (wpm.GetComponent<FieldManager>() != null)
			{
				fieldMan = wpm.GetComponent<FieldManager>();
			}
		}

		bricksMan = FETool.findWithinChildren(this.gameObject, "LevelBricks/Bricks").GetComponent<BricksManager>();
		bricksMan.Setup();

		CollectiblePlaces[] collecPla = FETool.findWithinChildren(this.gameObject, "Enviro/CollectiblePlaces").GetComponentsInChildren<CollectiblePlaces>();
		foreach (CollectiblePlaces cpl in collecPla)
		{
			collecPlaces.Add(cpl);
		}
		Gate = GameObject.FindGameObjectWithTag("SpaceGate").GetComponent<SpaceGate>();
		Gate.Setup();

		if (LocalTuning.OblivionLevel == false)
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
		proc.triggerStep(proc._listSteps[0]);
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

	}

	public void respawnPlayer(string way)
	{
		if (GAMESTATE == GameEventManager.GameState.GameOver)
		{
			if (score == bestScore && score != 0)
			{
				string name = _player.playerName.Replace("%0d", "");
				menuManager._GameOverUI._lb.SendScore(name, score, CurrentLevelInfo.levelID);
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

	public void spawnFields()
	{
		if (GameEventManager.gameOver != true )
		{
//			spawningField = fieldMan.respawnField();
		}
	}

	private void managerChecker()
	{
		if (Application.loadedLevelName != NAME.ToString() || waypointsMan == null || _player == null)
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
			_player.gameObject.SetActive(false);
			if (spawningField != null)
			{
				Destroy(spawningField.gameObject.transform.parent.gameObject);
			}
			CancelInvoke("updateTime");
			CancelInvoke("spawnFields");
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
			_player.gameObject.SetActive(true);
			score = 0f;
			fieldsCaptured = 0;
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
