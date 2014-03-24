using UnityEngine;
using System.Collections;

public class TinyGem : Collectible {

	private CollectiblePlaces collPlace;

	// Use this for initialization
	public void Setup (LevelManager _lm, CollectiblePlaces _place) 
	{
		base.Setup(_lm);
		collPlace = _place;
		PlaceCollectibleToGo = PlacesCollect.Panel;
		Pop();
		value = LevelManager.GlobTuning.Gem_Value;
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player") && picked == false)
		{
			_levMan.tools.CollectObject(this);
			collPlace.occupied = false;
			_levMan.triggerSpawnGem(collPlace);
			Vanish();
		}
	}
}
