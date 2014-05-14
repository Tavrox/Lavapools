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
	
	// Use this for initialization
	public void Setup (LevelManager _lm, CollectiblePlaces _place) 
	{
		base.Setup(_lm);
		thisType = partType.Spawner;
		collPlace = _place;
		PlaceCollectibleToGo = PlacesCollect.Stargate;
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
			}
			else
			{
				_levMan.collecSum += 1f;
				Vanish();
			}
		}
	}

	public void fadeAfterFail(float _time)
	{
		thisType = partType.Recup;
		busy = true;
		StartCoroutine(Unbusy(_time));
	}

	IEnumerator Unbusy(float _time)
	{
		yield return new WaitForSeconds(_time);
		if (_spr != null)
		{
			new OTTween(this._spr, _time * 4f).Tween("alpha", 0f);
		}
		else if (_animSpr != null)
		{
			new OTTween(this._animSpr, _time * 4f).Tween("alpha", 0f);
		}
		busy = false;
		Destroy(gameObject, _time * 3f);
	}
}
