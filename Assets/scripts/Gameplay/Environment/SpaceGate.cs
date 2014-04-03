using UnityEngine;
using System.Collections;

public class SpaceGate : MonoBehaviour {

	private OTSprite _spr;

	private GameObject firstStep;
	private GameObject secondStep;
	private GameObject thirdStep;

	private OTSprite[] spriteFirstStep = new OTSprite[0];
	private OTSprite[] spriteSecondStep = new OTSprite[0];
	private OTSprite[] spriteThirdStep = new OTSprite[0];

	public void Setup()
	{
		_spr = FETool.findWithinChildren(gameObject, "TheGate").GetComponentInChildren<OTSprite>();
		GameEventManager.Respawn += Respawn;

		firstStep = FETool.findWithinChildren(gameObject, "ExitLoc/1");
		secondStep = FETool.findWithinChildren(gameObject, "ExitLoc/2");
		thirdStep = FETool.findWithinChildren(gameObject, "ExitLoc/3");

		spriteFirstStep = firstStep.GetComponentsInChildren<OTSprite>();
		spriteSecondStep = secondStep.GetComponentsInChildren<OTSprite>();
		spriteThirdStep = thirdStep.GetComponentsInChildren<OTSprite>();

		fadeOutSprites(spriteFirstStep);
		fadeOutSprites(spriteSecondStep);
		fadeOutSprites(spriteThirdStep);

	}

	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			GameEventManager.TriggerEndGame();
		}
	}

	public void collectPart(float _score)
	{
		StartCoroutine(CollectGatePart(_score));
	}
	IEnumerator CollectGatePart(float _score)
	{
		yield return new WaitForSeconds(1.5f);
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

	public void triggTransition(int _nbTransition)
	{
		print ("trigg" + _nbTransition);
		switch (_nbTransition)
		{
		case 1:
		{
			fadeInSprites(spriteFirstStep);
			break;
		}
		case 2:
		{
			fadeInSprites(spriteSecondStep);
			break;
		}
		case 3:
		{
			fadeInSprites(spriteThirdStep);
			break;
		}
		}
	}

	private void fadeInSprites(OTSprite[] _arrSprite)
	{
		foreach (OTSprite _spr in _arrSprite)
		{
			new OTTween(_spr, 1f).Tween("alpha", 1f);
		}
	}
	private void fadeOutSprites(OTSprite[] _arrSprite)
	{
		foreach (OTSprite _spr in _arrSprite)
		{
			new OTTween(_spr, 1f).Tween("alpha", 0f);
		}
	}


}
