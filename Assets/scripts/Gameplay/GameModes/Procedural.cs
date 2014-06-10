using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Procedural : MonoBehaviour {

	[HideInInspector] public LevelManager levMan;
	[HideInInspector] public ProceduralBrickParam currParam;

	public LinearStep _CURRENTSTEP;
	
	public LevelParameters LevelSetup;
	public LevelParameters InitLevelSetup;
	public ProceduralLevelSetup ProcSetup;
	public List<LinearStep> _listSteps;
	
	public List<ProceduralBrickParam> paramlist = new List<ProceduralBrickParam>();
	public List<ProceduralBrickParam> paramToTrigger = new List<ProceduralBrickParam>();
	public List<ProceduralBrickParam> forcedParams = new List<ProceduralBrickParam>();


	
	private LevelBrick currModBrick;
	
	public void Setup (LevelManager _lev) 
	{
		levMan = _lev;
		string path = "Maps/" + levMan.NAME + "/Steps/";
		string mainPath = "Maps/" + levMan.NAME + "/";
		
		InitLevelSetup = Instantiate(Resources.Load(mainPath + "Setup")) as LevelParameters;
		LevelSetup = InitLevelSetup;
		ProcSetup = Instantiate(Resources.Load(mainPath + "Procedural")) as ProceduralLevelSetup;
		_listSteps = ProcSetup.LinearSteps;
		untriggerSteps();

		checkBrickSetup();
		
		_CURRENTSTEP = _listSteps[0];
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.Respawn += Respawn;
		GameEventManager.GameOver += GameOver;
	}
	
	private void checkScore()
	{
		LinearStep _targSteps = _listSteps.Find((LinearStep obj) => obj.Triggered == false && obj.ScoreCondition <= levMan.score);
		if (_targSteps != null)
		{
			triggerStep(_targSteps);
		}
	}
	
	public void triggerStep(LinearStep _step)
	{
		_CURRENTSTEP = _step;
		triggerList();
		
		levMan._player.lowSpeed = levMan._player.lowSpeed * _CURRENTSTEP.Crab_SpeedMultiplier;
		levMan._player.medSpeed = levMan._player.medSpeed * _CURRENTSTEP.Crab_SpeedMultiplier;
		levMan._player.highSpeed = levMan._player.highSpeed * _CURRENTSTEP.Crab_SpeedMultiplier;
		if (levMan.menuManager != null)
		{
			levMan.menuManager.changeLevelLabel(_CURRENTSTEP); 
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
			levMan.tools.UnlockLevel(_CURRENTSTEP.LevelToUnlock);
		}
	}
	
	private void triggerList()
	{
		if (ProcSetup.ListProcParam != null)
		{
			paramlist = ProcSetup.ListProcParam.FindAll((ProceduralBrickParam para) => para.stepID == _CURRENTSTEP.stepID && para.isTriggered == false);

			// Take Forced Params and add them to the trigger list by default
			forcedParams = paramlist.FindAll((ProceduralBrickParam obj) => obj.forceTrigger == true);
			foreach (ProceduralBrickParam prm in forcedParams)
			{
				paramlist.Remove(prm);
				paramToTrigger.Add(prm);
			}

			generateTriggerList(paramlist);
			activateBrick(paramToTrigger);
			_CURRENTSTEP.Triggered = true;
		}
	}

	private void activateBrick(List<ProceduralBrickParam> _list)
	{
		foreach (ProceduralBrickParam _parameter in _list)
		{
			print (_parameter.name);
			currParam = _parameter;
			currModBrick = findBrick();
			enableBrick();
			disableBrick();
			toggleBrick();
			attributeWaypoint();
			giveDirections();
			setupTowerLength();
			swapTowerRotation();
			_parameter.isTriggered = true;
		}
	}

	private void generateTriggerList(List<ProceduralBrickParam> _list)
	{
		if (_list.Count == 0)
		{
			return;
		}

		switch (_CURRENTSTEP.procType)
		{
		case LinearStep.procTrigger.BrickByBrick :
		{
			foreach (ProceduralBrickParam prm in _list)
			{
				int randNb = Random.Range(0,100);
				if (randNb <= prm.chanceToTrigger)
				{
					paramToTrigger.Add(prm);
				}
			}
			break;
		}
		case LinearStep.procTrigger.Mixed :
		{
			paramToTrigger.Add(_list[Random.Range(0, _list.Count)]);
			break;
		}
		}
	}
	
	private LevelBrick findBrick()
	{
		LevelBrick res = null;
		currModBrick = currParam.Brick;
		if (currModBrick == null)
		{
			Debug.LogError("The brick is NULL");
			Debug.Break();
		}
		res = currModBrick;
		return res;
	}

	private void enableBrick()
	{
		if (currParam.tryEnable == true && currParam.Brick != null)
		{
			if (currModBrick.type == LevelBrick.typeList.BladeTower)
			{
				currModBrick.GetComponent<BladeTower>().setupBladePart(currParam.addLength, false);
			}
			currModBrick.enableBrick();
		}
	}
	private void disableBrick()
	{
		if (currParam.tryDisable == true && currParam.Brick != null)
		{
			currModBrick.disableBrick();
		}
	}

	private void toggleBrick()
	{

	}
	
	private void attributeWaypoint()
	{
		if (currModBrick.GetComponent<PatrolBrick>() != null && currParam.giveWPM != null)
		{
			currModBrick.GetComponent<PatrolBrick>().brickPath = currParam.giveWPM;
		}
	}

	
	private void giveDirections()
	{
		if (currModBrick.type == LevelBrick.typeList.BladeTower || currModBrick.type == LevelBrick.typeList.ArrowTower)
		{
			currParam.changeDirections.ToUpper();
			if (currParam.changeDirections.Contains("U") || currParam.changeDirections.Contains("D") ||
			    currParam.changeDirections.Contains("L") || currParam.changeDirections.Contains("R") )
			{
				currModBrick.GetComponent<OppTower>().setupDirectionList(currParam.changeDirections);
				if (currModBrick.type == LevelBrick.typeList.ArrowTower)
				{
					currModBrick.GetComponent<ArrowTower>().displayDirections(currParam.changeDirections);
				}
			}
		}
	}


	private void setupTowerLength()
	{
		if (currModBrick.type == LevelBrick.typeList.BladeTower && currParam.addLength > 0)
		{
			currModBrick.GetComponent<BladeTower>().setupBladePart(currParam.addLength, false);
		}
	}
	private void swapTowerRotation()
	{
		if (currParam.tryInvert == true)
		{
			if (currModBrick.type == LevelBrick.typeList.BladeTower)
			{
				currModBrick.GetComponent<BladeTower>().swapRotation(currParam.tryInvert);
			}
			if (currModBrick.type == LevelBrick.typeList.Chainsaw || currModBrick.type == LevelBrick.typeList.Bird )
			{
				WaypointManager manplz = currParam.giveWPM;
				manplz.invertWaypoints();
				
			}
		}
	}

	
	private void untriggerSteps()
	{
		foreach (LinearStep stp in _listSteps)
		{
			stp.Triggered = false;
			foreach (ProceduralBrickParam brpm in stp.LinkedParam)
			{
				brpm.isTriggered = false;
			}
		}
	}

	private void checkBrickSetup()
	{
		foreach (ProceduralBrickParam prm in ProcSetup.ListProcParam)
		{
			if (prm.Brick == null)
			{
				Debug.Break();
			}
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
			untriggerSteps();
			_listSteps = ProcSetup.LinearSteps;
			LevelSetup = InitLevelSetup;
			levMan.tools.disableAllBrick();
			triggerStep(_listSteps[0]);
			InvokeRepeating("checkScore", 0f, 1f);
		}
	}
}
