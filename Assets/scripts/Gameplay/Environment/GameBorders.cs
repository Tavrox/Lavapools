using UnityEngine;
using System.Collections;

public class GameBorders : MonoBehaviour {

	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.GameBorders);
		}
		if (_other.GetComponent<Arrow>() != null)
		{
			_other.GetComponent<Arrow>().Reset();
		}
	}
}
