using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public LPTuning TuningDocument;
	
	public static GameEventManager.GameState GAMESTATE;
	public GameEventManager.GameState _EditorState ;

	public float score = 0f;
	public float bestScore = 0f;
	public int fieldsCaptured = 0;
	public int centSecondsElapsed;
	public int SecondsElapsed;
	public int bestTime;
	public string timeString;
	public float brickSpeed = 2.50f;
	public int levelID;

	public List<Waypoint> locationList = new List<Waypoint>();
	
	private Label scoreLabel;
	private Label bestScoreLabel;
	private Label besttimeLabel;
	private Label timeLabel;
	private Label respawnLabel;

	
	private Player _player;
	public PlayerProfile _profile;

	// Use this for initialization
	void Awake () {

		_player 	= GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		GAMESTATE = _EditorState;

		_profile = ScriptableObject.CreateInstance("PlayerProfile") as PlayerProfile;

		GameEventManager.TriggerGameStart(gameObject.name);

		scoreLabel 	= GameObject.Find("UI/Ingame/ScoreDisplayParent/Score").GetComponent<Label>();
		bestScoreLabel = GameObject.Find("UI/Ingame/ScoreDisplayParent/BestScore").GetComponent<Label>();
		timeLabel = GameObject.Find("UI/Ingame/TimeDisplayParent/Time").GetComponent<Label>();
		besttimeLabel = GameObject.Find("UI/Ingame/TimeDisplayParent/BestTime").GetComponent<Label>();
		//		respawnLabel = GameObject.Find("UI/Respawn/RespawnText").GetComponent<Label>();
		TuningDocument = FETool.setupDoc();
		TuningDocument.initScript();

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
		InvokeRepeating("checkScore", 0, 10f);
	
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
		score = (fieldsCaptured * 300f) + (SecondsElapsed * 5f);
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
		_newField.transform.parent = GameObject.Find("LevelManager").gameObject.transform;
		Fields _field = _newField.GetComponent<Fields>();
		Waypoint _wp = pickRandomLoc();
		_field.spawnWP = _wp;
		_field.nextWP = _field.spawnWP.nextWP;
		_field.transform.position = _field.spawnWP.transform.position;
		_field.capPoint = Fields.captureType.CapturePoint;
	}
	public void alimentFields()
	{
		if (GameEventManager.gameOver != true)
		{
			respawnField();
		}
	}
	
	private void checkScore()
	{
		if (score > 1000)
		{
			brickSpeed += 0.015f;
		}
		if (score > 1500)
		{
			brickSpeed += 0.020f;
		}
	}
	
	IEnumerator spawnFields()
	{
		yield return new WaitForSeconds(4f);
		respawnField();
	}
	
	private void GameStart()
	{
		brickSpeed = 0.050f;
		_player.gameObject.SetActive(true);
//		respawnLabel.isActivated = false;
	}
	
	private void GameOver()
	{
		//		respawnLabel.isActivated = true;
		_player.gameObject.SetActive(false);
		CancelInvoke("updateTime");
	}
	
	private void Respawn()
	{
		brickSpeed = 2.50f;
		InvokeRepeating("updateTime", 0f, 0.01f);
//		respawnLabel.isActivated = false;
		_player.gameObject.SetActive(true);
		score = 0f;
		fieldsCaptured = 0;
		centSecondsElapsed = 0;
		SecondsElapsed = 0;
	
	}
}
