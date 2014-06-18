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
	public OTAnimatingSprite _animSpr;

	// Use this for initialization
	public void Setup (LevelManager lm) {

		_levMan = lm;
	}

	public void Pop()
	{
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

			if (_animSpr != null)
			{
				new OTTween(_animSpr, 1f).Tween("alpha", 0f).Wait(1f);
				new OTTween(_animSpr, 0.5f, OTEasing.BackOut).Tween("depth", -15f);
				
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
					new OTTween(gameObject.transform, 1.5f).Tween("localScale", new Vector3(1.5f, 1.5f, 1f)).PingPong();
					if (_levMan.score < 25)
					{
						new OTTween(gameObject.transform, 1.5f, OTEasing.ExpoOut).Tween("position", StargatePlace.GetComponent<SpaceGate>().slotList[Mathf.FloorToInt(_levMan.score)].transform.position);
					}
					else
					{
						new OTTween(gameObject.transform, 1.5f, OTEasing.QuadIn).Tween("position", StargatePlace.GetComponent<SpaceGate>().defaultSlot.transform.position);
					}
					break;
				}
				}
			}
			Destroy (gameObject, 3f);
		}
	}

}
