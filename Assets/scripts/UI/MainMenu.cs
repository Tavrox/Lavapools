using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
	
	private List<GameObject> listMenus;
	private GameObject IngameUI;
	private GameObject LeaderboardUI;
	private GameObject RespawnUI;

	// Use this for initialization
	void Awake () {

		IngameUI = FETool.findWithinChildren(this.gameObject, "Ingame");
		LeaderboardUI = FETool.findWithinChildren(this.gameObject, "Leaderboard");
		RespawnUI = FETool.findWithinChildren(this.gameObject, "Respawn");

		LeaderboardUI.gameObject.SetActive(false);
		RespawnUI.gameObject.SetActive(false);

	}

	void Start()
	{
		
//		LeaderboardUI.gameObject.SetActive(true);
//		RespawnUI.gameObject.SetActive(true);

	}

	
	public void swapToMenu()
	{

	}

	private List<GameObject> buildChildrenList()
	{

		List<GameObject> childrenList = new List<GameObject>();
		/***** TO FIX ****
		GameObject[] listCompo = GetComponentsInChildren<GameObject>();
		foreach (GameObject _compo in listCompo)
		{

			if (_compo.tag != "Submenu")
			{
				childrenList.Add(_compo.gameObject);
				Debug.Log(_compo.gameObject.name);
			}
			Debug.Log(_compo.name);
		}
		*/
		return childrenList;
	}
}