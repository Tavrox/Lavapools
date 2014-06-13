using UnityEngine;
using System.Collections;

public class BladePart : MonoBehaviour {

	public LevelTools.DirectionList Direction;
	public bool killing;

	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player") == true && killing == true)
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.BladePart);
		}
	}

	public void startKiller()
	{
		StartCoroutine(delayKiller());
	}

	IEnumerator delayKiller()
	{
		yield return new WaitForSeconds(2f);
		killing = true;
	}
}
