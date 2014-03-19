using UnityEngine;
using System.Collections;

public class Fields : PatrolBrick {

	[HideInInspector]
	public bool isCaptured;
	public enum fieldState
	{
		Uncaptured,
		Capturing,
		Captured
	};
	private fieldState state;
	[HideInInspector]
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
			gameObject.transform.parent.transform.position = currentWP.transform.position;
			Destroy(gameObject.transform.parent.gameObject, 12f);
		}
		_anims = gameObject.AddComponent<FieldAnims>();
		_anims.Start();

		setupTarget();
		InvokeRepeating("CheckState", 0f, 0.1f);

	}

	void Update()
	{
		pos = gameObject.transform.parent.transform.position;
		gameObject.transform.parent.transform.position += new Vector3 ( speed * FETool.Round( direction.x, 2), speed * FETool.Round( direction.y, 2) , 0f);
		Debug.DrawRay(pos, direction);

		/*
		if (capScore >= 80f && countCaptured == false)
		{
			state = fieldState.Captured;
			isCaptured = true;
			countCaptured = true;
			_levMan.fieldsCaptured += 1;
		}
		*/
	
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
			_anims.playAnimation(_anims._CAPTURING, 0.075f);
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
		/* CAPTURE DEACTIVATED
		if (_other.CompareTag("Player") && isCaptured != true)
		{
			state = fieldState.Capturing;
			if (capScore < 100f)
			{
				capScore += 1f * LevelManager.TuningDocument.CaptureSpeed;			
			}
		}
		*/
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