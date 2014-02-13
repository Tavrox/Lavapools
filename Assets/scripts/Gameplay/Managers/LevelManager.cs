using UnityEngine;
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
	public int levelID;

	public List<Waypoint> locationList = new List<Waypoint>();
	
	private Label scoreLabel;
	private Label bestScoreLabel;
	private Label besttimeLabel;
	private Label timeLabel;
	private Label respawnLabel;
	private MainMenu menuManager;

	
	private Player _player;
	[HideInInspector] public PlayerProfile _profile;

	// Use this for initialization
	void Awake () {

		_player 	= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		GAMESTATE = _EditorState;

		_profile = ScriptableObject.CreateInstance("PlayerProfile") as PlayerProfile;

		scoreLabel 	= GameObject.Find("UI/Ingame/ScoreDisplayParent/Score").GetComponent<Label>();
		bestScoreLabel = GameObject.Find("UI/Ingame/ScoreDisplayParent/BestScore").GetComponent<Label>();
		timeLabel = GameObject.Find("UI/Ingame/TimeDisplayParent/Time").GetComponent<Label>();
		besttimeLabel = GameObject.Find("UI/Ingame/TimeDisplayParent/BestTime").GetComponent<Label>();
		//		respawnLabel = GameObject.Find("UI/Respawn/RespawnText").GetComponent<Label>();
		menuManager = GameObject.Find("UI").GetComponent<MainMenu>();
		TuningDocument = FETool.setupDoc();
		TuningDocument.initScript();
	
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
		StartCoroutine("spawnFields");
		InvokeRepeating("alimentFields", 10f, 8f);
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
		
//		Debug.LogWarning("GameState" + GameEventManager.state);
		
		timeLabel.text = SecondsElapsed.ToString();
		timeLabel.text += ":";
		timeLabel.text += centSecondsElapsed.ToString();
		besttimeLabel.text = bestTime.ToString();

		scoreLabel.text = score.ToString();
		scoreLabel.text += " pts";
		
		if (score > bestScore)
		{
			bestScore = score;
			bestScoreLabel.text = bestScore.ToString();
			bestScoreLabel.text += " pts";
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
	public Waypoint pickRandomLoc()
	{	
		int _rand = Random.Range(0, 3);
		return locationList[_rand];
	}
	public void respawnField()
	{
		GameObject _newField = Instantiate(Resources.Load("Bricks/Fields")) as GameObject;
		_newField.transform.parent = GameObject.Find("LevelManager/LevelBricks/Bricks").gameObject.transform;
		Fields _field = _newField.GetComponent<Fields>();
		Waypoint _wp = pickRandomLoc();
		_field.currentWP = _wp;
		_field.transform.position = _field.currentWP.transform.position;
	}
	public void alimentFields()
	{
		if (GameEventManager.gameOver != true)
		{
			respawnField();
		}
	}

	public void enableBrick(int _brickID)
	{

	}

	
	IEnumerator spawnFields()
	{
		yield return new WaitForSeconds(4f);
		if (FEDebug.spawnsFieldsBool != true)
		{
			respawnField();
		}
	}
	
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
}
