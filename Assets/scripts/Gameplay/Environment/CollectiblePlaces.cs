using UnityEngine;
using System.Collections;

public class CollectiblePlaces : MonoBehaviour {

	public Collectible.ListCollectible _placeType;
	public bool occupied = false;

	public void Spawn(LevelManager _lm)
	{
		switch (_placeType)
		{
		case Collectible.ListCollectible.TinyGem :
		{
			GameObject gemObj = Instantiate(Resources.Load("Bricks/Environment/BigGem")) as GameObject;
			BigGem _gem = gemObj.GetComponent<BigGem>();
			_gem.Setup(_lm);
			gemObj.transform.parent = gameObject.transform;
			gemObj.transform.position = gameObject.transform.position;
			_gem._relatedPlace = this;
			break;
		}
		case Collectible.ListCollectible.BigGem :
		{
			
			break;
		}
		}
		occupied = true;
//		Debug.Log("Gem has spawned at " + gameObject.name);
	}
}
