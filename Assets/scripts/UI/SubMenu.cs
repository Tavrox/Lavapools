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
	public ParentMenu _parent;
	public MainMenu _menuMan;

	public void SetupSub(ParentMenu _menu)
	{
		_parent = _menu.GetComponent<ParentMenu>();
		if (_menu.GetComponent<MainMenu>() != null)
		{
			_menuMan = _menu.GetComponent<MainMenu>();
		}
	}
	public void setupBtn()
	{
		MiscButton[] childBtn = GetComponentsInChildren<MiscButton>();
		foreach (MiscButton child in childBtn)
		{
			child.Setup(_parent);
			menuButtons.Add(child);
			menuButtons.Sort(CompareListByName);
		}
	}
	
	private int CompareListByName(MiscButton i1, MiscButton i2)
	{
		return i1.name.CompareTo(i2.name); 
	}

	public MiscButton findNextBtn( MiscButton _btnSource)
	{
		MiscButton res = null;
		MiscButton lastBtn = menuButtons[menuButtons.Count-1];
		int currBtn = menuButtons.FindIndex(_btn => _btn == _btnSource);
		if (menuButtons[currBtn] == lastBtn)
		{
			res = menuButtons[0];
		}
		else
		{
			res = menuButtons[currBtn+1];
		}

		return res;
	}
	public MiscButton findPrevBtn( MiscButton _btnSource)
	{
		MiscButton res = null;
		MiscButton firstBtn = menuButtons[0];
		MiscButton lastBtn = menuButtons[menuButtons.Count-1];
		int lastBtnId = menuButtons.FindIndex(_btn => _btn == lastBtn);
		int currBtn = menuButtons.FindIndex(_btn => _btn == _btnSource);
		if (menuButtons[currBtn] == firstBtn)
		{
			res = menuButtons[lastBtnId];
		}
		else
		{
			res = menuButtons[currBtn-1];
		}

		return res;
	}

	public void appearAll(GameObject target = null)
	{
		TextUI[] listTxt = GetComponentsInChildren<TextUI>();
		OTSprite[] listSpr = GetComponentsInChildren<OTSprite>();
		foreach(TextUI txt in listTxt )
		{
			txt.color = txt.initColor;
		}
		foreach (OTSprite spr in listSpr)
		{
			spr.alpha = 1f;
		}
	}

	public void clearAll(GameObject target = null)
	{
		TextUI[] listTxt = GetComponentsInChildren<TextUI>();
		OTSprite[] listSpr = GetComponentsInChildren<OTSprite>();
		LBEntry[] listEntry = GetComponentsInChildren<LBEntry>();
		foreach(TextUI txt in listTxt)
		{
			txt.color = Color.clear;
		}
		foreach (OTSprite spr in listSpr)
		{
			spr.alpha = 0f;
		}
	}

}
