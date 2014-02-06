using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubMenu : MonoBehaviour {

	public enum submenus
	{
		Ingame,
		Leaderboards,
		Levels
	}
	public submenus subMenuList;
	[HideInInspector] public List<GameObject> menuThings;

	// Use this for initialization
	void Start () {

//		GameObject[] _thingArray = GetComponentsInChildren<GameObject>();
//		foreach (GameObject _thing in _thingArray)
//		{
//			menuThings.Add(_thing);
//			Debug.Log(_thing.name);
//		}	
	}

	void OnEnable()
	{
//		print (name + "Enabled");
	}

	void OnDisable()
	{
//		print (name + "disabled");
	}
}
