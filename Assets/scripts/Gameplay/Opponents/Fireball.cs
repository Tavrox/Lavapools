using UnityEngine;
using System.Collections;

public class Fireball : MonoBehaviour {

	public LevelTools.DirectionList Direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player") == true)
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.Fireball);
		}
	}
}
