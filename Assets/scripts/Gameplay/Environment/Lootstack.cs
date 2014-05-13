using UnityEngine;
using System.Collections;

public class Lootstack : MonoBehaviour {

	private int stackValue;
	private BoxCollider coll;

	// Use this for initialization
	public void Setup (int _st) 
	{
		stackValue = _st;
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider _oth) 
	{
		if (_oth.CompareTag("Player") == true)
		{
			Player pl = _oth.GetComponent<Player>();
			pl.lootStack(stackValue);
			pl._levMan.tools.lootStack(stackValue);

		}
	}
}
