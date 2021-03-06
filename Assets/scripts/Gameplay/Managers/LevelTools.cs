﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelTools : MonoBehaviour {

	public LevelManager _levMan;
	public enum KillerList
	{
		Lava,
		Chainsaw,
		Bird,
		GameBorders,
		LevelManager,
		Arrow,
		BladePart,
		VerticalSlider
	};
	public enum DirectionList
	{
		Up, 	//Up
		Down,	//Down
		Left,	//Left
		Right	//Right
	};

	public void disableAllBrick ()
	{
		foreach (LevelBrick brk in _levMan.bricksMan.BricksList)
		{
			brk.disableBrick();
		}
	}

	public void lootStack(int _stk)
	{
		_levMan.collecSum += _stk * 1f;
		checkLevelCompletion();
	}

	public void tryDeath(LevelTools.KillerList _kl)
	{
		GameEventManager.TriggerGameOver(_kl);
	}

	public void CollectObject(Collectible _thing)
	{
		switch (_thing.typeCollectible)
		{
		case Collectible.ListCollectible.TinyGem :
		{
			_levMan.CollectibleGathered.Add(_thing);
			_levMan.collecSum += _thing.value;
			_levMan._player.triggerNotification(_thing.value);
			break;
		}
		case Collectible.ListCollectible.Gatepart :
		{
			_levMan.CollectibleGathered.Add(_thing);
			_levMan.collecSum += _thing.value;
			_levMan._player.triggerNotification(_thing.value);
			_levMan.Gate.collectPart(_levMan.score);
			checkLevelCompletion();
			break;
		}

		}
	}

	public Lootstack createStack()
	{
		GameObject initStack = Instantiate(Resources.Load("Bricks/Environment/Lootstack")) as GameObject;
		initStack.transform.position = new Vector3(1000f,1000f,1000f);
		Lootstack lts = initStack.GetComponent<Lootstack>();
		return lts;
	}

	public Lootstack modifyStack(ref Lootstack stk)
	{
		checkLevelCompletion();
		stk.stackValue = Mathf.FloorToInt((_levMan.score * LevelManager.GlobTuning.percentageLootStack));
		if (stk.stackValue == 0)
		{
			stk.Fade();
		}
		stk.transform.position = _levMan._player.transform.position;
//		List<LinearStep> stpList = _levMan.proc._listSteps.Find( (LinearStep obj) => obj.ScoreCondition < stk.stackValue);
		return stk;
	}

	public CollectiblePlaces calculateFarSpawnPlace(ref List<CollectiblePlaces> _AllPlace, Player _player)
	{
		Vector2 playerVec = new Vector2(_player.transform.position.x, _player.transform.position.y);
		List<CollectiblePlaces> PlacesToSpawn = new List<CollectiblePlaces>();
		CollectiblePlaces _chosen;

		PlacesToSpawn = _AllPlace;
		int removedItem = PlacesToSpawn.RemoveAll( obj => obj.occupied == true);
//		print (removedItem);
		if (removedItem < LevelManager.LocalTuning.Gem_MinimumInLevel)
		{
			foreach (CollectiblePlaces _place in PlacesToSpawn)
			{
				Vector2 placeVec = new Vector2(_place.transform.position.x, _place.transform.position.y);
				_place.distToPlayer = Vector2.Distance(placeVec, playerVec);
			}
			PlacesToSpawn.Sort(delegate (CollectiblePlaces x, CollectiblePlaces y)
			{
				if (x.distToPlayer < y.distToPlayer) return -1;
				if (x.distToPlayer > y.distToPlayer) return 1;
				else return 0;
			});
			PlacesToSpawn.RemoveAll((CollectiblePlaces collec) => collec.distToPlayer < _levMan._player.gpUntriggerArea);
			if (PlacesToSpawn.Count <= 0)
			{
				Debug.LogError("Places To spawn setupped too high");
			}
			int rando = Random.Range(0, PlacesToSpawn.Count);
			_chosen = PlacesToSpawn[rando];
		}
		else
		{
			_chosen = null;
		}
		return (_chosen);

	}

	public void checkLevelCompletion()
	{
		if (_levMan.score >= LevelManager.GlobTuning.finishFirstStep && _levMan.Gate.firstStepTrigg != true)
		{
			_levMan.Gate.triggTransition(1);
		}
		if (_levMan.score >= LevelManager.GlobTuning.finishSecondStep  && _levMan.Gate.secondStepTrigg != true)
		{
			_levMan.Gate.triggTransition(2);
		}
		if (_levMan.score >= LevelManager.GlobTuning.finishThirdStep  && _levMan.Gate.thirdStepTrigg != true)
		{
			_levMan.Gate.triggTransition(3);
		}
	}

	public void UnlockLevel(LevelInfo _lvl)
	{

	}
}
