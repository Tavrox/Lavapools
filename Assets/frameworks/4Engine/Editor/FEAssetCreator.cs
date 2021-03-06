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
		ScriptableObjectUtility.CreateAsset<LinearStep>();
	}
	[MenuItem("Assets/Create/LinearLevelSetu")]
	public static void CreateLinearLevelSetup ()
	{
		ScriptableObjectUtility.CreateAsset<LinearLevelSetup>();
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
	[MenuItem("Assets/Create/DisplayChanger")]
	public static void CreateDisplayChanger ()
	{
		ScriptableObjectUtility.CreateAsset<DisplayChanger>();
	}
	[MenuItem("Assets/Create/GUIEditorSkin")]
	public static void CreateGUIEditorSkin ()
	{
		ScriptableObjectUtility.CreateAsset<GUIEditorSkin>();
	}
	[MenuItem("Assets/Create/StepParam")]
	public static void CreateBrickStepParam ()
	{
		ScriptableObjectUtility.CreateAsset<BrickStepParam>();
	}
	[MenuItem("Assets/Create/LevelParameters")]
	public static void CreateLevelParameters ()
	{
		ScriptableObjectUtility.CreateAsset<LevelParameters>();
	}
	[MenuItem("Assets/Create/VerticalLevelSetup")]
	public static void CreateVerticalLevelSetup ()
	{
		ScriptableObjectUtility.CreateAsset<VerticalLevelSetup>();
	}
	[MenuItem("Assets/Create/ProceduralBrickParam")]
	public static void CreateProceduralBrickParam ()
	{
		ScriptableObjectUtility.CreateAsset<ProceduralBrickParam>();
	}
	[MenuItem("Assets/Create/ProceduralLevelSetup")]
	public static void CreateProceduralLevelSetup ()
	{
		ScriptableObjectUtility.CreateAsset<ProceduralLevelSetup>();
	}
	[MenuItem("Assets/Create/ProceduralGeneratorType")]
	public static void CreateProceduralGeneratorType ()
	{
		ScriptableObjectUtility.CreateAsset<ProceduralGeneratorType>();
	}


}
