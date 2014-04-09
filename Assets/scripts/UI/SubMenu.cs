using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubMenu : MonoBehaviour {

	public enum submenus
	{
		Ingame,
		GameOver,
		EntryMenu,
		EndGame
	}
	public submenus subMenuList;
	public List<MiscButton> menuButtons;
	public MainMenu _menuMan;

	public void SetupSub(MainMenu _menu)
	{
		_menuMan = _menu;
	}
	public void setupBtn()
	{
		MiscButton[] childBtn = GetComponentsInChildren<MiscButton>();
		foreach (MiscButton child in childBtn)
		{
			menuButtons.Add(child);
		}
	}

}
