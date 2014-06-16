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
		giveBrickInitPath();
		
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
			_parameter.Rename();
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
		_list.Clear();
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
				int randNb = Random.Range(1,100);
				if (randNb <= prm.chanceToTrigger)
				{
					paramToTrigger.Add(prm);
				}
			}
			break;
		}
		case LinearStep.procTrigger.Mixed :
		{
			insideLoop(_list);
			break;
		}
		}
	}

	private void insideLoop(List<ProceduralBrickParam> _list)
	{
		ProceduralBrickParam randParam = findRandomParam(_list);
		if (randParam == null)
		{
			return;
		}
		_list.Remove(randParam);
		if (currModBrick.isEnabled == true && currParam.tryEnable == true && _CURRENTSTEP.allowRetrigger == true)
		{
			insideLoop(_list);
		}
		if (currModBrick.isEnabled == false && currParam.tryDisable == true && _CURRENTSTEP.allowRetrigger == true)
		{
			insideLoop(_list);
		}
		else
		{
			paramToTrigger.Add(randParam);
		}
	}

	private ProceduralBrickParam findRandomParam(List<ProceduralBrickParam> _list)
	{
		if (_list.Count == 0)
		{
			return null;
		}
		int rand = Random.Range(0, _list.Count);
		currParam = _list[rand];
		currModBrick = findBrick();
		return currParam;
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
		if (currParam.Toggle == true && currParam.tryEnable == false && currParam.tryDisable == false)
		{
			if (currModBrick.isEnabled == true)
			{
				currModBrick.disableBrick();
			}
			else
			{
				currModBrick.enableBrick();
			}
		}
	}
	
	private void attributeWaypoint()
	{
		if (currModBrick.GetComponent<PatrolBrick>() != null && currParam.giveWPM != "")
		{
			string typeToFetch = currParam.Brick.ToString();
			string idToFetch = "_" + currParam.giveWPM.ToUpper();
			currModBrick.GetComponent<PatrolBrick>().brickPath = levMan.wpDirector.waypointsMan.Find((WaypointManager mana) => mana.name == typeToFetch + idToFetch);
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

	private void giveBrickInitPath()
	{
		levMan.bricksMan.BricksList.ForEach(delegate(LevelBrick brk) 
		{
			if (brk.GetComponent<PatrolBrick>() != null)
			{
				PatrolBrick ptb = brk.GetComponent<PatrolBrick>();
				// Take all param with type and id
				ProceduralBrickParam prbm = ProcSetup.ListProcParam.Find((ProceduralBrickParam _pb) => _pb.Brick == ptb.type && _pb.ID == ptb.brickId);
				// Take One with match previous ID > WPM

				if (prbm != null)
				{
					WaypointManager mana = levMan.wpDirector.waypointsMan.Find((WaypointManager obj) => obj.id == prbm.giveWPM);
					if (mana == null)
					{
						Debug.Log("The brick " + brk + " has no parameter with a path attributed. Care for bugs" );
					}
					ptb.brickPath = mana;
				}
				else
				{
					Debug.Log("Cant find an equivalent to " + ptb.type.ToString() +""+ ptb.brickId + ", bugs might occur");
				}
				ptb.setupPath();
				if (ptb.initWp != null && ptb.initWp.linkedManager != ptb.brickPath)
				{
					Debug.LogError(ptb.name + " Init doesn't fit attributed through proc");
					Debug.Break();
				}
			}
		});
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
			paramToTrigger.Clear();
			untriggerSteps();
			_listSteps = ProcSetup.LinearSteps;
			LevelSetup = InitLevelSetup;
			levMan.tools.disableAllBrick();
			triggerStep(_listSteps[0]);
			InvokeRepeating("checkScore", 0f, 1f);
		}
	}
}
