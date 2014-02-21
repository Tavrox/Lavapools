using UnityEngine;
using System.Collections;

public class IngameUI : SubMenu {

	public TextUI Score;
	public TextUI ScoreTxt;
	
	public TextUI LevelTxt;
	
	public TextUI BestScore;
	public TextUI BestScoreTxt;

	public Vector3 initPos;
	public Vector3 outPos;

	public void Setup()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		initPos = gameObject.transform.position;
		outPos = new Vector3 (initPos.x, initPos.y + 1f, initPos.z);

		Score = FETool.findWithinChildren(this.gameObject, "Score/Score_int").GetComponent<TextUI>();
		ScoreTxt = FETool.findWithinChildren(this.gameObject, "Score/Score_txt").GetComponent<TextUI>();

		LevelTxt = FETool.findWithinChildren(this.gameObject, "LevelLabel/LevelLabel_txt").GetComponent<TextUI>();

		BestScore = FETool.findWithinChildren(this.gameObject, "BestScore/BestScore_int").GetComponent<TextUI>();
		BestScoreTxt = FETool.findWithinChildren(this.gameObject, "BestScore/BestScore_txt").GetComponent<TextUI>();

	}

	public void GameStart()
	{
		new OTTween(gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", outPos);
	}
	public void GameOver()
	{
		new OTTween(gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", outPos);
	}
	public void Respawn()
	{
		new OTTween(gameObject.transform, 0.3f, OTEasing.QuadIn ).Tween("position", initPos);
	}

}
