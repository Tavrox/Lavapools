using UnityEngine;
using System.Collections;

public class EnterGame : MonoBehaviour {


	public void Setup(LevelInfo _currLvl)
	{
		FETool.findWithinChildren(gameObject, "Level").GetComponent<TextUI>().text = LevelManager.CurrentLevelInfo.levelID + " - " + LevelManager.CurrentLevelInfo.LvlName.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (this != null)
		{
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown(LevelManager.InputMan.EnterButton)) 
			{
				GameEventManager.TriggerRespawn("Enter Game");
			}
		}
	}
}
