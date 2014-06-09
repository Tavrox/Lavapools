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
		_step.Triggered = true;
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
			List<ProceduralBrickParam> paramlist = ProcSetup.ListProcParam.FindAll((ProceduralBrickParam para) => para.stepID == _CURRENTSTEP.stepID);
			List<ProceduralBrickParam> paramToTrigger = new List<ProceduralBrickParam>();
			List<ProceduralBrickParam> forcedParams = paramlist.FindAll((ProceduralBrickParam obj) => obj.forceTrigger == true);
			foreach (ProceduralBrickParam prm in forcedParams)
			{
				paramlist.Remove(prm);
				paramToTrigger.Add(prm);
			}

			switch (_CURRENTSTEP.procType)
			{
			case LinearStep.procTrigger.BrickByBrick :
			{
				foreach (ProceduralBrickParam prm in paramlist)
				{
					int randNb = Random.Range(0,100);
					if (randNb <= prm.chanceToTrigger)
					{
						paramToTrigger.Add(prm);
						print ("Added" + prm.name);
					}
				}
				break;
			}
			case LinearStep.procTrigger.Mixed :
			{
				paramToTrigger.Add(paramlist[Random.Range(0, paramlist.Count)]);
				break;
			}
			}

			foreach (ProceduralBrickParam _parameter in paramToTrigger)
			{
				print (_parameter.name);
				currParam = _parameter;
				currModBrick = findBrick();
				enableBrick();
//				attributeWaypoint();
//				giveDirections();
//				enableBrick();
//				disableBrick();
//				setupTowerLength();
//				swapTowerRotation();
			}
		}
	}
	
	private LevelBrick findBrick()
	{
		LevelBrick res = null;
		string typeToFetch = currParam.Brick.ToString();
		string idToFetch = "_" + currParam.ID.ToString();
		currModBrick = levMan.bricksMan.BricksList.Find ((LevelBrick obj) => obj.name == typeToFetch + idToFetch);
		if (currModBrick == null)
		{
			Debug.LogError("The brick " + typeToFetch + idToFetch + " hasn't been found");
			Debug.Break();
		}
		res = currModBrick;
		return res;
	}

	private void enableBrick()
	{
		if (currParam.tryEnable == true && currParam.ID != 0)
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
		if (currParam.tryDisable == true && currParam.ID != 0)
		{
			currModBrick.disableBrick();
		}
	}

	private void toggleBrick()
	{

	}
	
	private void attributeWaypoint()
	{
		if (currModBrick.GetComponent<PatrolBrick>() != null && currParam.giveWPM != "")
		{
			string typeToFetch = currParam.Brick.ToString();
			string idToFetch = "_" + currParam.giveWPM.ToUpper();
			currModBrick.GetComponent<PatrolBrick>().brickPath = levMan.wpDirector.waypointsMan.Find((WaypointManager mana) => mana.name == typeToFetch + idToFetch);
			currModBrick.GetComponent<PatrolBrick>().brickPath.type = currModBrick.type;
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
				string typeToFetch = currParam.Brick.ToString();
				string idToFetch = "_" + currParam.giveWPM.ToUpper();
				WaypointManager manplz = levMan.wpDirector.waypointsMan.Find((WaypointManager mana) => mana.name == typeToFetch + idToFetch);
				manplz.invertWaypoints();
				
			}
		}
	}

	
	private void untriggerSteps()
	{
		foreach (LinearStep stp in _listSteps)
		{
			stp.Triggered = false;
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
