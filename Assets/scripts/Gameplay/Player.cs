using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public LPTuning TuningDocument;
	[HideInInspector] public int OnPlatforms;

	public float speed = 0.1f;
	public float initSpeed;
	private RaycastHit hit;
	private Vector3 pos;
	private int layer;
	private int layerMask;
	private Bounds rect;
	private Vector3 startPos;
	private OTSprite spr;
	private Vector2 originalSize;
	private Notification _notif;
	private PlayerAnims _anims;

	[HideInInspector] public enum playerState
	{
		Walk,
		Static
	};
	[HideInInspector] public playerState _state;

	private UserLeaderboard _playerSheet;

	public string playerName;
	
	private RaycastHit hitInfo; //infos de collision
	private Ray detectTargetLeft, detectTargetRight; //point de départ, direction
	
	// Use this for initialization
	void Start () {
		
		if (GetComponentInChildren<OTSprite>() != null)
		{
			spr = GetComponentInChildren<OTSprite>();
			spr.alpha = 1f;
			originalSize = spr.size;
		}
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_playerSheet = ScriptableObject.CreateInstance("UserLeaderboard") as UserLeaderboard;
		_playerSheet.name = playerName;
		TuningDocument = LevelManager.TuningDocument;
		speed = TuningDocument.Player_Speed;
		initSpeed = speed;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		_anims = gameObject.AddComponent<PlayerAnims>() as PlayerAnims;
		_notif = GetComponentInChildren<Notification>();

		startPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {	
	
		Vector3 mod = new Vector3(0f,0f,0f);
		pos = gameObject.transform.position;
//		print ("Platforms" + OnPlatforms);

		if (LevelManager.GAMESTATE == GameEventManager.GameState.Live )
		{
			if (OnPlatforms == 0 )
			{
				GameEventManager.TriggerGameOver(gameObject.name);
			}

			if (Input.GetKey (KeyCode.RightArrow)) 
			{
				mod.x += speed;
				_anims.playAnimation(_anims._WALK);
			}
			else if (Input.GetKeyUp (KeyCode.RightArrow)) 
			{
				mod.x =0f;
				_anims.playAnimation(_anims._STATIC);
			}
			
			if (Input.GetKey (KeyCode.UpArrow)) 
			{
				mod.y += speed;
				_anims.playAnimation(_anims._WALK);
			}
			else if (Input.GetKeyUp (KeyCode.UpArrow)) 
			{
				mod.y = 0f;
				_anims.playAnimation(_anims._STATIC);
			}
			
			if (Input.GetKey (KeyCode.LeftArrow)) 
			{
				mod.x -= speed;
				_anims.playAnimation(_anims._WALK);
			}
			else if (Input.GetKeyUp (KeyCode.LeftArrow)) 
			{
				mod.x =0f;
				_anims.playAnimation(_anims._STATIC);
			}
			
			if (Input.GetKey (KeyCode.DownArrow)) 
			{
				mod.y -= speed;
				_anims.playAnimation(_anims._WALK);
			}
			else if (Input.GetKeyUp (KeyCode.DownArrow)) 
			{
				mod.x =0f;
				_anims.playAnimation(_anims._STATIC);
			}
			this.gameObject.transform.position += mod * Time.deltaTime;
		}
	}

	public void triggerNotification()
	{
		_notif.text = "+" + TuningDocument.CapturePoint_Score.ToString();
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
		OnPlatforms = 0;
	}
}
