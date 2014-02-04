using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public float score = 0f;
	public float bestScore = 0f;
	public int fieldsCaptured = 0;
	public int centSecondsElapsed;
	public int SecondsElapsed;
	public int bestTime;
	public string timeString;
	public float brickSpeed = 0.050f;
	
	public List<Waypoint> locationList = new List<Waypoint>();
	
	private Label scoreLabel;
	private Label bestScoreLabel;
	private Label besttimeLabel;
	private Label timeLabel;
	private Label respawnLabel;
	
	private Player _player;

	// Use this for initialization
	void Start () {
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
	
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		scoreLabel = GameObject.Find("UI/Ingame/Score").GetComponent<Label>();
		bestScoreLabel = GameObject.Find("UI/Ingame/BestScore").GetComponent<Label>();
		timeLabel = GameObject.Find("UI/Ingame/Time").GetComponent<Label>();
		besttimeLabel = GameObject.Find("UI/Ingame/BestTime").GetComponent<Label>();
		respawnLabel = GameObject.Find("UI/Respawn").GetComponent<Label>();
	
		InvokeRepeating("updateTime", 0f, 0.01f);
		StartCoroutine("spawnFields");
		InvokeRepeating("alimentFields", 10f, 8f);
		InvokeRepeating("checkScore", 0, 10f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
		updateScore();
		
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
		GameObject _newField = Instantiate(Resources.Load("Fields")) as GameObject;
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
		respawnField();
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
		respawnLabel.isActivated = false;
	}
	
	private void GameOver()
	{
		respawnLabel.isActivated = true;
		CancelInvoke("updateTime");
	}
	
	private void Respawn()
	{
		brickSpeed = 0.050f;
		InvokeRepeating("updateTime", 0f, 0.01f);
		respawnLabel.isActivated = false;
		_player.enabled = true;
		score = 0f;
		fieldsCaptured = 0;
		centSecondsElapsed = 0;
		SecondsElapsed = 0;
	
	}
}
