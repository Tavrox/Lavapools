using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinearLevelSetup : ScriptableObject 
{
	public LevelParameters LvlParam;
	public BrickStepParam BrickParam;
	public BrickStepParam ParamAdd;
	
	[SerializeField] private List<BrickStepParam> _ListBricks;
	[SerializeField] public List<BrickStepParam> ListBricks
	{
		get { return _ListBricks; }
		set { _ListBricks = value; }
	}
	public List<LinearStep> LinearSteps;
}