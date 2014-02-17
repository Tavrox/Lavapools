using UnityEngine;
using System.Collections;

public class IngameUI : SubMenu {

	public Label Score;
	public Label ScoreTxt;
	
	public Label LevelTxt;
	
	public Label BestScore;
	public Label BestScoreTxt;

	public void Setup()
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		Score = FETool.findWithinChildren(this.gameObject, "Score/Score_int").GetComponent<Label>();
		ScoreTxt = FETool.findWithinChildren(this.gameObject, "Score/Score_txt").GetComponent<Label>();

		LevelTxt = FETool.findWithinChildren(this.gameObject, "LevelLabel/LevelLabel_txt").GetComponent<Label>();

		BestScore = FETool.findWithinChildren(this.gameObject, "BestScore/BestScore_int").GetComponent<Label>();
		BestScoreTxt = FETool.findWithinChildren(this.gameObject, "BestScore/BestScore_txt").GetComponent<Label>();

	}

	public void GameStart()
	{
		
	}
	public void GameOver()
	{
		
	}
	public void Respawn()
	{
		
	}

}
