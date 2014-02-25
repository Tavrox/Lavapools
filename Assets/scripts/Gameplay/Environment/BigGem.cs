using UnityEngine;
using System.Collections;

public class BigGem : Collectible {

	// Use this for initialization
	public void Setup (LevelManager _lm) 
	{
		base.Setup(_lm);
		Pop();
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			_levMan.tools.CollectObject(this);
			_relatedPlace.occupied = false;
			Vanish();
		}
	}
}
