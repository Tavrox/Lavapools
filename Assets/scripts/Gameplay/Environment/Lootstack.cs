using UnityEngine;
using System.Collections;

public class Lootstack : MonoBehaviour {

	public int stackValue;
	public enum StateList
	{
		Picked,
		Unpicked
	};
	public StateList State;
	private BoxCollider coll;
	private OTSprite spr;
	
	// Use this for initialization
	public void Setup () 
	{
		spr = GetComponentInChildren<OTSprite>();
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider _oth) 
	{
		if (_oth.CompareTag("Player") == true && State != StateList.Picked)
		{
			State = StateList.Picked;
			Player pl = _oth.GetComponent<Player>();
			pl.lootStack(stackValue);
			pl._levMan.tools.lootStack(stackValue);
			Fade();
		}
	}

	private void Fade()
	{
		spr = GetComponentInChildren<OTSprite>();
		new OTTween(spr, 1f).Tween("alpha", 0f);
		GetComponent<BoxCollider>().enabled = false;
	}

	private void GameStart()
	{
		if (this != null)
		{
			
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			GetComponent<BoxCollider>().enabled = false;
			spr.alpha = 0f;
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			GetComponent<BoxCollider>().enabled = true;
			spr.alpha = 1f;
		}
	}
}
