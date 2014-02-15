using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Procedural : MonoBehaviour {

	private LevelManager _levMan;
	private List<ProceduralSteps> _listSteps;

	private LevelSetup SETUP;

	[HideInInspector] public ProceduralSteps _CURRENTSTEP;


	// Use this for initialization
	void Awake () 
	{
		string path = "Procedural/Level" + Application.loadedLevelName+"/";
		_levMan = GameObject.Find("LevelManager").GetComponent<LevelManager>();
		_listSteps = new List<ProceduralSteps>();
		LPTuning TuningDoc = _levMan.TuningDocument;

		SETUP = Resources.Load(path + "Setup") as LevelSetup;

		for (int i = 1; i <= SETUP.numberOfSteps ; i++)
		{
			_listSteps.Add( Resources.Load( path + i) as ProceduralSteps);
		}

		_CURRENTSTEP = _listSteps[0];
		InvokeRepeating("checkScore", 0f, 1f);
	}

	private void checkScore()
	{
		foreach (ProceduralSteps _step in _listSteps)
		{
			if ( _step.priority > _CURRENTSTEP.priority && _step.ScoreCondition < _levMan.score)
			{
				_CURRENTSTEP = _step;
			}

		}
	}

	private List<ProceduralSteps> loadSteps(int _levelID)
	{
		List<ProceduralSteps> _list = new List<ProceduralSteps>();
		switch (_levelID)
		{
			case 1 :
			{

			break;
			}
			case 2 :
			{
				
			break;
			}

		}
		return _list;
	}
}
