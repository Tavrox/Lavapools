using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Procedural : MonoBehaviour {

	private LevelManager _levMan;
	[HideInInspector] public ProceduralSteps _CURRENTSTEP;
	private List<ProceduralSteps> _listSteps;


	// Use this for initialization
	void Awake () 
	{
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_listSteps = new List<ProceduralSteps>();
		LPTuning TuningDoc = _levMan.TuningDocument;

		for (int i = 0 ; i < TuningDoc.scoreSteps.Count; i++)
		{
			ProceduralSteps _step = ScriptableObject.CreateInstance("ProceduralSteps") as ProceduralSteps;
			_step.condition = ProceduralSteps.conditionEnum.Score;
			_step.ScoreCondition = TuningDoc.scoreSteps[i];
			_step.priority = i+1;
			_listSteps.Add(_step);
		}
		_CURRENTSTEP = _listSteps[0];
		InvokeRepeating("checkScore", 0f, 1f);
	}

	private void checkScore()
	{
		foreach (ProceduralSteps _step in _listSteps)
		{
			if ( _step.priority > _CURRENTSTEP.priority)
			{
//				_CURRENTSTEP = 
			}

		}

	}
}
