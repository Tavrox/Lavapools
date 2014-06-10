using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(ProceduralLevelSetup))]
public class ProceduralStepsEditor : Editor
{
	private ProceduralLevelSetup setup;
	private float maxSize = 550f;
	private float boxSize;
	private float stepSize;
	[SerializeField] private ProceduralBrickParam brpm;
	private GUIEditorSkin customSkin;

	private bool displayEachParam = false;
	
	public override void OnInspectorGUI()
	{
		setup = (ProceduralLevelSetup)target;
		boxSize = maxSize / 8 ;
		stepSize = maxSize / 6;
		customSkin = Resources.Load("Tools/Skins/LvlEditor") as GUIEditorSkin;

		EditorGUILayout.HelpBox( "There are Two types of trigger : Brick by Brick and Mixed." +
			"\n Mixed take all non-forced bricks in a step and take 1 / X (X = nb of bricks) chance to trigger one of them. Ex : 2 bricks = 50 / 50 each" +
			"\n Brick by Brick take all brick and try to trigger it, according to the 'chance trigger' parameter ex : 2 bricks Can be spread for 80 / 20", MessageType.Info, true);

		base.OnInspectorGUI();

		if (GUILayout.Button("LoadSteps", GUILayout.ExpandWidth(true)))
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
		renameButton();

		if (setup.LinearSteps.Count > 0)
		{
			
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
			EditorGUILayout.Separator();
			GUI.color = customSkin.colorList[_stpList.IndexOf(_stp)];
			EditorGUILayout.HelpBox( "STEP " + _stp.stepID, MessageType.Info, true);
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

						pbrpm.forceTrigger 		= EditorGUILayout.Toggle("", pbrpm.forceTrigger, GUILayout.Width(boxSize));
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
						pbrpm.tryEnable 		= EditorGUILayout.Toggle("", pbrpm.tryEnable, GUILayout.Width(boxSize));
						pbrpm.tryDisable 		= EditorGUILayout.Toggle("", pbrpm.tryDisable, GUILayout.Width(boxSize));
						pbrpm.Toggle 			= EditorGUILayout.Toggle("", pbrpm.Toggle, GUILayout.Width(boxSize));
						string newName = pbrpm.stepID + "/" + pbrpm.Brick.ToString() + "/" + pbrpm.ID + "/" + pbrpm.giveWPM; 
						AssetDatabase.RenameAsset(pbrpm.name, newName);

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
				brpm = ProceduralBrickParam.CreateInstance("ProceduralBrickParam") as ProceduralBrickParam;
				_stp.LinkedParam.Add(brpm);
				setup.ListProcParam.Add(brpm);
				AssetDatabase.CreateAsset(brpm , "Assets/Resources/Maps/" + setup.lvlParam.NAME + "/ProcParam/" + Random.Range(0,1000000).ToString() +".asset");
				EditorUtility.SetDirty(brpm);

			}
			EditorUtility.SetDirty(setup);
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
			EditorGUILayout.Separator();
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
		GUILayout.Box("AllowRetrigger",GUILayout.Width(stepSize));
		GUILayout.Box("Music",GUILayout.Width(stepSize));
		GUILayout.Box("Condition",GUILayout.Width(stepSize));
		GUILayout.Box("CrabSpeed",GUILayout.Width(stepSize));
		GUILayout.Box("EnnmySpeed",GUILayout.Width(stepSize) );
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
		GUILayout.Box("tryEnable",GUILayout.Width(boxSize));
		GUILayout.Box("tryDisable",GUILayout.Width(boxSize));
		GUILayout.Box("Toggle",GUILayout.Width(boxSize));
		GUILayout.Box("TwDir",GUILayout.Width(boxSize));
		GUILayout.Box("+Length",GUILayout.Width(boxSize));
		GUILayout.Box("maxLength",GUILayout.Width(boxSize));
		GUILayout.Box("Invert",GUILayout.Width(boxSize));
		EditorGUILayout.EndHorizontal();
	}

	private void renameButton()
	{
		if (GUILayout.Button("Rename proc Brick", GUILayout.Width(200f)))
		{

		}
	}

}

