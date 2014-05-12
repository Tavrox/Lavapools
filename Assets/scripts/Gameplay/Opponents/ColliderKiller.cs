using UnityEngine;
using System.Collections;

public class ColliderKiller : MonoBehaviour {

	private LevelBrick _parent;

	// Use this for initialization
	public void Setup (LevelBrick _par) 
	{
		_parent = _par;
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player") && LevelManager.GAMESTATE != GameEventManager.GameState.MainMenu)
		{
			if (_parent != null)
			{
				if (_parent.type == LevelBrick.typeList.Bird)
				{
	//				_parent._levMan.tools.tryDeath(LevelTools.KillerList.Bird);
					GameEventManager.TriggerGameOver(LevelTools.KillerList.Bird);
				}
				if (_parent.type == LevelBrick.typeList.Chainsaw)
				{
	//				_parent._levMan.tools.tryDeath(LevelTools.KillerList.Chainsaw);
					GameEventManager.TriggerGameOver(LevelTools.KillerList.Chainsaw);
				}
				if (_parent.type == LevelBrick.typeList.BladeTower)
				{
					//				_parent._levMan.tools.tryDeath(LevelTools.KillerList.Chainsaw);
					GameEventManager.TriggerGameOver(LevelTools.KillerList.Fireball);
				}
			}
		}
	}
}
