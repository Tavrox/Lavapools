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
		GameEventManager.GameOver += GameOver;

		firstStep = FETool.findWithinChildren(gameObject, "ExitLoc/1");
		secondStep = FETool.findWithinChildren(gameObject, "ExitLoc/2");
		thirdStep = FETool.findWithinChildren(gameObject, "ExitLoc/3");

		spriteFirstStep = firstStep.GetComponentsInChildren<OTSprite>();
		spriteSecondStep = secondStep.GetComponentsInChildren<OTSprite>();
		spriteThirdStep = thirdStep.GetComponentsInChildren<OTSprite>();

		fadeSprites(spriteFirstStep, 0f);
		fadeSprites(spriteSecondStep, 0f);
		fadeSprites(spriteThirdStep, 0f);

		Vortex = FETool.findWithinChildren(gameObject, "Vortex").GetComponentInChildren<OTAnimatingSprite>();
	}

	void OnTriggerEnter(Collider _other)
	{
		if (_other.CompareTag("Player"))
		{
			GameEventManager.TriggerEndGame();
		}
	}

	private void triggerGround(int _Nbstep, bool _isEnabled, float _toAlpha)
	{
		switch (_Nbstep)
		{
			case 0: // FOR ALL
			{
				firstStep = FETool.findWithinChildren(gameObject, "ExitLoc/1");
				secondStep = FETool.findWithinChildren(gameObject, "ExitLoc/2");
				thirdStep = FETool.findWithinChildren(gameObject, "ExitLoc/3");
				firstStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				firstStep.GetComponent<ImmovableGround>().enabled = _isEnabled;
				fadeSprites(spriteFirstStep, _toAlpha);
				secondStep.GetComponent<ImmovableGround>().enabled = _isEnabled;
				secondStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteSecondStep, _toAlpha);
				thirdStep.GetComponent<ImmovableGround>().enabled = _isEnabled;
				thirdStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteThirdStep, _toAlpha);
				break;
			}
			case 1:
				{
			firstStep.GetComponent<ImmovableGround>().enabled = _isEnabled;
			firstStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteFirstStep, _toAlpha);
				break;
			}
			case 2:
			{
			secondStep.GetComponent<ImmovableGround>().enabled = _isEnabled;
			secondStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteSecondStep, _toAlpha);
				break;
			}
			case 3:
			{
			thirdStep.GetComponent<ImmovableGround>().enabled = _isEnabled;
			thirdStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteThirdStep, _toAlpha);
				Vortex.PlayLoop("active");
				Vortex.speed = 0.3f;
				break;
			}
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

	public void triggTransition(int _nbTransition)
	{
		switch (_nbTransition)
		{
		case 1:
		{
			triggerGround(1, true, 1f);
			break;
		}
		case 2:
		{
			triggerGround(2, true, 1f);
			break;
		}
		case 3:
		{
			triggerGround(3, true, 1f);
			Vortex.PlayLoop("active");
			Vortex.speed = 0.3f;
			break;
		}
		}
	}

	private void fadeSprites(OTSprite[] _arrSprite, float _toAlpha)
	{
		foreach (OTSprite _spr in _arrSprite)
		{
			new OTTween(_spr, 1f).Tween("alpha", _toAlpha);
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			triggerGround(0, false, 0f);
			_spr.frameName = "gate00load";
			Vortex.PlayLoop("idle");
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			triggerGround(0, false, 0f);
			_spr.frameName = "gate00load";
			Vortex.PlayLoop("idle");
		}
	}


}
