using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Procedural : MonoBehaviour {

	[HideInInspector] public LevelManager _levMan;
	[HideInInspector] public ProceduralSteps _CURRENTSTEP;
	public bool debug = true;

	public List<ProceduralSteps> _listSteps;
	private LevelSetup SETUP;

	// Use this for initialization
	public void Setup () 
	{
		string path = "Procedural/Level" + Application.loadedLevelName+"/";
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_listSteps = new List<ProceduralSteps>();
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
			if ( _step.priority > _CURRENTSTEP.priority)
			{
				if (_step.condition == ProceduralSteps.conditionEnum.Score && _step.ScoreCondition < _levMan.score)
				{
					triggerStep(_step);
				}
				if (_step.condition == ProceduralSteps.conditionEnum.Timer && _step.TimerCondition < _levMan.SecondsElapsed)
				{
					triggerStep(_step);
				}
				else
				{
					break;
				}
//				foreach (WaypointManager wpm in _step.WaypointsToInvert)
//				{
//					wpm.invertWaypoints(); // FUNC TO DO
//				}
			}

		}
	}

	public void triggerStep(ProceduralSteps _step)
	{
		_CURRENTSTEP = _step;
//		Debug.Log ("Triggered STEP" + _CURRENTSTEP.stepID);
		foreach (string _lb in _step.BricksDisabled)
		{
			if (_step.BricksDisabled != null)
			{
				_levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == _lb).disableBrick();
			}	
		}
		foreach (string _lb in _step.BricksEnabled)
		{
			if (_step.BricksEnabled != null)
			{
				_levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == _lb).enableBrick();
			}	
		}
		foreach (LevelBrick _brick in _levMan.bricksMan.BricksList)
		{
			if (_brick.speed > 0)
			{
				_brick.speed = _brick.speed * _step.SpeedMultiplier;
			}
			_brick.initSpeed = _brick.initSpeed * _step.SpeedMultiplier;
		}
		_levMan._player.speed = _levMan._player.speed * _step.SpeedMultiplier;
		_levMan.menuManager.changeLevelLabel(_CURRENTSTEP);
		if (_step.levelLabel != ProceduralSteps.Difficulty.Noobcrab)
		{
			MasterAudio.PlaySound("Steps", 1f, 1f,0f, _step.levelLabel.ToString());
		}
		if (_step.levelLabel == ProceduralSteps.Difficulty.Noobcrab && _step.PlayMusic == true)
		{
			MasterAudio.TriggerPlaylistClip("Step_1");
		}
		if (_step.levelLabel == ProceduralSteps.Difficulty.Crabbish && _step.PlayMusic == true)
		{
			MasterAudio.TriggerPlaylistClip("Step_2");
		}
		if (_step.levelLabel == ProceduralSteps.Difficulty.Crabmaster && _step.PlayMusic == true)
		{
			MasterAudio.TriggerPlaylistClip("Step_3");
		}
	}

	private List<ProceduralSteps> loadSteps(int _levelID)
	{
		List<ProceduralSteps> _list = new List<ProceduralSteps>();
		switch (_levelID)
		{
			case 1 :
			{
			print ("olol");
			break;
			}
			case 2 :
			{
				
			break;
			}
		}
		return _list;
	}

	private void GameOver()
	{
		CancelInvoke("checkScore");
	}

	private void Respawn()
	{
		triggerStep(_listSteps[0]);
		InvokeRepeating("checkScore", 0f, 1f);
//		MasterAudio.TriggerPlaylistClip("Step_1");
	}
}
