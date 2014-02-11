using UnityEngine;
using System.Collections;

public class Fields : LevelBrick {

	public enum captureType
	{
		CapturePoint,
		None,
		Static
	};
	public captureType capPoint = captureType.None;
	public bool isCaptured;
	public bool countCaptured;
	public Waypoint spawnWP;
	public Waypoint nextWP;
	private OTSprite spr;
	private bool isDestroying;
	private Vector3 pierce;
	private Vector3 randomTarget = new Vector3(0f,0f,0f);
	private int capScore;


	// Use this for initialization
	void Start () {
		base.Start();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		spr = GetComponentInChildren<OTSprite>();

		if (spawnWP != null && nextWP != null)
		{
			target = nextWP.transform.position;
			direction = (target - pos).normalized;
			Destroy(gameObject, 8f);
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		//InvokeRepeating("changeTarget", 3f, 6f);
	}
	
	// Update is called once per frame
	void Update () {
		pos = gameObject.transform.position;
		pierce = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y+10);
	
		Vector3 movingVec = new Vector3(0f,0f,0f);
	
		if (spr!=null && isCaptured == false)
		{
//			spr.frameName = "uncaptured";
		}
		if (spr!=null && capPoint == captureType.None)
		{
//			spr.frameName = "field";
		}
		if (spr!=null && isCaptured == true)
		{
//			spr.frameName = "captured";
		}
		if (this.gameObject.transform.position != randomTarget)
		{
			gameObject.transform.position += new Vector3 ( speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, 0f);
			//Debug.Log(direction);
			//Debug.Log(speed);
		}
		if (capScore == 80 && countCaptured == false)
		{
			isCaptured = true;
			_player.triggerNotification();
			countCaptured = true;
			_levMan.fieldsCaptured += 1;
			spr.alpha = 1f;
		}
	}
	
	public void changeTarget()
	{
		randomTarget = _levMan.pickRandomLoc().gameObject.transform.position;
		direction = (gameObject.transform.position - randomTarget).normalized;
	}
	
	public void destroyField()
	{
		new OTTween(spr, 3f).Tween("size",  new Vector2(0f,0f));
		Destroy(this.gameObject, 3f);
		_levMan.respawnField();
	}
	
	public void OnTriggerStay(Collider _other)
	{
		if (_other.CompareTag("Player") && capPoint == captureType.CapturePoint)
		{
			if (capScore < 100)
			{
//				spr.frameName = "capturing";
				capScore += 1;			
			}
		}
	}
	public void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			_player.OnPlatforms += 1;
		}
	}
	public void OnTriggerExit(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			_player.OnPlatforms -= 1;
		}
	}

	private void GameStart()
	{
		
	}
	
	private void GameOver()
	{
		
	}

	private void Respawn()
	{
		DestroyImmediate(this.gameObject);
	}
}