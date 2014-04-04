using UnityEngine;
using System.Collections;

public class EntryUI : SubMenu {

	private EnterGame _EnterUI;
//	private GameObject _Objective;

	// Use this for initialization
	public void Setup () 
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		_EnterUI = FETool.findWithinChildren(this.gameObject, "EnterGame").GetComponent<EnterGame>();
		_EnterUI.Setup(LevelManager.CurrentLevelInfo);
//		_Objective = FETool.findWithinChildren(this.gameObject, "Objective");
	}

	public void GameStart()
	{
		
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
