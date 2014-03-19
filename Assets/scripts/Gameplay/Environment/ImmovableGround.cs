using UnityEngine;
using System.Collections;

public class ImmovableGround : MonoBehaviour {
	
	private Player _player;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
	
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
