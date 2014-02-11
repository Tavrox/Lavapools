using UnityEngine;
using System.Collections;

public class Procedural : MonoBehaviour {


	private LevelManager _levMan;

	// Use this for initialization
	void Awake () {
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
//		InvokeRepeating("checkScore", 0f, 3f);
	
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
