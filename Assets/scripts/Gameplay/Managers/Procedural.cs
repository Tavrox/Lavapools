using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Procedural : MonoBehaviour {

	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public ProceduralSteps _CURRENTSTEP;
	[HideInInspector] public bool debug = true;

	[HideInInspector] public List<ProceduralSteps> _listSteps;
	private LevelSetup SETUP;

	// Use this for initialization
	public void Setup () 
	{
		string path = "Procedural/" + _levMan.NAME + "/";
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_listSteps = new List<ProceduralSteps>();
		GameEventManager.GameStart += GameStart;
		GameEventManager.Respawn += Respawn;
		GameEventManager.GameOver += GameOver;

		SETUP = Resources.Load(path + "Setup") as LevelSetup;
		for (int i = 1; i <= SETUP.numberOfSteps ; i++)
		{
			_listSteps.Add( Resources.Load( path + i) as ProceduralSteps);
		}
		_CURRENTSTEP = _listSteps[0];
	}

	private void checkScore()
	{
//		Debug.LogWarning(""+_CURRENTSTEP+"");
		foreach (ProceduralSteps _step in _listSteps)
		{
			if ( _step.stepID > _CURRENTSTEP.stepID)
			{
				if (_step.condition == ProceduralSteps.conditionEnum.Score && _step.ScoreCondition == _levMan.score)
				{
					triggerStep(_step);
				}
				if (_step.condition == ProceduralSteps.conditionEnum.Timer && _step.TimerCondition == _levMan.SecondsElapsed)
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

	public void triggerStep(ProceduralSteps _step)
	{
		_CURRENTSTEP = _step;
//		Debug.Log ("Triggered STEP" + _CURRENTSTEP.stepID);
		foreach (ProceduralSteps.BrickStack _lb in _step.BricksDisabled)
		{
			if (_step.BricksDisabled != null)
			{
				string brickToFetch = _lb.ToString().Replace("_", "/");
				LevelBrick brk = _levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == brickToFetch);
				if (brk != null)
				{
					brk.disableBrick();
				}
			}	
		}
		foreach (ProceduralSteps.BrickStack _lb in _step.BricksEnabled)
		{
			if (_step.BricksEnabled != null)
			{
				string brickToFetch = _lb.ToString().Replace("_", "/");
				LevelBrick brk = _levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == brickToFetch);
				if (brk != null)
				{
					brk.enableBrick();
				}
			}
		}
		foreach (LevelBrick _brick in _levMan.bricksMan.BricksList)
		{
			if (_brick.speed > 0)
			{
				_brick.speed = _brick.speed * _step.Enemies_SpeedMultiplier;
			}
			_brick.initSpeed = _brick.initSpeed * _step.Enemies_SpeedMultiplier;
		}
		foreach (ProceduralSteps.PathStack _wpm in _step.WaypointsToInvert)
		{
			if (_step.WaypointsToInvert != null && _wpm != null)
			{
				string pathToFetch = _wpm.ToString().Replace("_", "/");
				WaypointManager man = _levMan.waypointsMan.Find((WaypointManager obj) => obj.name == pathToFetch);
				man.invertWaypoints();
//				man.relatedBrick.GetComponent<PatrolBrick>().setupTarget();
			}	
		}
		_levMan._player.lowSpeed = _levMan._player.lowSpeed * _step.Crab_SpeedMultiplier;
		_levMan._player.medSpeed = _levMan._player.medSpeed * _step.Crab_SpeedMultiplier;
		_levMan._player.highSpeed = _levMan._player.highSpeed * _step.Crab_SpeedMultiplier;
		_levMan.menuManager.changeLevelLabel(_CURRENTSTEP); 

		if (_step.stepID > 1)
		{
			MasterAudio.PlaySound("Steps", 1f, 1f, 0f, "step_" + _step.levelLabel.ToString().ToLower());
		}
		if (_step.MusicSource != null)
		{
			MasterAudio.TriggerPlaylistClip(_step.MusicSource.name);
		}

		if (_step.LevelToUnlock != null)
		{
			_levMan.tools.UnlockLevel(_step.LevelToUnlock);
		}
	}

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
			triggerStep(_listSteps[0]);
			InvokeRepeating("checkScore", 0f, 0.001f);
		}
	}
}
