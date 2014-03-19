using UnityEngine;
using System.Collections;

public class ColliderKiller : MonoBehaviour {

	private PatrolBrick _parent;

	// Use this for initialization
	public void Setup (PatrolBrick _par) {

		_parent = _par;
	
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player") && LevelManager.GAMESTATE != GameEventManager.GameState.MainMenu)
		{
			if (_parent.type == LevelBrick.typeList.Bird)
			{
				GameEventManager.TriggerGameOver(LevelTools.KillerList.Bird);
			}
			if (_parent.type == LevelBrick.typeList.Chainsaw)
			{
				GameEventManager.TriggerGameOver(LevelTools.KillerList.Chainsaw);
			}
		}
	}
}
