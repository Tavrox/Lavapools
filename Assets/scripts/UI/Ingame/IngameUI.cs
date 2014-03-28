﻿using UnityEngine;
using System.Collections;

public class IngameUI : SubMenu {

	public TextUI Score;
	public TextUI ScoreTxt;
	
	public TextUI LevelTxt;
	
	public TextUI BestScore;
	public TextUI BestScoreTxt;

	public Vector3 initPos;
	public Vector3 outPos;

	public bool iscreated = false;

	public void Setup()
	{
		print ("setup ingame ui");

		initPos = gameObject.transform.position;
		outPos = new Vector3 (initPos.x, initPos.y + 1f, initPos.z);

		Score = FETool.findWithinChildren(this.gameObject, "Score/Score_int").GetComponent<TextUI>();
		ScoreTxt = FETool.findWithinChildren(this.gameObject, "Score/Score_txt").GetComponent<TextUI>();

		LevelTxt = FETool.findWithinChildren(this.gameObject, "LevelLabel/LevelLabel_txt").GetComponent<TextUI>();

		BestScore = FETool.findWithinChildren(this.gameObject, "BestScore/BestScore_int").GetComponent<TextUI>();
		BestScoreTxt = FETool.findWithinChildren(this.gameObject, "BestScore/BestScore_txt").GetComponent<TextUI>();

		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

	}

	void GameStart()
	{
		if (this != null && gameObject != null)
		{
			new OTTween(gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", outPos);
		}
	}
	void GameOver()
	{
		if (this != null)
		{
			new OTTween(gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", outPos);
		}
	}
	void Respawn()
	{
		if (this != null)
		{
			new OTTween(gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", initPos);
		}
	}

}
