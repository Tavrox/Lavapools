using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public InputManager InputMan;
	public int OnPlatforms;
	
	public playerState _state;
	[HideInInspector] public float lowSpeed;
	[HideInInspector] public float medSpeed;
	[HideInInspector] public float highSpeed;
	[HideInInspector] public float initLowSpeed;
	[HideInInspector] public float initMedSpeed;
	[HideInInspector] public float initHighSpeed;
	[HideInInspector] public float currSpeed;

	public enum speedList
	{
		Low,
		Med,
		High
	};
	[HideInInspector] public speedList currSp;
	private RaycastHit hit;
	private Vector3 pos;
	private int layer;
	private int layerMask;
	private Bounds rect;
	private Vector3 startPos;
	private Vector3 friction;
	private Vector3 playerSteps;
	private Vector3 mod = new Vector3(0f,0f,0f);
	private OTSprite spr;
	private Vector2 originalSize;
	private Notification _notif;
	private PlayerAnims _anims;
	private float speedStack = 0f;
	public OTSprite dialBubble;
	public float gpUntriggerArea;

	public float bigAxNeg;
	public float bigAxPos;
	public float smallAxNeg;
	public float smallAxPos;


	[HideInInspector] public enum playerState
	{
		Alive,
		Dead
	};

	private UserLeaderboard _playerSheet;

	[HideInInspector] public string playerName;
	
	private RaycastHit hitInfo; //infos de collision
	private Ray detectTargetLeft, detectTargetRight; //point de départ, direction
	
	// Use this for initialization
	public void Setup () {
		
		if (GetComponentInChildren<OTSprite>() != null)
		{
			spr = GetComponentInChildren<OTSprite>();
			spr.alpha = 1f;
			originalSize = spr.size;
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_playerSheet = ScriptableObject.CreateInstance("UserLeaderboard") as UserLeaderboard;
		_playerSheet.name = playerName;

		lowSpeed = LevelManager.LocalTuning.Player_Speed_low;
		medSpeed = LevelManager.LocalTuning.Player_Speed_med;
		highSpeed = LevelManager.LocalTuning.Player_Speed_high;
		gpUntriggerArea = LevelManager.GlobTuning.areaOfUntrigger;
		initLowSpeed = lowSpeed;
		initMedSpeed = medSpeed;
		initHighSpeed = highSpeed;
		playerSteps = LevelManager.GlobTuning.PlayerSteps;
		InputMan = Resources.Load("Tuning/InputManager") as InputManager;

		bigAxNeg = InputMan.BigAxisNeg;
		bigAxPos = InputMan.BigAxisPos;
		smallAxNeg = InputMan.SmallAxisNeg;
		smallAxPos = InputMan.SmallAxisPos;
		 
		_anims = gameObject.AddComponent<PlayerAnims>() as PlayerAnims;
		_anims.Setup();
		_notif = GetComponentInChildren<Notification>();
		dialBubble = FETool.findWithinChildren(gameObject, "Bubble").GetComponentInChildren<OTSprite>();

		startPos = gameObject.transform.position;
		friction.x = LevelManager.GlobTuning.Player_Friction.x;
		friction.y = LevelManager.GlobTuning.Player_Friction.y;

		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GameEventManager.EndGame += EndGame;
	}

	void Update () 
	{	
		if (_state == playerState.Alive)
		{
			pos = gameObject.transform.position;
			if (LevelManager.GAMESTATE == GameEventManager.GameState.Live )
			{
				if (OnPlatforms <= 0)
				{
					_levMan.tools.tryDeath(LevelTools.KillerList.Lava);
				}
				speedChecker();
				keyboardInput();
				XboxInput();
				noMoveChecker();
				mod.x *= friction.x;
				mod.y *= friction.y;
				this.gameObject.transform.position += mod * Time.deltaTime;
			}
		}
	}

	public void lootStack(int _stk)
	{
		triggerNotification(_stk * 1f);
	}

	private void speedChecker()
	{
		if (this != null)
		{
			if (speedStack > playerSteps.x && speedStack < playerSteps.y )
			{
				currSpeed = lowSpeed;
				_anims._CURR = _anims._WALK;
				_anims.changeAnimSpeed(_anims._CURR, 1f);
			}
			if (speedStack > playerSteps.y  && speedStack < playerSteps.z)
			{
				currSpeed = medSpeed;
				_anims._CURR = _anims._WALKFASTER;
				_anims.changeAnimSpeed(_anims._CURR, 1f);
			}
			if (speedStack > playerSteps.z)
			{
				currSpeed = highSpeed;
				_anims._CURR = _anims._MAXWALK;
				_anims.changeAnimSpeed(_anims._CURR, 0.5f);
			}
		}
	}


	private void keyboardInput()
	{
		if (this != null)
		{
			if (Input.GetKey (InputMan.KeyRight)) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.x += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetKeyUp (InputMan.KeyRight)) 
			{
				StartCoroutine("StartCheckReset");
				mod.x = 10f;
				_anims.playAnimation(_anims._STATIC);
			}
			
			if (Input.GetKey (InputMan.KeyUp)) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.y += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetKeyUp (InputMan.KeyUp)) 
			{
				StartCoroutine("StartCheckReset");
				mod.y = 10f;
				_anims.playAnimation(_anims._STATIC);
			}

			if (Input.GetKey (InputMan.KeyLeft)) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				mod.x -= currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetKeyUp (InputMan.KeyLeft)) 
			{
				StartCoroutine("StartCheckReset");
				mod.x = -10f;
				_anims.playAnimation(_anims._STATIC);
			}
			
			if (Input.GetKey (InputMan.KeyDown)) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y -= currSpeed;
			}
			else if (Input.GetKeyUp (InputMan.KeyDown)) 
			{
				StartCoroutine("StartCheckReset");
				mod.y = -10f;
				_anims.playAnimation(_anims._STATIC);
			}

			// TO DO DIAGONALS SPEED LIMIT FOR UL UR DL DU etc.
			 
			if (Input.GetKey (InputMan.KeyDown) && Input.GetKey (InputMan.KeyLeft))
			{


			}
		}
	}

	private void XboxInput()
	{
		
		if (this != null)
		{
			if ( Input.GetAxisRaw("X axis") < bigAxNeg) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.x += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetAxisRaw("X axis") > bigAxPos) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				mod.x -= currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetAxisRaw("Y axis") > bigAxPos) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.y += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			else if (Input.GetAxisRaw("Y axis") < bigAxNeg) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				mod.y -= currSpeed;
				_anims.playAnimation(_anims._CURR);
			}

			if (Input.GetAxisRaw("Y axis") > bigAxPos  && Input.GetAxisRaw("X axis") > bigAxPos )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * 1f;
				mod.x = (currSpeed) * -1f;
			}
			else if (Input.GetAxisRaw("Y axis") < bigAxNeg  && Input.GetAxisRaw("X axis") < bigAxNeg )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * -1f;
				mod.x = (currSpeed) * 1f;
			}
			else if (Input.GetAxisRaw("Y axis") > bigAxPos  && Input.GetAxisRaw("X axis") < bigAxNeg )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * 1f;
				mod.x = (currSpeed) * 1f;
			}
			else if (Input.GetAxisRaw("Y axis") < bigAxNeg  && Input.GetAxisRaw("X axis") > bigAxPos )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * -1f;
				mod.x = (currSpeed) * -1f;
			}


			// KEY D PAD ON XBOX JOYSTICK


			if (Input.GetAxisRaw("6th axis") < smallAxNeg) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.x += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			
			if (Input.GetAxisRaw("7th axis") > smallAxPos) 
			{
				StopCoroutine("StartCheckReset");
				speedStack += 1f * Time.deltaTime;
				mod.y += currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			
			if (Input.GetAxisRaw("6th axis") > smallAxPos ) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				mod.x -= currSpeed;
				_anims.playAnimation(_anims._CURR);
			}
			
			if (Input.GetAxisRaw("7th axis") < smallAxNeg ) 
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				mod.y -= currSpeed;
				_anims.playAnimation(_anims._CURR);
			}

			if (Input.GetAxisRaw("7th axis") > smallAxPos  && Input.GetAxisRaw("6th axis") > smallAxPos )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * 1f;
				mod.x = (currSpeed) * -1f;
			}
			else if (Input.GetAxisRaw("7th axis") < smallAxNeg  && Input.GetAxisRaw("6th axis") < smallAxNeg )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * -1f;
				mod.x = (currSpeed) * 1f;
			}
			else if (Input.GetAxisRaw("7th axis") > smallAxPos  && Input.GetAxisRaw("6th axis") < smallAxNeg )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * 1f;
				mod.x = (currSpeed) * 1f;
			}
			else if (Input.GetAxisRaw("7th axis") < smallAxNeg  && Input.GetAxisRaw("6th axis") > smallAxPos )
			{
				speedStack += 1f * Time.deltaTime;
				StopCoroutine("StartCheckReset");
				_anims.playAnimation(_anims._CURR);
				mod.y = (currSpeed) * -1f;
				mod.x = (currSpeed) * -1f;
			}
		}
	}

	private void noMoveChecker()
	{
		if (Mathf.Abs(Input.GetAxisRaw("X axis")) < 0.1f && Mathf.Abs(Input.GetAxisRaw("Y axis" )) < 0.1f )
		{
			if ( Mathf.Abs(Input.GetAxisRaw("7th axis")) < 0.1f  && Mathf.Abs(Input.GetAxisRaw("6th axis")) < 0.1f )
			{
				if (Input.GetKey (InputMan.KeyRight) == false && Input.GetKey (InputMan.KeyDown) == false && Input.GetKey (InputMan.KeyUp) == false && Input.GetKey (InputMan.KeyLeft) == false)
				{
					StartCoroutine("StartCheckReset");
					_anims.playAnimation(_anims._STATIC);
				}
			}
		}
	}

	IEnumerator StartCheckReset()
	{
		yield return new WaitForSeconds(LevelManager.GlobTuning.PlayerSpeedReset);
		speedStack = 0f;
	}

	public void triggerNotification(float _value)
	{
		_notif.text = "+" + _value.ToString();
		_notif.makeFadeIn();
		new OTTween(dialBubble, 0.5f).Tween("alpha", 1f);
		StopCoroutine("WaitFadeSec");
		StartCoroutine(WaitFadeSec(2f));
	}

	IEnumerator WaitFadeSec(float _time)
	{
		yield return new WaitForSeconds(_time);
		_notif.makeFadeOut();
		new OTTween(dialBubble, 0.5f).Tween("alpha", 0f);
	}

	private void GameStart()
	{
		if (this != null)
		{
			_state = playerState.Alive;
			_anims.playAnimation(_anims._STATIC);
			lowSpeed = initLowSpeed;
			medSpeed = initMedSpeed;
			highSpeed = initHighSpeed;
			currSpeed = initLowSpeed;
			speedStack = 0f;
			new OTTween(spr, 0.5f).Tween("alpha", 1f);
			new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
			_notif.makeFadeOut();
			dialBubble.alpha = 0f;
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			_state = playerState.Dead;
			_anims.playAnimation(_anims._STATIC);
			lowSpeed = initLowSpeed;
			medSpeed = initMedSpeed;
			highSpeed = initHighSpeed;
			currSpeed = initLowSpeed;
			speedStack = 0f;
			new OTTween(spr, 0.5f).Tween("alpha", 0f);
			new OTTween(spr, 0.5f).Tween("size", new Vector2(0.25f,0.25f));
			_notif.makeFadeOut();
			new OTTween(dialBubble, 0.5f).Tween("alpha", 0f);
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			_state = playerState.Alive;
			_anims.playAnimation(_anims._STATIC);
			lowSpeed = initLowSpeed;
			medSpeed = initMedSpeed;
			highSpeed = initHighSpeed;
			currSpeed = initLowSpeed;
			speedStack = 0f;
			gameObject.transform.position = startPos;
			new OTTween(spr, 0.5f).Tween("alpha", 1f);
			new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
			_notif.makeFadeOut();
			dialBubble.alpha = 0f;
		}
	}

	private void EndGame()
	{
//		transform.position = new Vector3(transform.position.x, transform.position.y, 200f);
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(this.transform.position, gpUntriggerArea);
	}
}
