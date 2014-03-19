using UnityEngine;
using System.Collections;

public class BigGem : Collectible {

	// Use this for initialization
	public void Setup (LevelManager _lm) 
	{
		base.Setup(_lm);
		_spr.alpha = 1f;
		_animSpr.animationFrameset = "BigGemBounce";
		_animSpr.PlayLoop("BigGemBounce");
//		value = LevelManager..BigGem_Value;
	}
	
	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			gameObject.transform.parent = GameObject.Find("LevelManager/GarbagePool").gameObject.transform.parent;
			_levMan.tools.CollectObject(this);
			Vanish();
		}
	}
}
