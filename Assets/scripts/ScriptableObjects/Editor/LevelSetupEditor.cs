using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(LevelSetup))]
public class LevelSetupEditor : Editor
{
	private LevelSetup step;
	private float maxSize = 600f;
	private float boxSize;
	private GUIStyle style;
	[SerializeField] private BrickStepParam brpm;
	
	public override void OnInspectorGUI()
	{
		step = (LevelSetup)target;
		boxSize = maxSize / 10 ;
		GUIEditorSkin customSkin = Resources.Load("Tools/Skins/LvlEditor") as GUIEditorSkin;
		brpm = BrickStepParam.CreateInstance("BrickStepParam") as BrickStepParam;

		base.OnInspectorGUI();

		if (step.ListBricks.Count > 0)
		{
			GUI.color = customSkin.col1;
			EditorGUILayout.BeginVertical(GUILayout.Width(maxSize)); // Start
			GUILayout.Box("Bricks setupped", GUILayout.Width(maxSize));
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
//						DestroyObject(_prm);
						AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_prm));
						step.ListBricks.Remove(_prm);
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndVertical();
		}


//		GUILayout.Box("Add a new brick", GUILayout.Width(maxSize));
//		displayBrickHeader(brpm);
//		EditorGUILayout.EndHorizontal();

		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
//		displayBrickInfo(brpm);
		if (GUILayout.Button("Add a brick", GUILayout.ExpandWidth(true)))
		{
			if (step.ListBricks == null)
			{
				step.ListBricks = new List<BrickStepParam>();
			}
//			Instantiate(Resources.Load("Tools/Parameters/test/Param"))
			AssetDatabase.CreateAsset(brpm , "Assets/Resources/Tools/Parameters/" + step.NAME + "/" + Random.Range(0,1000000).ToString() +".asset");
			EditorUtility.SetDirty(brpm);
			step.ListBricks.Add(brpm);
		}
		EditorGUILayout.EndHorizontal();
		EditorUtility.SetDirty(step);
	}

	private void displayBrickHeader(BrickStepParam _prm)
	{
		// NOTES FOR DESIGNER
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("Type",GUILayout.Width(boxSize));
		GUILayout.Box("StepID",GUILayout.Width(boxSize) );
		GUILayout.Box("ID",GUILayout.Width(boxSize) );
		GUILayout.Box("WayP",GUILayout.Width(boxSize) );
		GUILayout.Box("Enable", GUILayout.Width(boxSize));
		GUILayout.Box("Disabled", GUILayout.Width(boxSize));
		GUILayout.Box("Direction", GUILayout.Width(boxSize));
		GUILayout.Box("Length", GUILayout.Width(boxSize));
		GUILayout.Box("Invert", GUILayout.Width(boxSize));
	}
	
	private void displayBrickInfo(BrickStepParam _prm)
	{
		_prm.Brick 					= (LevelBrick.typeList)System.Enum.Parse(typeof(LevelBrick.typeList) , EditorGUILayout.EnumPopup("", _prm.Brick, GUILayout.Width(boxSize)).ToString());
		_prm.stepID					= EditorGUILayout.IntField("", _prm.stepID, GUILayout.Width(boxSize));
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