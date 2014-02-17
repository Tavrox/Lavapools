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


}
