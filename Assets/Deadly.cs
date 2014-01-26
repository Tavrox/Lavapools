using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deadly : MonoBehaviour {

	private Vector3 direction;
	private Vector3 target;
	private Vector3 pos;
	private Vector3 initPos;
	public Waypoint currentWP;
	public bool gameOver = false;
	private LevelManager _levMan;
	private float speed;

	// Use this for initialization
	void Start () {
	
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		speed = _levMan.brickSpeed;
		initPos = gameObject.transform.position;
		pos = gameObject.transform.position;
		target = currentWP.nextWP.transform.position;
		direction = (target - pos).normalized;
//		followWaypoints();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (gameOver != true)
		{
			speed = _levMan.brickSpeed;
			pos = gameObject.transform.position;
			Debug.DrawRay(pos, direction, Color.cyan);
			
			if (pos.x == target.x && pos.y == target.y)
			{
				followWaypoints();
			}
			gameObject.transform.position += new Vector3 ( speed * direction.x, speed * direction.y, 0f);
		}
	}
	
	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player"))
		{
			GameEventManager.TriggerGameOver();
		}
	}
	
	public void followWaypoints()
	{
		currentWP = currentWP.nextWP;
		target = currentWP.nextWP.transform.position;
		direction = (target - pos).normalized;
	}
	
	private void GameStart()
	{
		gameOver = false;
	}
	
	private void GameOver()
	{
		gameOver = true;
	}
	
	private void Respawn()
	{
		gameOver = false;
		gameObject.transform.position = initPos;
	}
}
