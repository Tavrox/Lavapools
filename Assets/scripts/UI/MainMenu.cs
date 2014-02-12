using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
	
	private List<GameObject> listMenus;
	[HideInInspector] public GameObject IngameUI;
	[HideInInspector] public GameObject LeaderboardUI;
	[HideInInspector] public GameObject RespawnUI;
	[HideInInspector] public GameObject Logo;

	// Use this for initialization
	void Awake () {
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		IngameUI = FETool.findWithinChildren(this.gameObject, "Ingame");
		LeaderboardUI = FETool.findWithinChildren(this.gameObject, "Leaderboard");
		RespawnUI = FETool.findWithinChildren(this.gameObject, "Respawn");
		Logo = FETool.findWithinChildren(this.gameObject, "Logo");

	}

	void Start()
	{

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

	public void MakeFadeOut(UIThing _thing)
	{
		StartCoroutine(CoFadeOut(_thing));
	}

	IEnumerator CoFadeOut(UIThing _thing)
	{
		yield return new WaitForSeconds(2f);
		_thing.gameObject.SetActive(false);
	}

	private void GameStart()
	{
		LeaderboardUI.gameObject.SetActive(false);
		Logo.gameObject.SetActive(false);
		RespawnUI.gameObject.SetActive(false);
	}
	
	private void GameOver()
	{
//		LeaderboardUI.gameObject.SetActive(true);
		RespawnUI.gameObject.SetActive(true);
	}
	
	private void Respawn()
	{
//		LeaderboardUI.gameObject.SetActive(false);
		RespawnUI.gameObject.SetActive(false);
	}
}