using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public static LPTuning TuningDocument;
	
	[HideInInspector] public static GameEventManager.GameState GAMESTATE;
	public GameEventManager.GameState _EditorState ;

	[HideInInspector] public float score = 0f;
	[HideInInspector] public int fieldsCaptured = 0;
	[HideInInspector] public int centSecondsElapsed;
	[HideInInspector] public int SecondsElapsed;
	[HideInInspector] public int OvertimeScoreElapsed;
	[HideInInspector] public int bestTime;
	[HideInInspector] public string timeString;
	public float bestScore = 0f;
	public int levelID;

	[HideInInspector] public List<WaypointManager> waypointsMan = new List<WaypointManager>();
	[HideInInspector] public BricksManager bricksMan;

	
	[HideInInspector] public List<Collectible> CollectibleGathered = new List<Collectible>();
	[HideInInspector] public List<CollectiblePlaces> collecPlaces = new List<CollectiblePlaces>();
	[HideInInspector] public bool GemHasSpawned = false;
	
	[HideInInspector] public LevelTools tools;
	[HideInInspector] public Procedural proc;
	public MainMenu menuManager;
	private FieldManager fieldMan;
	private Fields spawningField;
	
	[HideInInspector] public PlayerProfile _profile;
	public Player _player;


	// Use this for initialization
	void Awake () {

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		GAMESTATE = _EditorState;

		_profile = ScriptableObject.CreateInstance("PlayerProfile") as PlayerProfile;

		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
//		_player.Setup();

		TuningDocument = FETool.setupDoc();
		TuningDocument.initScript();

		proc = gameObject.AddComponent<Procedural>();
		proc._levMan = this;
		proc.Setup();

		tools = gameObject.AddComponent<LevelTools>();
		tools._levMan = this;
		levelID = int.Parse(Application.loadedLevelName);

		bricksMan = FETool.findWithinChildren(this.gameObject, "LevelBricks/Bricks").GetComponent<BricksManager>();
		bricksMan.Setup();

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

		CollectiblePlaces[] collecPla = FETool.findWithinChildren(this.gameObject, "Enviro/CollectiblePlaces").GetComponentsInChildren<CollectiblePlaces>();
		foreach (CollectiblePlaces cpl in collecPla)
		{
			collecPlaces.Add(cpl);
//			cpl.Setup(this);
		}

		menuManager = GameObject.Find("UI").GetComponent<MainMenu>();
		menuManager.Setup(this);

		proc.triggerStep(proc._listSteps[0]);
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
			GameEventManager.TriggerGameOver("LM", true);
		}
		if (GAMESTATE == GameEventManager.GameState.Live)
		{
			GameEventManager.TriggerRespawn("LM", true);
		}
		InvokeRepeating("updateTime", 0f, 0.01f);
		InvokeRepeating("UpdateScoreOverTime", 0f, 0.1f);
		InvokeRepeating("spawnFields", TuningDocument.DelayBeforeSpawn, TuningDocument.SpawnFrequency);
		InvokeRepeating("spawnGem", 3f, TuningDocument.TinyGem_SpawnRate);
	}

	// Update is called once per frame
	void Update () {
	
		updateScore();
		
		if (GAMESTATE == GameEventManager.GameState.GameOver)
		{
			if (Input.GetKey(KeyCode.Return))
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
		int sumCollectible = 0;
		foreach (Collectible _obj in CollectibleGathered)
		{
			sumCollectible += _obj.value;
		}
		score = 
			(fieldsCaptured * TuningDocument.CapturePoint_Score) 
			+ (OvertimeScoreElapsed * TuningDocument.ScoreOverTime)
			+ sumCollectible;
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

	public void spawnGem ()
	{
		List<CollectiblePlaces> _list = collecPlaces;
		int randomPick = Random.Range(0, _list.Count-1);
		foreach (CollectiblePlaces _pl in _list)
		{
			if (_pl.occupied == true)
			{
				GemHasSpawned = true;
				break;
			}
			else
			{
				GemHasSpawned = false;
			}
		}
		if (GemHasSpawned == false)
		{
			_list[randomPick].Spawn(this);
		}

	}

	public void spawnFields()
	{
		if (GameEventManager.gameOver != true )
		{
			spawningField = fieldMan.respawnField();
		}
	}

	#region Events
	private void GameStart()
	{
//		_player.gameObject.SetActive(false);
	}
	
	private void GameOver()
	{
		_player.gameObject.SetActive(false);
		if (spawningField != null)
		{
			Destroy(spawningField.gameObject);
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
		InvokeRepeating("spawnFields", TuningDocument.DelayBeforeSpawn, TuningDocument.SpawnFrequency);
		_player.gameObject.SetActive(true);
		score = 0f;
		fieldsCaptured = 0;
		centSecondsElapsed = 0;
		SecondsElapsed = 0;
	
	}
	#endregion
}
