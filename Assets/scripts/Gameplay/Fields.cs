using UnityEngine;
using System.Collections;

public class Fields : PatrolBrick {

	public bool isCaptured;
	public enum fieldState
	{
		Uncaptured,
		Capturing,
		Captured
	};
	private fieldState state;
	public bool countCaptured;
	private float capScore;

	private FieldAnims _anims;


	// Use this for initialization
	void Start () {
		base.Start();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();

		currentWP = _levMan.pickRandomLoc();
		gameObject.transform.position = currentWP.transform.position;

		_anims = gameObject.AddComponent<FieldAnims>();
		_anims.Start();

		setupTarget();
		Destroy(gameObject, 8f);
		InvokeRepeating("CheckState", 0f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		pos = gameObject.transform.position;
		gameObject.transform.position += new Vector3 ( speed * direction.x * Time.deltaTime, speed * direction.y * Time.deltaTime, 0f);
		Debug.DrawRay(pos, direction);
		print ("Entered Update");

		if (capScore >= 80f && countCaptured == false)
		{
			state = fieldState.Captured;
			isCaptured = true;
			_player.triggerNotification();
			countCaptured = true;
			_levMan.fieldsCaptured += 1;
			print (state);
			print ("Point Captured");
		}
	}

	public void CheckState()
	{
		print ("Entered Repeating");
		switch (state)
		{
		case fieldState.Uncaptured :
		{
			_anims.playAnimation(_anims._UNCAPTURED);
			break;
		}
		case fieldState.Capturing :
		{
			_anims.playAnimation(_anims._CAPTURING, 0.2f);
			break;
		}
		case fieldState.Captured :
		{
			_anims.playAnimation(_anims._CAPTURED);
			print ("Playing Captured Anim");
			break;
		}
		}
	}

	public void OnTriggerStay(Collider _other)
	{
		if (_other.CompareTag("Player") && isCaptured != true)
		{
			state = fieldState.Capturing;
			if (capScore < 100f)
			{
				capScore += 1f * _levMan.TuningDocument.CaptureSpeed;			
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