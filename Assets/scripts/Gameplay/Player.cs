using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public LPTuning TuningDocument;
	[HideInInspector] public int OnPlatforms;

	private float speed = 0.1f;
	private RaycastHit hit;
	private Vector3 pos;
	private int layer;
	private int layerMask;
	private Bounds rect;
	private Vector3 startPos;
	private OTSprite spr;
	private Vector2 originalSize;
	private Label _notif;

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
		TuningDocument = _levMan.TuningDocument;
		speed = TuningDocument.Player_Speed;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
	
		_notif = GetComponentInChildren<Label>();
		startPos = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {	
	
		Vector3 mod = new Vector3(0f,0f,0f);
		pos = gameObject.transform.position;
//		print ("Platforms" + OnPlatforms);
		
		if (OnPlatforms == 0 )
		{
			GameEventManager.TriggerGameOver(gameObject.name);
		}

		if (Input.GetKey (KeyCode.RightArrow)) 
		{
			mod.x += speed;
		}
		else if (Input.GetKeyUp (KeyCode.RightArrow)) 
		{
			mod.x =0f;
		}
		
		if (Input.GetKey (KeyCode.UpArrow)) 
		{
			mod.y += speed;
		}
		else if (Input.GetKeyUp (KeyCode.UpArrow)) 
		{
			mod.y = 0f;
		}
		
		if (Input.GetKey (KeyCode.LeftArrow)) 
		{
			mod.x -= speed;
		}
		else if (Input.GetKeyUp (KeyCode.LeftArrow)) 
		{
			mod.x =0f;
		}
		
		if (Input.GetKey (KeyCode.DownArrow)) 
		{
			mod.y -= speed;
		}
		else if (Input.GetKeyUp (KeyCode.DownArrow)) 
		{
			mod.x =0f;
		}
		this.gameObject.transform.position += mod * Time.deltaTime;
	}

	public void triggerNotification()
	{
		_notif.makeFadeIn();
		StartCoroutine(WaitFadeSec(3f));
	}

	IEnumerator WaitFadeSec(float _time)
	{
		yield return new WaitForSeconds(_time);
		_notif.makeFadeOut();
	}
	
	private void GameStart()
	{
		new OTTween(spr, 0.5f).Tween("alpha", 1f);
		new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
		OnPlatforms = 0;
	}
	
	private void GameOver()
	{
		if (FEDebug.GodMode != true)
		{
			new OTTween(spr, 0.5f).Tween("alpha", 0f);
			new OTTween(spr, 0.5f).Tween("size", new Vector2(0.25f,0.25f));
			OnPlatforms = 0;
		}
		else
		{
			Debug.Log("God Mode is activated");
		}
	}
	
	private void Respawn()
	{
		gameObject.transform.position = startPos;
		new OTTween(spr, 0.5f).Tween("alpha", 1f);
		new OTTween(spr, 0.5f).Tween("size", new Vector2(originalSize.x,originalSize.y));
		OnPlatforms = 0;
	}
}
