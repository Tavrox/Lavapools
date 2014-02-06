using UnityEngine;
using System.Collections;
using UnityEditor;

public class FETuning {

	[MenuItem("Assets/Create/Tuning")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<LPTuning>();
	}

}
