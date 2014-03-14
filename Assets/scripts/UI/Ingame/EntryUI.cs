using UnityEngine;
using System.Collections;

public class EntryUI : SubMenu {

	private EnterGame _EnterUI;
	private UIThing _Logo;
	private GameObject _Objective;

	// Use this for initialization
	public void Setup () 
	{
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		_EnterUI = FETool.findWithinChildren(this.gameObject, "EnterGame").GetComponent<EnterGame>();
		_Logo = FETool.findWithinChildren(this.gameObject, "Logo").GetComponent<UIThing>();
		_Objective = FETool.findWithinChildren(this.gameObject, "Objective");
	}

	public void GameStart()
	{
		
	}
	public void GameOver()
	{
		_EnterUI.gameObject.SetActive(false);
		_Objective.gameObject.SetActive(false);
		_Logo.fadeSprite(1f, 0.3f);
	}
	public void Respawn()
	{
		_EnterUI.gameObject.SetActive(false);
		_Objective.gameObject.SetActive(false);
		_Logo.fadeSprite(0f, 0.3f);
	}
}
