using UnityEngine;
using System.Collections;

public class SpaceGate : MonoBehaviour {

	private OTSprite _spr;

	public void Setup()
	{
		_spr = GetComponentInChildren<OTSprite>();
		GameEventManager.Respawn += Respawn;

	}

	public void collectPart(float _score)
	{
		_score +=1;
		if (_score < 10)
		{
			_spr.frameName = "gate" + "0" +  _score +"load";

		}
		else
		{
			_spr.frameName = "gate" + _score +"load";
		}

	}

	private void Respawn()
	{
		_spr.frameName = "gate00load";
	}

}
