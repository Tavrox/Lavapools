﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	[HideInInspector] public LPTuning TuningDocument;
	
	[HideInInspector] public static GameEventManager.GameState GAMESTATE;
	public GameEventManager.GameState _EditorState ;

	[HideInInspector] public float score = 0f;
	[HideInInspector] public float bestScore = 0f;
	[HideInInspector] public int fieldsCaptured = 0;
	[HideInInspector] public int centSecondsElapsed;
	[HideInInspector] public int SecondsElapsed;
	[HideInInspector] public int bestTime;
	[HideInInspector] public string timeString;
	[HideInInspector] public LevelTools tools;
	[HideInInspector] public Procedural proc;
	public int levelID;

	[HideInInspector] public List<WaypointManager> waypointsMan = new List<WaypointManager>();
	[HideInInspector] public BricksManager bricksMan;

	private MainMenu menuManager;
	private FieldManager fieldMan;
	
	private Player _player;
	[HideInInspector] public PlayerProfile _profile;

	// Use this for initialization
	void Awake () {

		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GAMESTATE = _EditorState;
		_profile = ScriptableObject.CreateInstance("PlayerProfile") as PlayerProfile;
		
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

		menuManager = GameObject.Find("UI").GetComponent<MainMenu>();
		menuManager.Setup();
		
		proc.triggerStep(proc._listSteps[0]);
	
	}

	void Start()
	{
		switch (GAMESTATE)
		{
		case GameEventManager.GameState.Live :
		{
			GameEventManager.TriggerGameStart(name);
			break;
		}	
		case GameEventManager.GameState.GameOver :
		{
			GameEventManager.TriggerGameOver(name);
			break;
		}	
		}
		InvokeRepeating("updateTime", 0f, 0.01f);
		InvokeRepeating("spawnFields", TuningDocument.DelayBeforeSpawn, TuningDocument.SpawnFrequency);
	}

	// Update is called once per frame
	void Update () {
	
		updateScore();
		
		if (GAMESTATE == GameEventManager.GameState.GameOver)
		{
			if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Space))
			{
				GameEventManager.TriggerRespawn(gameObject.name);
			}
		}
		menuManager._IngameUI.Score.text = score.ToString();
		
		if (score > bestScore)
		{
			bestScore = score;
			menuManager._IngameUI.BestScore.text = bestScore.ToString();
		}
	}
	
	public void updateScore()
	{
		score = (fieldsCaptured * TuningDocument.CapturePoint_Score) + (SecondsElapsed * TuningDocument.ScoreOverTime);
	}
	public void updateTime()
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

	public void spawnFields()
	{
		if (GameEventManager.gameOver != true )
		{
			fieldMan.respawnField();
//			fieldSpawning = true;
		}
	}

	#region Events
	private void GameStart()
	{
		_player.gameObject.SetActive(true);
	}
	
	private void GameOver()
	{
		_player.gameObject.SetActive(false);
		CancelInvoke("updateTime");
	}
	
	private void Respawn()
	{
		InvokeRepeating("updateTime", 0f, 0.01f);
		_player.gameObject.SetActive(true);
		score = 0f;
		fieldsCaptured = 0;
		centSecondsElapsed = 0;
		SecondsElapsed = 0;
	
	}
	#endregion
}
