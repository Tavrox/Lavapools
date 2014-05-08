using UnityEngine;
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
		Fireball
	};
	public enum DirectionList
	{
		Up, 	//Up
		Down,	//Down
		Left,	//Left
		Right	//Right
	};
	
	public void disableBrick (LevelBrick _brickToEnable)
	{
		
	}
	
	public void disableAllBrick ()
	{
		foreach (LevelBrick brk in _levMan.bricksMan.BricksList)
		{
			brk.disableBrick();
		}
	}

	public void enableBrick (LevelBrick _brickToEnable)
	{
		
	}

	public void enableBrick (string _brickToEnable)
	{
		
	}

	public void fetchWaypoints(LevelBrick.typeList _type)
	{

	}

	public WaypointManager findWpManager(LevelBrick.typeList _type)
	{
		WaypointManager res = null;
		res = _levMan.wpDirector.waypointsMan.Find( delegate(WaypointManager obj) 
		{
			return obj.relatedBrick.type == _type;
		});
		return res;
	}
	public WaypointManager pickRandomWP(LevelBrick.typeList _type)
	{
		List<WaypointManager> _wpm  = _levMan.wpDirector.waypointsMan.FindAll((WaypointManager obj) => obj.type == _type);
		return _wpm[Random.Range(0,_wpm.Count)];
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
		if (_levMan.score == LevelManager.GlobTuning.finishFirstStep)
		{
			_levMan.Gate.triggTransition(1);
		}
		if (_levMan.score == LevelManager.GlobTuning.finishSecondStep)
		{
			_levMan.Gate.triggTransition(2);
		}
		if (_levMan.score == LevelManager.GlobTuning.finishThirdStep)
		{
			_levMan.Gate.triggTransition(3);
		}
	}

	public void UnlockLevel(LevelInfo _lvl)
	{

	}
}
