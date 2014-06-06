using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProceduralLevelSetup : ScriptableObject 
{
	public LevelParameters lvlParam;
	public List<LinearStep> LinearSteps;
	public List<ProceduralBrickParam> ListProcParam;

}
