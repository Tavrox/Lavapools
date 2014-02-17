using UnityEngine;
using System.Collections;

public class ImmovableGround : LevelBrick {

	// Use this for initialization
	void Start () {

		base.Setup();
	
	}

	public void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			_player.OnPlatforms += 1;
		}
	}
	public void OnTriggerExit(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			_player.OnPlatforms -= 1;
		}
	}
}
