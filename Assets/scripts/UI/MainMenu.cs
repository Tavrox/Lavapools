using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {
	
	private List<GameObject> listMenus;

	[HideInInspector] public IngameUI _IngameUI;
	[HideInInspector] public GameOverUI _GameOverUI;
	[HideInInspector] public EntryUI _EntryUI;

	// Use this for initialization
	public void Setup () {
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		_IngameUI = FETool.findWithinChildren(this.gameObject, "Ingame").GetComponent<IngameUI>();
		_GameOverUI = FETool.findWithinChildren(this.gameObject, "GameOver").GetComponent<GameOverUI>();
		_EntryUI = FETool.findWithinChildren(this.gameObject, "EntryMenu").GetComponent<EntryUI>();

		_IngameUI.Setup();
		_GameOverUI.Setup();
		_EntryUI.Setup();

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

	}
	
	private void GameOver()
	{

	}
	
	private void Respawn()
	{

	}
}