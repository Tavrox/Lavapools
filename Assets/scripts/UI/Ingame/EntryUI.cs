using UnityEngine;
using System.Collections;

public class EntryUI : SubMenu {

	private EnterGame _EnterUI;
//	private GameObject _Objective;

	// Use this for initialization
	public void Setup () 
	{
		_EnterUI = FETool.findWithinChildren(this.gameObject, "EnterGame").GetComponent<EnterGame>();
		_EnterUI.Setup(LevelManager.CurrentLevelInfo);

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

//		_Objective = FETool.findWithinChildren(this.gameObject, "Objective");
	}

	public void GameStart()
	{
		switch (LevelManager.LocalTuning.levelType)
		{
		case LevelParameters.levelTypeList.Debuggin :
		{
			_EnterUI.Objective.TranslateThis("OBJECTIVE");
			break;
		}
		case LevelParameters.levelTypeList.Linear :
		{
			_EnterUI.Objective.TranslateThis("OBJECTIVE");
			break;
		}
		case LevelParameters.levelTypeList.Maze :
		{
			_EnterUI.Objective.TranslateThis("OBJECTIVE");
			break;
		}
		case LevelParameters.levelTypeList.Procedural :
		{
			_EnterUI.Objective.TranslateThis("OBJECTIVE");
			break;
		}
		}
	}

	public void GameOver()
	{
		if (this != null && gameObject.activeInHierarchy == true)
		{
			_EnterUI.gameObject.SetActive(false);
	//		_Objective.gameObject.SetActive(false);
		}
	}
	public void Respawn()
	{
		if (this != null && gameObject.activeInHierarchy == true)
		{
			_EnterUI.gameObject.SetActive(false);
	//		_Objective.gameObject.SetActive(false);
		}
	}
}
