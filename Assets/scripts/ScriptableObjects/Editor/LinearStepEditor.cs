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

//		base.DrawDefaultInspector();
		base.OnInspectorGUI();

		if (step.BrickParam != null)
		{
			// NOTES FOR DESIGNER
			GUILayout.Box("NEW THINGS", GUILayout.ExpandWidth(true));
			EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
			GUILayout.Box("Type",GUILayout.Width(boxSize));
			GUILayout.Box("ID",GUILayout.Width(boxSize) );
			GUILayout.Box("Waypoint",GUILayout.Width(boxSize) );
			GUILayout.Box("Enable", GUILayout.Width(boxSize));
			GUILayout.Box("Disabled", GUILayout.Width(boxSize));
			GUILayout.FlexibleSpace();
			if (step.BrickParam.Brick == LevelBrick.typeList.FireTower || step.BrickParam.Brick == LevelBrick.typeList.ArrowTower )
			{
				GUILayout.Box("Direction", GUILayout.Width(boxSize));
				if (step.BrickParam.Brick == LevelBrick.typeList.FireTower)
				{

					GUILayout.Box("TowLength", GUILayout.Width(boxSize));
					GUILayout.Box("TowSwapRot", GUILayout.Width(boxSize));
				}
			}
			GUILayout.Box("", GUILayout.Width(boxSize));
			EditorGUILayout.EndHorizontal();

			// FIELDS TO ENTER
			EditorGUILayout.BeginHorizontal(GUILayout.Width(maxSize));
			displayBrickInfo(step.BrickParam);
			if (GUILayout.Button("Save", GUILayout.Width(boxSize)))
			{
				step.ListBricks.Add(step.BrickParam);
				step.SetuppedBricks.Add(serializeBrickSetup(step.BrickParam));
			}
			EditorGUILayout.EndHorizontal();
		}
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
		res += _brk.Brick.ToString();
		res += "/";
		res += _brk.ID.ToString();
		res += "/";
		res += _brk.WaypointsAttributed;
		res += "/";
		if (_brk.Enable == true)
		{
			res += "enabled";
			res += "/";
		}
		if (_brk.Disable == true)
		{
			res += "disabled";
			res += "/";
		}
		res += _brk.Directions;
		res += "/";
		res += _brk.TowerLength.ToString();
		res += "/";
		if (_brk.TowerSwapRot == true)
		{
			res += "TowerSwapped";
			res += "/";
		}
		return res;
	}
}