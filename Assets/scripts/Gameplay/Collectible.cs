using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	public enum ListCollectible
	{
		BigGem,
		TinyGem
	};
	public ListCollectible typeCollectible;
	public int value;
	private LevelManager _levMan;

	// Use this for initialization
	void Setup (LevelManager lm) {

		_levMan = lm;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			_levMan.tools.CollectObject(this);
		}
	}
}
