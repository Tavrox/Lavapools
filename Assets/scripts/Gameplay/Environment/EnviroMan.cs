using UnityEngine;
using System.Collections;

public class EnviroMan : MonoBehaviour {

	public GameObject Background;
	public GameObject Borders;
	public GameObject CollectiblePlaces;
	public GameObject MapBuilder;
	public GameObject SpaceGate;

	// Use this for initialization
	void Start () {

		Background = FETool.findWithinChildren(gameObject, "Background");
		Borders = FETool.findWithinChildren(gameObject, "Borders");
		CollectiblePlaces = FETool.findWithinChildren(gameObject, "CollectiblePlaces");
		MapBuilder = FETool.findWithinChildren(gameObject, "MapBuilder");
		SpaceGate = FETool.findWithinChildren(gameObject, "SpaceGate");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
