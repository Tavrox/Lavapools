using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	public enum ListCollectible
	{
		BigGem,
		TinyGem,
		Gatepart
	};
	public enum PlacesCollect
	{
		Panel,
		Stargate
	};
	public PlacesCollect PlaceCollectibleToGo;
	public ListCollectible typeCollectible;
	public int value;
	public LevelManager _levMan;
	public CollectiblePlaces _relatedPlace;
	public bool picked = false;
	public bool busy = false;
	public OTSprite _spr;
	public OTAnimatingSprite _animSpr;

	// Use this for initialization
	public void Setup (LevelManager lm) {

		_levMan = lm;
		if (GetComponentInChildren<OTSprite>() != null)
		{
			_spr = GetComponentInChildren<OTSprite>();
		}
		if (GetComponentInChildren<OTAnimatingSprite>() != null)
		{
			_animSpr = GetComponentInChildren<OTAnimatingSprite>();
		}
		_spr.alpha = 0f;
	
	}

	public void Pop()
	{
		if (_spr != null)
		{
			new OTTween(_spr, 1f).Tween("alpha", 1f);
		}
		if (_animSpr != null)
		{
			new OTTween(_animSpr, 1f).Tween("alpha", 1f);
		}
	}

	public void Vanish()
	{
		if (picked == false && busy == false)
		{
			picked = true;

			if (_spr != null)
			{
				new OTTween(_spr, 1f).Tween("alpha", 0f).Wait(1f);
				new OTTween(_spr, 0.5f, OTEasing.BackOut).Tween("depth", -15f);
			}

			if (_animSpr != null)
			{
				new OTTween(_animSpr, 1f).Tween("alpha", 0f).Wait(1f);
				new OTTween(_animSpr, 0.5f, OTEasing.BackOut).Tween("depth", -15f);
			}

			if (_animSpr != null || _spr != null)
			{
				
				switch (PlaceCollectibleToGo)
				{
				case PlacesCollect.Panel :
				{
					
					new OTTween(gameObject.transform, 1.5f, OTEasing.BackOut).Tween("localScale", new Vector3(1.5f, 1.5f, 1f)).PingPong();
					new OTTween(gameObject.transform, 1.5f, OTEasing.BackIn).Tween("position", new Vector3(0f,5.5f,0f));
					break;
				}
				case PlacesCollect.Stargate :
				{
					GameObject StargatePlace = GameObject.FindGameObjectWithTag("SpaceGate");
					new OTTween(gameObject.transform, 1.5f, OTEasing.BackOut).Tween("localScale", new Vector3(1.5f, 1.5f, 1f)).PingPong();
					new OTTween(gameObject.transform, 1.5f, OTEasing.BackIn).Tween("position", StargatePlace.transform.position);
					break;
				}
				}
			}
			Destroy (gameObject, 3f);
		}
	}

}
