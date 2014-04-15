using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player"))
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.Arrow);
		}
	}
}
