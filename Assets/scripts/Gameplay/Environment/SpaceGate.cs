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

	public OTAnimatingSprite Vortex;

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

		Vortex = FETool.findWithinChildren(gameObject, "Vortex").GetComponentInChildren<OTAnimatingSprite>();
	}

	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			GameEventManager.TriggerEndGame();
		}
	}

	private void disableGround(GameObject _step)
	{
		_step.GetComponent<ImmovableGround>().enabled = false;
	}

	private void enableGround(GameObject _step)
	{
		_step.GetComponent<ImmovableGround>().enabled = true;
		print (_step.GetComponent<ImmovableGround>().enabled);
	}

	public void collectPart(float _score)
	{
		StartCoroutine(CollectGatePart(_score));
	}
	IEnumerator CollectGatePart(float _score)
	{
		yield return new WaitForSeconds(1.5f);
		_score +=1;
		if (_score < 26)
		{
			MasterAudio.PlaySound("door_piece");
			if (_score < 10)
			{
				_spr.frameName = "gate" + "0" +  _score +"load";
				
			}
			else
			{
				_spr.frameName = "gate" + _score +"load";
			}
		}
	}


	private void Respawn()
	{
		_spr.frameName = "gate00load";
		Vortex.PlayLoop("idle");
		Vortex.speed = 0.8f;
	}

	public void triggTransition(int _nbTransition)
	{
		switch (_nbTransition)
		{
		case 1:
		{
			enableGround(firstStep);
			fadeInSprites(spriteFirstStep);
			break;
		}
		case 2:
		{
			enableGround(secondStep);
			fadeInSprites(spriteSecondStep);
			break;
		}
		case 3:
		{
			enableGround(thirdStep);
			fadeInSprites(spriteThirdStep);
			Vortex.PlayLoop("active");
			Vortex.speed = 0.3f;
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
