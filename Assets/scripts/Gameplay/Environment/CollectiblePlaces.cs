using UnityEngine;
using System.Collections;

public class CollectiblePlaces : MonoBehaviour {

	public Collectible.ListCollectible _placeType;
	public bool occupied = false;
	public float distToPlayer;

	public void Spawn(LevelManager _lm)
	{
		if (occupied == false)
		{
			switch (_placeType)
			{
			case Collectible.ListCollectible.TinyGem :
			{

				GameObject gemObj = Instantiate(Resources.Load("Bricks/Environment/TinyGem")) as GameObject;
				TinyGem _gem = gemObj.GetComponent<TinyGem>();
				_gem.Setup(_lm, this);
				gemObj.transform.parent = gameObject.transform;
				gemObj.transform.position = gameObject.transform.position;
				_gem.value = LevelManager.GlobTuning.Gem_Value;
				_gem._relatedPlace = this;
				break;
			}
			case Collectible.ListCollectible.Gatepart :
			{
				GameObject partObj = Instantiate(Resources.Load("Bricks/Environment/Gatepart")) as GameObject;
				Gatepart _part = partObj.GetComponent<Gatepart>();
				_part.Setup(_lm, this);
				partObj.transform.parent = gameObject.transform;
				partObj.transform.position = gameObject.transform.position;
				_part.value = LevelManager.GlobTuning.Gatepart_Value;
				_part._relatedPlace = this;
				break;

			}
			}
			occupied = true;
		}
//		Debug.Log("Gem has spawned at " + gameObject.name);
	}
}
