using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public static LPTuning GlobTuning;
	public static LevelSetup LocalTuning;
	public static LevelInfo CurrentLevelInfo;
	public static GameEventManager.GameState GAMESTATE;

	public GameEventManager.GameState _EditorState ;
	public GameSetup.LevelList NAME;

	[HideInInspector] public bool GemHasSpawned = false;
	[HideInInspector] public float score = 0f;
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
	[HideInInspector] public List<CollectiblePlaces> collecPlaces = new List<CollectiblePlaces>();
	[HideInInspector] public LevelTools tools;
	[HideInInspector] public LevelTools.KillerList killer;
	[HideInInspector] public Procedural proc;
	[HideInInspector] public MainMenu menuManager;
	[HideInInspector] public PlayerProfile _profile;
	[HideInInspector] public InputManager InputMan;
	[HideInInspector] public Player _player;
	
	private FieldManager fieldMan;
	private Fields spawningField;

	public SpaceGate Gate;



	// Use this for initialization
	void Awake () {

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		GAMESTATE = _EditorState;

		_profile = ScriptableObject.CreateInstance("PlayerProfile") as PlayerProfile;

		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		
		GlobTuning = Instantiate(Resources.Load("Tuning/Global")) as LPTuning;
		LocalTuning = Instantiate(Resources.Load("Procedural/" + NAME + "/Setup")) as LevelSetup;
		LocalTuning.initScript();
		InputMan = Resources.Load("Tuning/InputManager") as InputManager;
		CurrentLevelInfo = Instantiate(Resources.Load("Tuning/Levels/" + NAME)) as LevelInfo;

		proc = gameObject.AddComponent<Procedural>();
		proc._levMan = this;
		proc.Setup();

		tools = gameObject.AddComponent<LevelTools>();
		tools._levMan = this;

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
			cpl.Spawn(this);
		}
		Gate = GameObject.FindGameObjectWithTag("SpaceGate").GetComponent<SpaceGate>();
		Gate.Setup();

		menuManager = GameObject.Find("UI").GetComponent<MainMenu>();
		menuManager.Setup(this);

		managerChecker();

		proc.triggerStep(proc._listSteps[0]);
		_player.Setup();
		Setup();
	}

	void Setup()
	{

		if (GAMESTATE == GameEventManager.GameState.MainMenu)
		{
			GameEventManager.TriggerGameStart("LM", true);
		}
		if (GAMESTATE == GameEventManager.GameState.GameOver)
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.LevelManager, true);
		}
		if (GAMESTATE == GameEventManager.GameState.Live)
		{
			GameEventManager.TriggerRespawn("LM", true);
		}
		InvokeRepeating("updateTime", 0f, 0.01f);
		InvokeRepeating("UpdateScoreOverTime", 0f, 0.1f);
//		InvokeRepeating("spawnFields", LocalTuning.Fields_DelaySpawn, LocalTuning.Fields_FrequencySpawn);
	}

	// Update is called once per frame
	void Update () {
	
		updateScore();

		if (GAMESTATE == GameEventManager.GameState.GameOver)
		{
			if (Input.GetKey(InputMan.KeyEnter) || Input.GetButton(InputMan.EnterButton))
			{
				if (score == bestScore && score != 0)
				{
					string name = _player.playerName.Replace("%0d", "");
					menuManager._GameOverUI._lb.SendScore(name, score);
				}
				GameEventManager.TriggerRespawn(gameObject.name);
			}
		}
		menuManager._IngameUI.Score.text = score.ToString();
		
		if (score > bestScore)
		{
			bestScore = score;
			menuManager._IngameUI.BestScore.text = bestScore.ToString();
		}
		_player.playerName = menuManager._GameOverUI._RespawnUI._playerInput.text;

	}
	
	public void updateScore()
	{
		score = collecSum;
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

	public void triggerSpawnGem(CollectiblePlaces _place)
	{
		StartCoroutine(delayedSpawnGem(_place));
	}

	IEnumerator delayedSpawnGem(CollectiblePlaces _place)
	{
		yield return new WaitForSeconds(LocalTuning.Gem_SpawnRate);
		_place.Spawn(this);
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
	}

	#region Events
	private void GameStart()
	{

	}
	
	private void GameOver()
	{
		_player.gameObject.SetActive(false);
		if (spawningField != null)
		{
			Destroy(spawningField.gameObject.transform.parent.gameObject);
		}
		CancelInvoke("updateTime");
		CancelInvoke("spawnFields");
		CancelInvoke("UpdateScoreOverTime");
	}
	
	private void Respawn()
	{
		OvertimeScoreElapsed = 0;
		InvokeRepeating("UpdateScoreOverTime", 0f, 0.1f);
		InvokeRepeating("updateTime", 0f, 0.01f);
		InvokeRepeating("spawnFields", LocalTuning.Fields_DelaySpawn, LocalTuning.Fields_FrequencySpawn);
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
	#endregion
}
