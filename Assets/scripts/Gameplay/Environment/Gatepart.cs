using UnityEngine;
using System.Collections;

public class Gatepart : Collectible {

	private CollectiblePlaces collPlace;
	public enum partType
	{
		Spawner,
		Recup
	};
	public partType thisType;
	public OTAnimatingSprite Taken;
	
	// Use this for initialization
	public void Setup (LevelManager _lm, CollectiblePlaces _place) 
	{
		base.Setup(_lm);
		thisType = partType.Spawner;
		collPlace = _place;
		name = "Gatepart|" + collPlace.name + Random.Range(0,100).ToString();
		PlaceCollectibleToGo = PlacesCollect.Stargate;
		_animSpr = FETool.findWithinChildren(this.gameObject, "Part").GetComponentInChildren<OTAnimatingSprite>();
		Taken = FETool.findWithinChildren(this.gameObject, "Taken").GetComponentInChildren<OTAnimatingSprite>();
		Taken.alpha = 0f;
		Pop();
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player") && picked == false)
		{
			if (thisType == partType.Spawner)
			{
				collPlace.occupied = false;
				_levMan.tools.CollectObject(this);
				_levMan.triggerSpawnGem(collPlace);
				MasterAudio.PlaySound("door_piece_pick");
				Vanish();
				Taken.alpha = 1f;
			}
			else
			{
				_levMan.collecSum += 1f;
				Vanish();
			}
		}
	}
}
