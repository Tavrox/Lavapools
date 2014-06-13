using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Lootstack : MonoBehaviour {

	public int stackValue;
	private LevelManager levMan;
	public enum StateList
	{
		Picked,
		Unpicked
	};
	public StateList State;
	private BoxCollider coll;
	private OTSprite spr;
	public List<BrickStepParam> paramToTrigger;
	
	// Use this for initialization
	public void Setup (LevelManager _lev) 
	{
		levMan = _lev;
		paramToTrigger = new List<BrickStepParam>();
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
			pl._levMan.tools.checkLevelCompletion();
			pl.triggerNotification(stackValue * 1);
			Fade();
		}
	}

	public void Fade()
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
			State = StateList.Unpicked;
			GetComponent<BoxCollider>().enabled = true;
			spr.alpha = 1f;
		}
	}
}
