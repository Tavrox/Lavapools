using UnityEngine;
using System.Collections;

public class Gatepart : Collectible {

	private CollectiblePlaces collPlace;
	
	// Use this for initialization
	public void Setup (LevelManager _lm, CollectiblePlaces _place) 
	{
		base.Setup(_lm);
		collPlace = _place;
		PlaceCollectibleToGo = PlacesCollect.Stargate;
		Pop();
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player") && picked == false)
		{
			collPlace.occupied = false;
			_levMan.tools.CollectObject(this);
			_levMan.triggerSpawnGem(collPlace);
			MasterAudio.PlaySound("door_piece_pick");
			Vanish();
		}
	}
}
