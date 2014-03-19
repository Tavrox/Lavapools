using UnityEngine;
using System.Collections;
using UnityEditor;

public class FEAssetCreator {

	[MenuItem("Assets/Create/Tuning")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<LPTuning>();
	}
	[MenuItem("Assets/Create/ProceduralSteps")]
	public static void CreateStep ()
	{
		ScriptableObjectUtility.CreateAsset<ProceduralSteps>();
	}
	[MenuItem("Assets/Create/LevelSetup")]
	public static void CreateLevelSetup ()
	{
		ScriptableObjectUtility.CreateAsset<LevelSetup>();
	}
	[MenuItem("Assets/Create/BrickInfo")]
	public static void CreateBrickInfo ()
	{
		ScriptableObjectUtility.CreateAsset<BrickInfo>();
	}
	[MenuItem("Assets/Create/DepthTuning")]
	public static void CreateDepthTuning ()
	{
		ScriptableObjectUtility.CreateAsset<DepthTuning>();
	}
	[MenuItem("Assets/Create/LevelInfo")]
	public static void CreateLevelInfo ()
	{
		ScriptableObjectUtility.CreateAsset<LevelInfo>();
	}
	[MenuItem("Assets/Create/GameSetup")]
	public static void CreateGameSetup ()
	{
		ScriptableObjectUtility.CreateAsset<GameSetup>();
	}
	[MenuItem("Assets/Create/DialogSheet")]
	public static void CreateDialogSheet ()
	{
		ScriptableObjectUtility.CreateAsset<DialogSheet>();
	}
	[MenuItem("Assets/Create/PlayerProfile")]
	public static void CreatePlayerProfile ()
	{
		ScriptableObjectUtility.CreateAsset<PlayerProfile>();
	}



}
