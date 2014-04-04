using UnityEngine;
using System.Collections;

public class ImmovableGround : MonoBehaviour {
	
	private Player _player;
	public bool killer;

	// Use this for initialization
	void Start () 
	{
		_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		GameEventManager.Respawn += Respawn;
		GameEventManager.GameOver += GameOver;
	
	}

	public void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player") && _other != null & this != null)
		{
			_other.GetComponent<Player>().OnPlatforms += 1;
		}
	}
	public void OnTriggerExit(Collider _other)
	{
		if (_other.CompareTag("Player") && _other != null & this != null)
		{
			_other.GetComponent<Player>().OnPlatforms -= 1;
		}
	}

	public void GameOver()
	{
		if (this == null)
		{
			GetComponent<BoxCollider>().enabled = false;
			enabled = false;
//			Destroy(this);
		}
	}

	public void Respawn()
	{
		if (this == null)
		{
			_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
			GetComponent<BoxCollider>().enabled = true;
			enabled = true;
//			Destroy(this);
		}
	}
}
