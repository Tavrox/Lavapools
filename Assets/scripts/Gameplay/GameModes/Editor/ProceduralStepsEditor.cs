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
			
			displayStepHeader();
			EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
			_stp.stepID 				= EditorGUILayout.IntField("", _stp.stepID, GUILayout.Width(stepSize));
			_stp.procType				= (LinearStep.procTrigger)System.Enum.Parse(typeof(LinearStep.procTrigger) , EditorGUILayout.EnumPopup("", _stp.procType, GUILayout.Width(stepSize)).ToString());
			_stp.MusicSource			= (AudioClip) EditorGUILayout.ObjectField(_stp.MusicSource, typeof(AudioClip),false, GUILayout.Width(stepSize));
			_stp.ScoreCondition			= EditorGUILayout.FloatField("", _stp.ScoreCondition, GUILayout.Width(stepSize));
			_stp.Crab_SpeedMultiplier	= EditorGUILayout.FloatField("", _stp.Crab_SpeedMultiplier, GUILayout.Width(stepSize));
			_stp.Enemies_SpeedMultiplier= EditorGUILayout.FloatField("", _stp.Enemies_SpeedMultiplier, GUILayout.Width(stepSize));
			EditorGUILayout.EndHorizontal();

			displayProcHeader();
			foreach (ProceduralBrickParam pbrpm in _stp.LinkedParam)
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
				pbrpm.Brick				= (LevelBrick.typeList)System.Enum.Parse(typeof(LevelBrick.typeList) , EditorGUILayout.EnumPopup("", pbrpm.Brick, GUILayout.Width(boxSize)).ToString());
				pbrpm.stepID 			= _stp.stepID;
				pbrpm.ID 				= EditorGUILayout.IntField("", pbrpm.ID, GUILayout.Width(boxSize));
				pbrpm.giveWPM 			= EditorGUILayout.TextField("", pbrpm.giveWPM, GUILayout.Width(boxSize));
				pbrpm.tryEnable 		= EditorGUILayout.Toggle("", pbrpm.tryEnable, GUILayout.Width(boxSize));
				pbrpm.tryDisable 		= EditorGUILayout.Toggle("", pbrpm.tryDisable, GUILayout.Width(boxSize));
				pbrpm.Toggle 			= EditorGUILayout.Toggle("", pbrpm.Toggle, GUILayout.Width(boxSize));

				if (pbrpm.Brick == LevelBrick.typeList.ArrowTower || pbrpm.Brick == LevelBrick.typeList.BladeTower)
				{
					pbrpm.changeDirections = EditorGUILayout.TextField("", pbrpm.changeDirections, GUILayout.Width(boxSize));
				}
				if ( pbrpm.Brick == LevelBrick.typeList.BladeTower)
				{
					pbrpm.addLength 	= EditorGUILayout.IntField("", pbrpm.addLength, GUILayout.Width(boxSize));
					pbrpm.maxLength 	= EditorGUILayout.IntField("", pbrpm.maxLength, GUILayout.Width(boxSize));
				}

				pbrpm.tryInvert 		= EditorGUILayout.Toggle("", pbrpm.tryInvert, GUILayout.Width(boxSize));

				if (GUILayout.Button("Remove", GUILayout.Width(boxSize)))
				{
					AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(pbrpm));
					setup.ListProcParam.Remove(pbrpm);
					_stp.LinkedParam.Remove(pbrpm);
				}
				_stp.triggerSum += pbrpm.chanceToTrigger;
				EditorGUILayout.EndHorizontal();
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
	
	
	private void displayStepHeader()
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("StepID",GUILayout.Width(stepSize));
		GUILayout.Box("ProcType",GUILayout.Width(stepSize));
		GUILayout.Box("Music",GUILayout.Width(stepSize));
		GUILayout.Box("Condition",GUILayout.Width(stepSize));
		GUILayout.Box("CrabSpeed",GUILayout.Width(stepSize));
		GUILayout.Box("EnnmySpeed",GUILayout.Width(stepSize) );
		EditorGUILayout.EndHorizontal();
	}

	private void displayProcHeader()
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("Force",GUILayout.Width(boxSize));
		GUILayout.Box("Chances",GUILayout.Width(boxSize));
		GUILayout.Box("Type",GUILayout.Width(boxSize));
		GUILayout.Box("BrkID",GUILayout.Width(boxSize));
		GUILayout.Box("GiveWPM",GUILayout.Width(boxSize));
		GUILayout.Box("tryEnable",GUILayout.Width(boxSize));
		GUILayout.Box("tryDisable",GUILayout.Width(boxSize));
		GUILayout.Box("Toggle",GUILayout.Width(boxSize));
		GUILayout.Box("TwDir",GUILayout.Width(boxSize));
		GUILayout.Box("+Length",GUILayout.Width(boxSize));
		GUILayout.Box("maxLength",GUILayout.Width(boxSize));
		GUILayout.Box("Invert",GUILayout.Width(boxSize));
		EditorGUILayout.EndHorizontal();
	}
}

