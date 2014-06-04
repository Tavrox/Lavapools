using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(LinearLevelSetup))]
public class LinearLevelSetupEditor : Editor
{
	private LinearLevelSetup step;
	private float maxSize = 550f;
	private float boxSize;
	private float stepSize;
	private GUIStyle style;
	[SerializeField] private BrickStepParam brpm;
	
	public override void OnInspectorGUI()
	{
		step = (LinearLevelSetup)target;
		boxSize = maxSize / 10 ;
		stepSize = maxSize / 6;
		GUIEditorSkin customSkin = Resources.Load("Tools/Skins/LvlEditor") as GUIEditorSkin;
		brpm = BrickStepParam.CreateInstance("BrickStepParam") as BrickStepParam;

		base.OnInspectorGUI();
		if (step.LvlParam == null)
		{
			Debug.Log("Check for parameters in setups");
		}

		if (GUILayout.Button("LoadSteps", GUILayout.ExpandWidth(true)))
		{
			LinearStep[] listSteps = Resources.LoadAll<LinearStep>("Linear/" + step.LvlParam.NAME +"/Steps/");
			step.Procedural_Steps.Clear();
			foreach (LinearStep stp in listSteps)
			{
				step.Procedural_Steps.Add(stp);
			}
			step.Procedural_Steps.Sort(delegate (LinearStep x, LinearStep y)
			                           {
				if (x.stepID < y.stepID) return -1;
				if (x.stepID > y.stepID) return 1;
				else return 0;
			});
		}
		
		if (step.Procedural_Steps.Count > 0)
		{
			
			EditorGUILayout.BeginVertical(GUILayout.Width(maxSize)); 
			displayStepHeader();
			displayStepInfo(step.Procedural_Steps);
			EditorGUILayout.EndVertical();

		}

		if (step.ListBricks.Count > 0)
		{
			GUI.color = customSkin.col1;
			EditorGUILayout.BeginVertical(GUILayout.Width(maxSize)); 
			GUI.color = customSkin.col3;
			displayBrickHeader(brpm);
			if (GUILayout.Button("SortID", GUILayout.Width(boxSize)))
			{
				step.ListBricks.Sort(delegate (BrickStepParam x, BrickStepParam y)
				                     {
					if (x.stepID < y.stepID) return -1;
					if (x.stepID > y.stepID) return 1;
					else return 0;
				});
			}
			EditorGUILayout.EndHorizontal(); // End
			foreach(BrickStepParam _prm in step.ListBricks)
			{
				if (_prm != null)
				{
					EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
					displayBrickInfo(AssetDatabase.LoadAssetAtPath( AssetDatabase.GetAssetPath(_prm), typeof(BrickStepParam)) as BrickStepParam);
					EditorUtility.SetDirty(AssetDatabase.LoadAssetAtPath( AssetDatabase.GetAssetPath(_prm), typeof(BrickStepParam)) as BrickStepParam);
					if (GUILayout.Button("Remove", GUILayout.Width(boxSize)))
					{
						AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_prm));
						step.ListBricks.Remove(_prm);
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndVertical();
		}

		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
//		displayBrickInfo(brpm);
		if (GUILayout.Button("Add a brick", GUILayout.ExpandWidth(true)))
		{
			if (step.ListBricks == null)
			{
				step.ListBricks = new List<BrickStepParam>();
			}
			AssetDatabase.CreateAsset(brpm , "Assets/Resources/Tools/Parameters/" + step.LvlParam.NAME + "/" + Random.Range(0,1000000).ToString() +".asset");
			EditorUtility.SetDirty(brpm);
			step.ListBricks.Add(brpm);
		}
		EditorGUILayout.EndHorizontal();


		EditorUtility.SetDirty(step);
		EditorUtility.SetDirty(step.LvlParam);
	}

	private void displayBrickHeader(BrickStepParam _prm)
	{
		// NOTES FOR DESIGNER
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("Type",GUILayout.Width(boxSize * 1.5f));
		GUILayout.Box("Step",GUILayout.Width(boxSize / 1.5f));
		GUILayout.Box("ID",GUILayout.Width(boxSize));
		GUILayout.Box("WayP",GUILayout.Width(boxSize) );
		GUILayout.Box("Enable", GUILayout.Width(boxSize));
		GUILayout.Box("Disabled", GUILayout.Width(boxSize));
		GUILayout.Box("Directio", GUILayout.Width(boxSize));
		GUILayout.Box("Length", GUILayout.Width(boxSize));
		GUILayout.Box("Invert", GUILayout.Width(boxSize));
	}
	
	private void displayBrickInfo(BrickStepParam _prm)
	{
		_prm.Brick 					= (LevelBrick.typeList)System.Enum.Parse(typeof(LevelBrick.typeList) , EditorGUILayout.EnumPopup("", _prm.Brick, GUILayout.Width(boxSize*1.5f)).ToString());
		_prm.stepID					= EditorGUILayout.IntField("", _prm.stepID, GUILayout.Width(boxSize / 1.5f));
		_prm.ID						= EditorGUILayout.IntField("", _prm.ID, GUILayout.Width(boxSize));
		_prm.WaypointsAttributed 	= EditorGUILayout.TextField("", _prm.WaypointsAttributed, GUILayout.Width(boxSize));
		_prm.Enable 				= EditorGUILayout.Toggle("", _prm.Enable, GUILayout.Width(boxSize));
		_prm.Disable 				= EditorGUILayout.Toggle("", _prm.Disable, GUILayout.Width(boxSize));

		if (_prm.Brick == LevelBrick.typeList.BladeTower || _prm.Brick == LevelBrick.typeList.ArrowTower)
		{
			_prm.Directions = EditorGUILayout.TextField("", _prm.Directions, GUILayout.Width(boxSize));
			if (_prm.Brick == LevelBrick.typeList.BladeTower)
			{
				_prm.TowerLength = EditorGUILayout.IntField("", _prm.TowerLength, GUILayout.Width(boxSize));
			}
			else
			{
				GUILayout.Box("",GUILayout.Width(boxSize));
				_prm.TowerLength = 0;
			}
		}
		else
		{
			GUILayout.Box("",GUILayout.Width(boxSize));
			GUILayout.Box("",GUILayout.Width(boxSize));
			_prm.Directions = "";
		}

		if (_prm.Brick == LevelBrick.typeList.BladeTower || 
		    _prm.Brick == LevelBrick.typeList.Chainsaw ||
		    _prm.Brick == LevelBrick.typeList.Bird)
		{
			_prm.Invert = EditorGUILayout.Toggle("", _prm.Invert, GUILayout.Width(boxSize));
		}
		else
		{
			GUILayout.Box("",GUILayout.Width(boxSize));
			_prm.Invert = false;
		}
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
	
	private string serializeBrickSetup(BrickStepParam _brk)
	{
		// SERIALIZE BRICK INFO TO DISPLAY ENABLED BRICKS.
		string res = "";
		res += "[";
		res += _brk.Brick.ToString();
		res += "] [ID=";
		res += _brk.ID.ToString();
		res += "] [WP=";
		res += _brk.WaypointsAttributed;
		res += "]";
		if (_brk.Enable == true)
		{
			res += "[Enabled] ";
		}
		if (_brk.Disable == true)
		{
			res += " [Disabled]";
		}
		res += "[Dir=";
		res += _brk.Directions;
		res += "] [TwLen_";
		res += _brk.TowerLength.ToString();
		res += "]";
		if (_brk.Invert == true)
		{
			res += " [Inverted]";
		}
		return res;
	}
	
	private Color randomColor()
	{
		return new Color(Random.Range(0f, 255f), Random.Range(0f,255f), 255f);
	}
}