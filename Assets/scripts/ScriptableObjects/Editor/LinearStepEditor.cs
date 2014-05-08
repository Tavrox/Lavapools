using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LinearStep))]
public class LinearStepEditor : Editor
{
	private LinearStep step;
	private float maxSize;
	private float boxSize;
	private GUIStyle style;
	
	public override void OnInspectorGUI()
	{
		step = (LinearStep)target;
		maxSize = step.wishedGUISize;
		boxSize = step.wishedGUISize / 8 ;
		GUIEditorSkin customSkin = Resources.Load("Tools/Skins/LvlEditor") as GUIEditorSkin;

//		base.DrawDefaultInspector();
		base.OnInspectorGUI();

		if (step.ListBricks.Count > 0)
		{
			GUI.color = customSkin.col1;
			EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
			GUILayout.Box("Bricks setupped", GUILayout.ExpandWidth(true));
			GUI.color = customSkin.col3;
			foreach(BrickStepParam prmtr in step.ListBricks)
			{
				if (prmtr != null)
				{
					EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
					GUILayout.Box(serializeBrickSetup(prmtr), GUILayout.ExpandWidth(true));
					if (GUILayout.Button("Remove", GUILayout.ExpandWidth(true)))
					{
						step.ListBricks.Remove(prmtr);
					}
					EditorGUILayout.EndHorizontal();
				}
			}
			EditorGUILayout.EndVertical();
		}

		// NOTES FOR DESIGNER
		GUI.color = customSkin.col2;
		GUILayout.Box("Add a new brick", GUILayout.ExpandWidth(true));
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		GUILayout.Box("Type",GUILayout.Width(boxSize));
		GUILayout.Box("ID",GUILayout.Width(boxSize) );
		GUILayout.Box("WayP",GUILayout.Width(boxSize) );
		GUILayout.Box("Enable", GUILayout.Width(boxSize));
		GUILayout.Box("Disabled", GUILayout.Width(boxSize));
		if (step.BrickParam.Brick == LevelBrick.typeList.FireTower || step.BrickParam.Brick == LevelBrick.typeList.ArrowTower )
		{
			GUILayout.Box("Direction", GUILayout.Width(boxSize));
			if (step.BrickParam.Brick == LevelBrick.typeList.FireTower)
			{
				GUILayout.Box("Length", GUILayout.Width(boxSize));
				GUILayout.Box("SwapRot", GUILayout.Width(boxSize));
			}
		}
		GUILayout.Box("", GUILayout.Width(boxSize));
		EditorGUILayout.EndHorizontal();

		// FIELDS TO ENTER
		EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
		displayBrickInfo(step.BrickParam);
		if (GUILayout.Button("Save", GUILayout.Width(boxSize)))
		{
			if (step.BrickParam.Directions.Contains("U") || step.BrickParam.Directions.Contains("D") ||
			    step.BrickParam.Directions.Contains("L") || step.BrickParam.Directions.Contains("R") || step.BrickParam.Directions == ""  )
			{
				step.ListBricks.Add(step.BrickParam);
			}
			else
			{
				Debug.LogError("Direction wrong setup");
			}
		}
		EditorGUILayout.EndHorizontal();
	}

	private void displayBrickInfo(BrickStepParam _prm)
	{
		_prm.Brick = (LevelBrick.typeList)System.Enum.Parse(typeof(LevelBrick.typeList) , EditorGUILayout.EnumPopup("", _prm.Brick, GUILayout.Width(boxSize)).ToString());
		_prm.ID = EditorGUILayout.IntField("", _prm.ID, GUILayout.Width(boxSize));
		_prm.WaypointsAttributed = EditorGUILayout.TextField("", _prm.WaypointsAttributed, GUILayout.Width(boxSize));
		_prm.Enable = EditorGUILayout.Toggle("", _prm.Enable, GUILayout.Width(boxSize));
		_prm.Disable = EditorGUILayout.Toggle("", _prm.Disable, GUILayout.Width(boxSize));
		if (_prm.Brick == LevelBrick.typeList.FireTower || _prm.Brick == LevelBrick.typeList.ArrowTower)
		{
			_prm.Directions = EditorGUILayout.TextField("", _prm.Directions, GUILayout.Width(boxSize));
			if (_prm.Brick == LevelBrick.typeList.FireTower)
			{
				_prm.TowerLength = EditorGUILayout.IntField("", _prm.TowerLength, GUILayout.Width(boxSize));
				_prm.TowerSwapRot = EditorGUILayout.Toggle("", _prm.TowerSwapRot, GUILayout.Width(boxSize));
			}
			else
			{
				_prm.TowerLength = 0;
				_prm.TowerSwapRot = false;
			}
		}
		else
		{
			_prm.Directions = "";
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
		if (_brk.TowerSwapRot == true)
		{
			res += " [TwSwapped]";
		}
		return res;
	}

	private Color randomColor()
	{
		return new Color(Random.Range(0f, 255f), Random.Range(0f,255f), 255f);
	}
}