﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpaceGate : MonoBehaviour {

	private OTSprite _spr;
	public LevelManager _levMan;

	private GameObject firstStep;
	private GameObject secondStep;
	private GameObject thirdStep;

	public bool firstStepTrigg = false;
	public bool secondStepTrigg = false;
	public bool thirdStepTrigg = false;

	private OTSprite[] spriteFirstStep = new OTSprite[0];
	private OTSprite[] spriteSecondStep = new OTSprite[0];
	private OTSprite[] spriteThirdStep = new OTSprite[0];
	public List<GameObject> slotList = new List<GameObject>();
	public GameObject defaultSlot;

	public OTAnimatingSprite Vortex;

	public void Setup(LevelManager _lev)
	{
		_spr = FETool.findWithinChildren(gameObject, "TheGate").GetComponentInChildren<OTSprite>();
		_levMan = _lev;
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

		for (int i = 0 ; i < 24 ; i++)
		{
			slotList.Add(FETool.findWithinChildren(gameObject, "Slots/" + (i+1).ToString()));
		}
		defaultSlot = FETool.findWithinChildren(gameObject, "Slots/Default");

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
				firstStepTrigg = _isEnabled;
				secondStepTrigg = _isEnabled;
				thirdStepTrigg = _isEnabled;
				firstStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				secondStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				thirdStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteFirstStep, _toAlpha);
				fadeSprites(spriteSecondStep, _toAlpha);
				fadeSprites(spriteThirdStep, _toAlpha);
				break;
			}
			case 1:
			{
				firstStepTrigg = _isEnabled;
				firstStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteFirstStep, _toAlpha);
				break;
			}
			case 2:
			{
				secondStepTrigg = _isEnabled;
				secondStep.GetComponent<BoxCollider>().enabled = _isEnabled;
				fadeSprites(spriteSecondStep, _toAlpha);
				break;
			}
			case 3:
			{
			print ("play this");
				thirdStepTrigg = _isEnabled;
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
			_spr.frameName = "gate00load";
			Vortex.PlayLoop("idle");
		}
	}


}
