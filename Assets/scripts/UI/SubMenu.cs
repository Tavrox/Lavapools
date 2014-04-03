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
	[HideInInspector] public List<GameObject> menuThings;
	public MainMenu _menuMan;

	public void SetupSub(MainMenu _menu)
	{
		_menuMan = _menu;
	}
}
