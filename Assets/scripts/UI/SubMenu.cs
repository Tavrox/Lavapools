﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubMenu : MonoBehaviour {

	public enum submenus
	{
		Ingame,
		GameOver,
		EntryMenu
	}
	public submenus subMenuList;
	[HideInInspector] public List<GameObject> menuThings;
}
