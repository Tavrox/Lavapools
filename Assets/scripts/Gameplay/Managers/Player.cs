using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public InputManager InputMan;
	public int OnPlatforms;

	[SerializeField] private float _speed;
	public float initSpeed;
	private RaycastHit hit;
	private Vector3 pos;
	private int layer;
	private int layerMask;
	private Bounds rect;
	private Vector3 startPos;
	private Vector3 friction;
	private Vector3 mod = new Vector3(0f,0f,0f);
	private OTSprite spr;
	private Vector2 originalSize;
	private Notification _notif;
	private PlayerAnims _anims;
	public float speed
	{ get {return _speed;} set {_speed = value;} }

	[HideInInspector] public enum playerState
	{
		Walk,
		Static
	};
	[HideInInspector] public playerState _state;

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

		speed = LevelManager.LocalTuning.Player_Speed;
		initSpeed = speed;
		InputMan = Resources.Load("Tuning/InputManager") as InputManager;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		 
		_anims = gameObject.AddComponent<PlayerAnims>() as PlayerAnims;
		_notif = GetComponentInChildren<Notification>();

		startPos = gameObject.transform.position;
		friction.x = LevelManager.GlobTuning.Player_Friction.x;
		friction.y = LevelManager.GlobTuning.Player_Friction.y;
	}
	
	// Update is called once per frame
	void Update () {	
	
		pos = gameObject.transform.position;
//		print ("Platforms[" + OnPlatforms+"]");

		if (speed == 0)
		{
			Debug.Log("Speed of crab is 0");
		}

		if (LevelManager.GAMESTATE == GameEventManager.GameState.Live )
		{
			if (OnPlatforms <= 0)
			{
				GameEventManager.TriggerGameOver(LevelTools.KillerList.Lava);
				MasterAudio.PlaySound("Enviro");
			}
			KeyInput();
			XboxInput();
			mod.x *= friction.x;
			mod.y *= friction.y;
			this.gameObject.transform.position += mod * Time.deltaTime;
		}
	}

	private void KeyInput()
	{
		if (Input.GetKey (InputMan.KeyRight)) 
		{
			mod.x += speed;
			_anims.playAnimation(_anims._WALK);
		}
		else if (Input.GetKeyUp (InputMan.KeyRight)) 
		{
			mod.x = 10f;
			_anims.playAnimation(_anims._STATIC);
		}
		
		if (Input.GetKey (InputMan.KeyUp)) 
		{
			mod.y += speed;
			_anims.playAnimation(_anims._WALK);
		}
		else if (Input.GetKeyUp (InputMan.KeyUp)) 
		{
			mod.y = 10f;
			_anims.playAnimation(_anims._STATIC);
		}
		
		if (Input.GetKey (InputMan.KeyLeft)) 
		{
			mod.x -= speed;
			_anims.playAnimation(_anims._WALK);
		}
		else if (Input.GetKeyUp (InputMan.KeyLeft)) 
		{
			mod.x = -10f;
			_anims.playAnimation(_anims._STATIC);
		}
		
		if (Input.GetKey (InputMan.KeyDown)) 
		{
			mod.y -= speed;
			_anims.playAnimation(_anims._WALK);
		}
		else if (Input.GetKeyUp (InputMan.KeyDown)) 
		{
			mod.y = -10f;
			_anims.playAnimation(_anims._STATIC);
		}
	}

	private void XboxInput()
	{
		if(Input.GetAxisRaw("X axis") > InputMan.X_AxisPos_Sensibility)
		{
			mod.x -= speed;
		}
		if(Input.GetAxisRaw("X axis") < InputMan.X_AxisNeg_Sensibility )
		{
			mod.x += speed;
		}
		
		if(Input.GetAxisRaw("Y axis") > InputMan.Y_AxisPos_Sensibility)
		{
			mod.y += speed;
		}
		if(Input.GetAxisRaw("Y axis") < InputMan.Y_AxisNeg_Sensibility)
		{
			mod.y -= speed;
		}

		/*
		if(Input.GetAxisRaw("6th axis") > InputMan.X_AxisPos_Sensibility )
		{
			mod.x -= speed;
		}
		if(Input.GetAxisRaw("6th axis") < InputMan.X_AxisNeg_Sensibility)
		{
			mod.x += speed;
		}
		
		if(Input.GetAxisRaw("7th axis") > InputMan.Y_AxisPos_Sensibility)
		{
			mod.y += speed;
		}
		if(Input.GetAxisRaw("7th axis") < InputMan.Y_AxisNeg_Sensibility)
		{
			mod.y -= speed;
		}
		*/

	}

	public void triggerNotification(float _value)
	{
		_notif.text = "+" + _value.ToString();
		_notif.makeFadeIn();
		StartCoroutine(WaitFadeSec(2f));
	}

	IEnumerator WaitFadeSec(float _time)
	{
		yield return new WaitForSeconds(_time);
		_notif.makeFadeOut();
	}

	private void GameStart()
	{
		speed = initSpeed;
		new OTTween(spr, 0.5f).Tween("alpha", 1f);
		new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
		_notif.makeFadeOut();
		OnPlatforms = 0;
	}
	
	private void GameOver()
	{
		speed = initSpeed;
		new OTTween(spr, 0.5f).Tween("alpha", 0f);
		new OTTween(spr, 0.5f).Tween("size", new Vector2(0.25f,0.25f));
		_notif.makeFadeOut();
		OnPlatforms = 0;
	}
	
	private void Respawn()
	{
		speed = initSpeed;
		gameObject.transform.position = startPos;
		new OTTween(spr, 0.5f).Tween("alpha", 1f);
		new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
		_notif.makeFadeOut();
//		OnPlatforms = 0;
	}
}
