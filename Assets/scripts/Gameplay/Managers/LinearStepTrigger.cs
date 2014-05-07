using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinearStepTrigger : MonoBehaviour {

	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public LinearStep _CURRENTSTEP;
	[HideInInspector] public bool debug = true;

	[HideInInspector] public List<LinearStep> _listSteps;
	private LevelSetup SETUP;

	// Use this for initialization
	public void Setup () 
	{
		string path = "Procedural/" + _levMan.NAME + "/";
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_listSteps = new List<LinearStep>();
		GameEventManager.GameStart += GameStart;
		GameEventManager.Respawn += Respawn;
		GameEventManager.GameOver += GameOver;

		SETUP = Resources.Load(path + "Setup") as LevelSetup;
		for (int i = 1; i <= SETUP.numberOfSteps ; i++)
		{
			_listSteps.Add( Resources.Load( path + i) as LinearStep);
		}
		_CURRENTSTEP = _listSteps[0];
	}

	private void checkScore()
	{
//		Debug.LogWarning(""+_CURRENTSTEP+"");
		foreach (LinearStep _step in _listSteps)
		{
			if ( _step.stepID > _CURRENTSTEP.stepID)
			{
				if (_step.condition == LinearStep.conditionEnum.Score && _step.ScoreCondition == _levMan.score)
				{
					triggerStep(_step);
				}
				if (_step.condition == LinearStep.conditionEnum.Timer && _step.TimerCondition == _levMan.SecondsElapsed)
				{
					triggerStep(_step);
				}
				else
				{
//					break;
				}
			}

		}
	}

	public void triggerStep(LinearStep _step)
	{
		_CURRENTSTEP = _step;
//		Debug.Log ("Triggered STEP" + _CURRENTSTEP.stepID);

//		disableBrickList();
//		enableBrickList();
//		setupSpeedBrickList();
//		invertWayList();
//		setupTowerList();
//		setupFireTower();

		_levMan._player.lowSpeed = _levMan._player.lowSpeed * _CURRENTSTEP.Crab_SpeedMultiplier;
		_levMan._player.medSpeed = _levMan._player.medSpeed * _CURRENTSTEP.Crab_SpeedMultiplier;
		_levMan._player.highSpeed = _levMan._player.highSpeed * _CURRENTSTEP.Crab_SpeedMultiplier;
		if (_levMan.menuManager != null)
		{
			_levMan.menuManager.changeLevelLabel(_CURRENTSTEP); 
		}
		if (_CURRENTSTEP.stepID > 1)
		{
			MasterAudio.PlaySound("Steps", 1f, 1f, 0f, "step_" + _CURRENTSTEP.levelLabel.ToString().ToLower());
		}
		if (_CURRENTSTEP.MusicSource != null)
		{
			MasterAudio.TriggerPlaylistClip(_CURRENTSTEP.MusicSource.name);
		}

		if (_CURRENTSTEP.LevelToUnlock != null)
		{
			_levMan.tools.UnlockLevel(_CURRENTSTEP.LevelToUnlock);
		}
	}

	/*

	private void disableBrickList()
	{
		foreach (LinearStep.BrickStack _lb in _CURRENTSTEP.BricksDisabled)
		{
			if (_CURRENTSTEP.BricksDisabled != null)
			{
				string brickToFetch = _lb.ToString().Replace("_", "/");
				LevelBrick brk = _levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == brickToFetch);
				if (brk != null)
				{
					brk.disableBrick();
				}
			}	
		}
	}
	private void enableBrickList()
	{
		foreach (LinearStep.BrickStack _lb in _CURRENTSTEP.BricksEnabled)
		{
			if (_CURRENTSTEP.BricksEnabled != null)
			{
				string brickToFetch = _lb.ToString().Replace("_", "/");
				LevelBrick brk = _levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == brickToFetch);
				if (brk != null)
				{
					brk.enableBrick();
				}
			}
		}
	}
	private void setupSpeedBrickList()
	{
		foreach (LevelBrick _brick in _levMan.bricksMan.BricksList)
		{
			if (_brick.speed > 0)
			{
				_brick.speed = _brick.speed * _CURRENTSTEP.Enemies_SpeedMultiplier;
			}
			_brick.initSpeed = _brick.initSpeed * _CURRENTSTEP.Enemies_SpeedMultiplier;
		}
	}
	private void invertWayList()
	{
		foreach (LinearStep.PathStack _wpm in _CURRENTSTEP.WaypointsToInvert)
		{
			if (_CURRENTSTEP.WaypointsToInvert != null && _wpm != null)
			{
				string pathToFetch = _wpm.ToString().Replace("_", "/");
				WaypointManager man = _levMan.waypointsMan.Find((WaypointManager obj) => obj.name == pathToFetch);
				man.invertWaypoints();
			}	
		}
	}
	private void setupTowerList()
	{
		foreach (LinearStep.BrickStack _lb in _CURRENTSTEP.ArrowTowerSetup)
		{
			if (_CURRENTSTEP.ArrowTowerSetup != null)
			{
				string brickToFetch = _lb.ToString().Replace("_", "/");
				LevelBrick brk = _levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == brickToFetch);
				if (brk != null)
				{
//					brk.enableBrick();
					int currentIndex = _CURRENTSTEP.ArrowTowerSetup.IndexOf(_lb);
					if (brk.GetComponent<ArrowTower>().enabledDirection != null)
					{
						brk.GetComponent<ArrowTower>().enabledDirection.Clear();
					}
					brk.GetComponent<ArrowTower>().setupDirectionList(_CURRENTSTEP.ArrowTowerDirections[currentIndex]);
				}
			}
		}
	}

	private void setupFireTower()
	{
		foreach (LinearStep.BrickStack _lb in _CURRENTSTEP.FireTowerSetup)
		{
			if (_CURRENTSTEP.FireTowerSetup != null)
			{
				string brickToFetch = _lb.ToString().Replace("_", "/");
				LevelBrick brk = _levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == brickToFetch);
				if (brk != null)
				{
					// brk.enableBrick();
					int currentIndex = _CURRENTSTEP.FireTowerSetup.IndexOf(_lb);
					brk.GetComponent<FireTower>().enabledDirection.Clear();
					if (_CURRENTSTEP.FireTowerDirection[currentIndex] != "" &&
					    _CURRENTSTEP.FireTowerDirection[currentIndex] != null &&
					    _CURRENTSTEP.FireTowerLength[currentIndex] != null &&
					    _CURRENTSTEP.FireTowerSwapRot[currentIndex] != null)
					{
						brk.GetComponent<FireTower>().stepChanger(_CURRENTSTEP.FireTowerLength[currentIndex], _CURRENTSTEP.FireTowerDirection[currentIndex], _CURRENTSTEP.FireTowerSwapRot[currentIndex]);
					}
				}
			}
		}

	}
*/

	private void GameStart()
	{
		if (this != null)
		{

		}
	}

	private void GameOver()
	{
		if (this != null)
		{
			MasterAudio.StopAllPlaylists();
			CancelInvoke("checkScore");
		}
	}

	private void Respawn()
	{
		if (this != null)
		{
			_levMan.tools.disableAllBrick();
			triggerStep(_listSteps[0]);
			InvokeRepeating("checkScore", 0f, 0.001f);
		}
	}
}