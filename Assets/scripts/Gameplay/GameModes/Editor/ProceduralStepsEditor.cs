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
	
	public override void OnInspectorGUI()
	{
		setup = (ProceduralLevelSetup)target;
		boxSize = maxSize / 10 ;
		stepSize = maxSize / 6;
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
			displayStepHeader();
			displayStepInfo(setup.LinearSteps);
			EditorGUILayout.EndVertical();
		}
	
		EditorUtility.SetDirty(setup);
	}

	private void displayStepHeader()
	{
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("StepID",GUILayout.Width(stepSize));
		GUILayout.Box("Music",GUILayout.Width(stepSize));
		GUILayout.Box("Score Condition",GUILayout.Width(stepSize));
		GUILayout.Box("CrabSpeed*",GUILayout.Width(stepSize));
		GUILayout.Box("EnemiesSpeed*",GUILayout.Width(stepSize) );
		EditorGUILayout.EndHorizontal();
	}

	private void displayStepInfo(List<LinearStep> _stpList)
	{
		foreach (LinearStep _stp in _stpList)
		{
			EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
			_stp.stepID 				= EditorGUILayout.IntField("", _stp.stepID, GUILayout.Width(stepSize));
			_stp.MusicSource			= (AudioClip) EditorGUILayout.ObjectField(_stp.MusicSource, typeof(AudioClip),false, GUILayout.Width(stepSize));
			_stp.ScoreCondition			= EditorGUILayout.FloatField("", _stp.ScoreCondition, GUILayout.Width(stepSize));
			_stp.Crab_SpeedMultiplier	= EditorGUILayout.FloatField("", _stp.Crab_SpeedMultiplier, GUILayout.Width(stepSize));
			_stp.Enemies_SpeedMultiplier= EditorGUILayout.FloatField("", _stp.Enemies_SpeedMultiplier, GUILayout.Width(stepSize));
			EditorGUILayout.EndHorizontal();
			EditorUtility.SetDirty(_stp);
		}
	}

}

