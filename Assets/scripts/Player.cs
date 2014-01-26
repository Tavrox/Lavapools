using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	private float speed = 0.1f;
	public bool safe = true;
	private RaycastHit hit;
	private Vector3 pos;
	private int layer;
	private int layerMask;
	private Bounds rect;
	public int OnPlatforms;
	private Vector3 startPos;
	
	private RaycastHit hitInfo; //infos de collision
	private Ray detectTargetLeft, detectTargetRight; //point de départ, direction
	
	// Use this for initialization
	void Start () {
		
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
	
		startPos = gameObject.transform.position;
		layer =  3;
		layerMask =  1 << layer;
	}
	
	// Update is called once per frame
	void Update () {	
	
		Vector3 mod = new Vector3(0f,0f,0f);
		pos = gameObject.transform.position;
		
		if (OnPlatforms ==0)
		{
			GameEventManager.TriggerGameOver();
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
		
		if (Input.GetKey (KeyCode.A)) 
		{
			GameEventManager.TriggerRespawn();
		}
		this.gameObject.transform.position += mod;
	}
	
	private void GameStart()
	{
		enabled = true;
	}
	
	private void GameOver()
	{
		enabled = false;
	}
	
	private void Respawn()
	{
		gameObject.transform.position = startPos;
	}
}
