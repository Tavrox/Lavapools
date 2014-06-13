using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(ProceduralLevelSetup))]
public class ProceduralStepsEditor : Editor
{
	private ProceduralLevelSetup setup;
	private float maxSize = 550f;
	private float boxSize;
	private float stepSize;
	[SerializeField] private ProceduralBrickParam brpm;
	private GUIEditorSkin customSkin;

	// Parameters for RNG
	public int rngSteps;
	public int rngBrick;
	[Tooltip("Remove toggle within RNG")] public bool restrainToggle;
	[Tooltip("Remove disable within in RNG!")] public bool restrainDisable;
	[Tooltip("Remove invert within RNG!")] public bool restrainInvert;
	public int minChances;
	public int maxChances;
	public int minAddLength;
	public int maxAddLength;
	public LevelBrick.typeList rngType;
	public List<LevelBrick> ingameBricks;
	
	Vector2 vec = new Vector2(100f,100f);

	private bool displayEachParam = false;
	
	public override void OnInspectorGUI()
	{
		setup = (ProceduralLevelSetup)target;
		boxSize = maxSize / 8 ;
		stepSize = maxSize / 6;
		customSkin = Resources.Load("Tools/Skins/LvlEditor") as GUIEditorSkin;
		GUI.skin = customSkin.skin;
		GUI.skin.toggle = customSkin.skin.toggle;
		base.OnInspectorGUI();

		
		GUILayout.Box("Tools", customSkin.skin.textField  , GUILayout.ExpandWidth(true));

		buttonLoadStep();

		if (setup.LinearSteps.Count > 0)
		{			
			ingameBricks = setup.lvlParam.getBrickGameList();
			Cleaner();
			
			string gameBricks = "Ingame defined bricks \n";
			foreach (LevelBrick _brick in ingameBricks)
			{
				string parse = _brick.type.ToString() + " " + _brick.brickId ;
				if (_brick.GetComponent<PatrolBrick>() && _brick.GetComponent<PatrolBrick>().brickPath.id != null)
				{
					parse += " ";
					parse += _brick.GetComponent<PatrolBrick>().brickPath.id;
				}
				parse += "\n";
				gameBricks += parse;
			}
			EditorGUILayout.HelpBox(gameBricks, MessageType.Info,true);
			randomStepParameters();
			randomStepsButton(rngSteps, rngBrick);
			autoAttributeWPM();
			buttonRemoveAll();
			buttonCheckSame();
			buttonTryStep();
			GUILayout.Label("How to use dat tool" +
							"\nThere are Two types of trigger : Brick by Brick and Mixed." +
			                "\n-Mixed take all non-forced bricks in a step and divides to calc chance. Ex : 2 bricks = 50 / 50 each" +
			                "\n-Brick by Brick take all brick and try to trigger it and use Chances parameter" +
			                "\n\nRandom generation will generate for X steps, X bricks number with random parameters." +
			                "\n-You may use auto attribute button afterwards." +
			                "\n-The auto attribute button gives bricks their ingame path id," +
			                "then you don't have to fill them yourselves", customSkin.skin.label, GUILayout.Width(500f));


			EditorGUILayout.BeginVertical(GUILayout.Width(maxSize)); 
			displayStepInfo(setup.LinearSteps);
			EditorGUILayout.EndVertical();
		}
	
		EditorUtility.SetDirty(setup);
	}

	private void displayStepInfo(List<LinearStep> _stpList)
	{
		foreach (LinearStep _stp in _stpList)
		{
			EditorGUILayout.Separator();
			GUI.color = customSkin.colorList[_stpList.IndexOf(_stp)];
			GUILayout.Box("Step" + _stp.stepID, customSkin.skin.textField  , GUILayout.ExpandWidth(true));
			_stp.triggerSum = 0;
			
			displayStepHeader(_stp);
			modifyStep(_stp);
			displayProcHeader(_stp);

			if (_stp.LinkedParam != null)
			{
				foreach (ProceduralBrickParam pbrpm in _stp.LinkedParam)
				{
					if (pbrpm != null)
					{
						EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));

						pbrpm.forceTrigger 		= EditorGUILayout.Toggle("", pbrpm.forceTrigger, customSkin.skin.toggle ,GUILayout.Width(boxSize));
						if (_stp.procType == LinearStep.procTrigger.BrickByBrick)
						{
							pbrpm.chanceToTrigger	= EditorGUILayout.IntField("", pbrpm.chanceToTrigger, GUILayout.Width(boxSize));
							if (pbrpm.forceTrigger == true)
							{
								pbrpm.chanceToTrigger = 100;
							}
						}
						pbrpm.Brick 			= (LevelBrick.typeList)System.Enum.Parse(typeof(LevelBrick.typeList) , EditorGUILayout.EnumPopup("", pbrpm.Brick, GUILayout.Width(boxSize)).ToString());
						pbrpm.ID				= EditorGUILayout.IntField("", pbrpm.ID, GUILayout.Width(boxSize));
						pbrpm.stepID 			= _stp.stepID;
						pbrpm.giveWPM 			= EditorGUILayout.TextField("", pbrpm.giveWPM, GUILayout.Width(boxSize));
						pbrpm.tryEnable 		= EditorGUILayout.Toggle("", pbrpm.tryEnable, customSkin.skin.toggle , GUILayout.Width(boxSize));
						pbrpm.tryDisable 		= EditorGUILayout.Toggle("", pbrpm.tryDisable, customSkin.skin.toggle, GUILayout.Width(boxSize));
						pbrpm.Toggle 			= EditorGUILayout.Toggle("", pbrpm.Toggle, customSkin.skin.toggle, GUILayout.Width(boxSize));

						if (pbrpm.Brick != null)
						{
							if (pbrpm.Brick == LevelBrick.typeList.ArrowTower || pbrpm.Brick == LevelBrick.typeList.BladeTower)
							{
								pbrpm.changeDirections = EditorGUILayout.TextField("", pbrpm.changeDirections, GUILayout.Width(boxSize));
							}
							else
							{
								GUI.color = Color.clear;
								GUILayout.Box("", GUILayout.Width(boxSize));
								GUI.color = customSkin.colorList[_stpList.IndexOf(_stp)];
							}

							
							if ( pbrpm.Brick == LevelBrick.typeList.BladeTower)
							{
								pbrpm.addLength 	= EditorGUILayout.IntField("", pbrpm.addLength, GUILayout.Width(boxSize));
								pbrpm.maxLength 	= EditorGUILayout.IntField("", pbrpm.maxLength, GUILayout.Width(boxSize));
							}
							else
							{
								GUI.color = Color.clear;
								GUILayout.Box("", GUILayout.Width(boxSize));
								GUILayout.Box("", GUILayout.Width(boxSize));
								GUI.color = customSkin.colorList[_stpList.IndexOf(_stp)];
							}
						}


						pbrpm.tryInvert 		= EditorGUILayout.Toggle("", pbrpm.tryInvert, GUILayout.Width(boxSize));

						if (GUILayout.Button("Remove", GUILayout.Width(boxSize)))
						{
							AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(pbrpm));
							setup.ListProcParam.Remove(pbrpm);
							_stp.LinkedParam.Remove(pbrpm);
						}
						_stp.triggerSum += pbrpm.chanceToTrigger;
						EditorUtility.SetDirty(pbrpm);
						EditorGUILayout.EndHorizontal();
					}
				}
			}
			// Add a brick and asset in the directory
			if (GUILayout.Button("Add Procedural Brick", GUILayout.Width(200f)))
			{
				addNewParam(_stp, ingameBricks[Random.Range(0, ingameBricks.Count)], Random.Range(1,10) * 10);
			}
			if (GUILayout.Button("Remove Brick Steps", GUILayout.Width(200f)))
			{
				removeBrickStep(_stp);
			}
			if (GUILayout.Button("Add " + rngBrick + " new bricks", GUILayout.Width(200f)))
			{
				addNewParam(_stp, ingameBricks[Random.Range(0, ingameBricks.Count)], Random.Range(1,10) * 10);
				addNewParam(_stp, ingameBricks[Random.Range(0, ingameBricks.Count)], Random.Range(1,10) * 10);
				addNewParam(_stp, ingameBricks[Random.Range(0, ingameBricks.Count)], Random.Range(1,10) * 10);
			}
			EditorUtility.SetDirty(setup);
		}
	}

	private void modifyStep(LinearStep _stp)
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		_stp.stepID 				= EditorGUILayout.IntField("", _stp.stepID, GUILayout.Width(stepSize));
		_stp.procType				= (LinearStep.procTrigger)System.Enum.Parse(typeof(LinearStep.procTrigger) , EditorGUILayout.EnumPopup("", _stp.procType, GUILayout.Width(stepSize)).ToString());
		_stp.allowRetrigger			= EditorGUILayout.Toggle("", _stp.allowRetrigger, GUILayout.Width(stepSize));
		_stp.MusicSource			= (AudioClip) EditorGUILayout.ObjectField(_stp.MusicSource, typeof(AudioClip),false, GUILayout.Width(stepSize));
		_stp.ScoreCondition			= EditorGUILayout.FloatField("", _stp.ScoreCondition, GUILayout.Width(stepSize));
		_stp.Crab_SpeedMultiplier	= EditorGUILayout.FloatField("", _stp.Crab_SpeedMultiplier, GUILayout.Width(stepSize));
		_stp.Enemies_SpeedMultiplier= EditorGUILayout.FloatField("", _stp.Enemies_SpeedMultiplier, GUILayout.Width(stepSize));
		EditorGUILayout.EndHorizontal();
	}
	
	
	private void displayStepHeader(LinearStep _stp)
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("StepID",GUILayout.Width(stepSize));
		GUILayout.Box("ProcType",GUILayout.Width(stepSize));
		GUILayout.Box("Allow\nRetrigger",GUILayout.Width(stepSize));
		GUILayout.Box("Music",GUILayout.Width(stepSize));
		GUILayout.Box("Condition",GUILayout.Width(stepSize));
		GUILayout.Box("CrabSpeed",GUILayout.Width(stepSize));
		GUILayout.Box("Ennemies\nSpeed",GUILayout.Width(stepSize) );
		EditorGUILayout.EndHorizontal();
	}

	private void displayProcHeader(LinearStep _stp)
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("Force",GUILayout.Width(boxSize));
		if (_stp.procType != LinearStep.procTrigger.Mixed)
		{
			GUILayout.Box("Chances",GUILayout.Width(boxSize));
		}
		GUILayout.Box("Brick",GUILayout.Width(boxSize));
		GUILayout.Box("ID",GUILayout.Width(boxSize));
		GUILayout.Box("WPM",GUILayout.Width(boxSize));
		GUILayout.Box("try\nEnable",GUILayout.Width(boxSize));
		GUILayout.Box("try\nDisable",GUILayout.Width(boxSize));
		GUILayout.Box("Toggle",GUILayout.Width(boxSize));
		GUILayout.Box("TwDir",GUILayout.Width(boxSize));
		GUILayout.Box("add\nLength",GUILayout.Width(boxSize));
		GUILayout.Box("max\nLength",GUILayout.Width(boxSize));
		GUILayout.Box("Invert",GUILayout.Width(boxSize));
		EditorGUILayout.EndHorizontal();
	}

	private ProceduralBrickParam addNewParam(LinearStep _stp, LevelBrick  _brick, int _randChance = 50)
	{
		// Création de l'instance
		brpm = ProceduralBrickParam.CreateInstance("ProceduralBrickParam") as ProceduralBrickParam;

		// Attribution des paramètres

//		_stp.Enemies_SpeedMultiplier = 1f;
//		int speedRand = Random.Range(0,10);
//		_stp.Enemies_SpeedMultiplier = (speedRand < 5 && _stp.stepID > 3) ?  1.1f : 1f;

		brpm.Brick = _brick.type;
		brpm.chanceToTrigger = _randChance;
		if (_brick.GetComponent<PatrolBrick>() != null)
		{
			brpm.giveWPM = _brick.GetComponent<PatrolBrick>().brickPath.id;
		}
		brpm.ID = _brick.brickId;
		
		int enableRand = Random.Range(0,10);
		brpm.tryEnable = (enableRand <= 5) ? true : false;

		int disableRand = Random.Range(0,10);
		brpm.tryDisable = (disableRand <= 5 && brpm.tryEnable == false && restrainDisable == false) ? true : false;

		brpm.Toggle = false;
		if (brpm.tryEnable == false && brpm.tryDisable == false)
		{
			int toggleRand = Random.Range(0,10);
			brpm.Toggle = (toggleRand <= 5 && restrainToggle == false) ? true : false;
		}

		brpm.addLength = Random.Range(3, 10);

		int changeDirRand = Random.Range(0, 10);
		string randDir = "";
		randDir += (changeDirRand <= 5) ?"U" : "";
		randDir += (changeDirRand <= 5) ?""  : "D";
		randDir += (changeDirRand <= 5) ?""  : "L";
		randDir += (changeDirRand <= 5) ?"R" : "";
		brpm.changeDirections = randDir;

		int invertRand = Random.Range(0,10);
		brpm.tryInvert = (invertRand <= 2 && _stp.stepID > 3 && restrainInvert == false) ? true : false;

		// Ajout dans les listes
		_stp.LinkedParam.Add(brpm);
		setup.ListProcParam.Add(brpm);
		AssetDatabase.CreateAsset(brpm , "Assets/Resources/Maps/" + setup.lvlParam.NAME + "/ProcParam/" + Random.Range(0,1000000).ToString() +".asset");
		EditorUtility.SetDirty(brpm);
		return brpm;
	}

	private void randomStepsButton(int _randStep, int _randbrick)
	{
//		LevelBrick currBrick = ingameBricks[0];
//		LevelBrick prevBrick = ingameBricks[0];
		if (GUILayout.Button(_randStep + " steps, generate" + _randbrick + "random bricks", GUILayout.Width(200f)))
		{
			foreach (LinearStep _step in setup.LinearSteps)
			{
				for (int j = 0; j < _randbrick; j++)
				{
					addNewParam(_step, ingameBricks[Random.Range(0, ingameBricks.Count)], Random.Range(1,10) * 10);
				}
			}
		}
	}

	private void autoAttributeWPM()
	{
		if (GUILayout.Button("AutoAttributeWPM", GUILayout.Width(200f)))
		{
			foreach (ProceduralBrickParam prm in setup.ListProcParam)
			{
				if (prm.Brick == LevelBrick.typeList.Bird || prm.Brick == LevelBrick.typeList.Chainsaw)
				{
					LevelBrick brickToFind = ingameBricks.Find((LevelBrick obj) => obj.type == prm.Brick && obj.brickId == prm.ID);
					prm.giveWPM = brickToFind.GetComponent<PatrolBrick>().brickPath.id;
				}
			}
		}
	}

	private void buttonLoadStep()
	{
		if (GUILayout.Button("LoadSteps", GUILayout.Width(200f)))
		{
			LinearStep[] listSteps = Resources.LoadAll<LinearStep>("Maps/" + setup.lvlParam.NAME +"/Steps/");
			setup.LinearSteps.Clear();
			foreach (LinearStep stp in listSteps)
			{
				setup.LinearSteps.Add(stp);
			}
			setup.LinearSteps.Sort(delegate (LinearStep x, LinearStep y)
			                      {
				if (x.stepID < y.stepID) return -1;
				if (x.stepID > y.stepID) return 1;
				else return 0;
			});
		}
	}

	private void Cleaner()
	{
		setup.ListProcParam.RemoveAll((ProceduralBrickParam obj) => obj == null);
		foreach (LinearStep lstp in setup.LinearSteps)
		{
			lstp.LinkedParam.RemoveAll((ProceduralBrickParam obj) => obj == null);
		}
	}

	private void removeBrickStep(LinearStep _stp)
	{
		List<ProceduralBrickParam> prm = setup.ListProcParam.FindAll((ProceduralBrickParam obj) => obj.stepID == _stp.stepID);
		foreach (ProceduralBrickParam _par in prm)
		{
			AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_par));
		}
		setup.ListProcParam.RemoveAll((ProceduralBrickParam obj) => obj.stepID == _stp.stepID);
		_stp.LinkedParam.RemoveAll((ProceduralBrickParam obj) => obj.stepID == _stp.stepID);
	}

	private void buttonRemoveAll()
	{
		if (GUILayout.Button("Remove All", GUILayout.Width(200f)))
		{
			foreach (ProceduralBrickParam prm in setup.ListProcParam)
			{
				AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(prm));
			}
			setup.ListProcParam.Clear();
			foreach (LinearStep lstp in setup.LinearSteps)
			{
				lstp.LinkedParam.Clear();
			}
		}
	}

	private void buttonTryStep()
	{
		if (GUILayout.Button("Gen TryStep", GUILayout.Width(200f)))
		{
			for (int i = 0; i < ingameBricks.Count; i++)
			{
				addNewParam(setup.LinearSteps[0], ingameBricks[i], 100);
			}
		}
	}

	private void randomStepParameters()
	{
		// RANDOM GENERATION SECTION
		EditorGUILayout.BeginVertical(GUILayout.Width(500f));
		GUILayout.Box("Random Generation", customSkin.skin.textField  , GUILayout.ExpandWidth(true));
		rngSteps = EditorGUILayout.IntField("RNG : Step number", rngSteps, GUILayout.ExpandWidth(true));
		rngBrick = EditorGUILayout.IntField("RNG : Bricks Number", rngBrick, GUILayout.ExpandWidth(true));
		restrainToggle = EditorGUILayout.Toggle("RNG : Restrain Toggle", restrainToggle, GUILayout.ExpandWidth(true));
		restrainDisable = EditorGUILayout.Toggle("RNG : Restrain Disable", restrainDisable, GUILayout.ExpandWidth(true));
		restrainInvert = EditorGUILayout.Toggle("RNG : Restrain Invert", restrainInvert, GUILayout.ExpandWidth(true));
		EditorGUILayout.EndVertical();
	}

	private void buttonCheckSame()
	{
//		if (GUILayout.Button("Remove similar", GUILayout.Width(200f)))
//		{
//			foreach (LinearStep _stp in setup.LinearSteps)
//			{
//
//			}
//
//		}
	}
}

