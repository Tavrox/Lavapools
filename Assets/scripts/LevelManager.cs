using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

	public float score = 0f;
	public int fieldsCaptured = 0;
	public int centSecondsElapsed;
	public int SecondsElapsed;
	public int bestTime;
	public string timeString;
	
	public List<Waypoint> locationList = new List<Waypoint>();
	
	private Label scoreLabel;
	private Label besttimeLabel;
	private Label fieldcapturedLabel;
	private Label timeLabel;

	// Use this for initialization
	void Start () {
	
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
	
		scoreLabel = GameObject.Find("UI/Score").GetComponent<Label>();
		fieldcapturedLabel = GameObject.Find("UI/FieldsCaptured").GetComponent<Label>();
		timeLabel = GameObject.Find("UI/Time").GetComponent<Label>();
		besttimeLabel = GameObject.Find("UI/BestTime").GetComponent<Label>();
	
		InvokeRepeating("updateTime", 0f, 0.01f);
		StartCoroutine("spawnFields");
		InvokeRepeating("alimentFields", 10f, 8f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
		updateScore();
		
		
		timeLabel.text = SecondsElapsed.ToString();
		timeLabel.text += ":";
		timeLabel.text += centSecondsElapsed.ToString();
		besttimeLabel.text = bestTime.ToString();
		fieldcapturedLabel.text = fieldsCaptured.ToString();

		scoreLabel.text = score.ToString();
	}
	
	public void updateScore()
	{
		score = (fieldsCaptured * 100f) + (SecondsElapsed * 5f);
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
		
		int res = Random.Range(0,1);
		if (res == 0)
		{
			_field.capPoint = Fields.captureType.CapturePoint;
		}
		else
		{
			_field.capPoint = Fields.captureType.None;
		}	
	}
	public void alimentFields()
	{
		respawnField();
	}
	
	IEnumerator spawnFields()
	{
		yield return new WaitForSeconds(4f);
		respawnField();
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
