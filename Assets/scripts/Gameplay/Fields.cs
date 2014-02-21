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
	public bool isStatic = false;

	private FieldAnims _anims;


	// Use this for initialization
	void Start () {
		base.Setup();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();

//		currentWP = _levMan.tools.pickRandomLoc(_levMan.locationList);
		if (isStatic != true)
		{
			gameObject.transform.position = currentWP.transform.position;
		}
		_anims = gameObject.AddComponent<FieldAnims>();
		_anims.Start();

		setupTarget();
		if (isStatic != true)
		{
			Destroy(gameObject, 8f);
		}
//		InvokeRepeating("StepUpdate", 0f, _levMan.TuningDocument.GLOBAL_speed);
		InvokeRepeating("CheckState", 0f, 0.1f);

	}

	void Update()
	{

	}
	
	// Update is called once per frame
	void StepUpdate () {
		pos = gameObject.transform.position;
		gameObject.transform.position += new Vector3 ( speed * FETool.Round( direction.x, 2), speed * FETool.Round( direction.y, 2) , 0f);
		Debug.DrawRay(pos, direction);

		if (capScore >= 80f && countCaptured == false)
		{
			state = fieldState.Captured;
			isCaptured = true;
			_player.triggerNotification();
			countCaptured = true;
			_levMan.fieldsCaptured += 1;
		}
	}

	public void CheckState()
	{
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
				capScore += 1f * LevelManager.TuningDocument.CaptureSpeed;			
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
			if (isCaptured != true)
			{
			capScore = 0f;
			state = fieldState.Uncaptured;
			}
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

	}
}