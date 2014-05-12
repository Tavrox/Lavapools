using UnityEngine;
using System.Collections;

public class BladePart : MonoBehaviour {

	public LevelTools.DirectionList Direction;

	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player") == true)
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.Fireball);
		}
	}
}
