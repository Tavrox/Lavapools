using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArrowTower : LevelBrick {

	private OTAnimatingSprite _mainSpr;
	public List<LevelTools.DirectionList> enabledDirection;
	public List<Arrow> linkedArrow;
	public Arrow launchArrow;
	public int maxPool = 50;
	public OTSprite dirUp;
	public OTSprite dirLeft;
	public OTSprite dirDown;
	public OTSprite dirRight;
	
	// Use this for initialization
	public void Setup () 
	{
		base.Setup();
		type = typeList.ArrowTower;

		dirUp = FETool.findWithinChildren(gameObject, "Direction/up").GetComponentInChildren<OTSprite>();
		dirLeft = FETool.findWithinChildren(gameObject, "Direction/left").GetComponentInChildren<OTSprite>();
		dirDown = FETool.findWithinChildren(gameObject, "Direction/down").GetComponentInChildren<OTSprite>();
		dirRight = FETool.findWithinChildren(gameObject, "Direction/right").GetComponentInChildren<OTSprite>();
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GameEventManager.EndGame += EndGame;
		createArrows(maxPool);
	}

	private void createArrows(int poolSize)
	{
		// Create pool of Arrows with poolSize
		for (int i = 0; i < poolSize ; i++)
		{
			GameObject _arrow = Instantiate(Resources.Load("Bricks/Opponent/SingleArrow")) as GameObject;
			_arrow.transform.parent = FETool.findWithinChildren(gameObject,"Arrows").transform;
			_arrow.name += i.ToString();
			_arrow.transform.position = _levMan.OuterSpawn.transform.position;
			_arrow.GetComponent<Arrow>().speed = LevelManager.LocalTuning.Arrow_Speed;
			_arrow.GetComponent<Arrow>().linkedTower = this;
			linkedArrow.Add(_arrow.GetComponent<Arrow>());
		}
	}

	private List<LevelTools.DirectionList> changeDirection(List<LevelTools.DirectionList> _dirList)
	{
		enabledDirection.Clear();
		enabledDirection = _dirList;
		return enabledDirection; // Return result
	}

	override public void enableBrick()
	{
		isEnabled = true;
		triggerShooting();
	}
	
	public void triggerShooting()
	{
		// Setup the frequency of shoots
		InvokeRepeating("ShootArrow", 0f, speed);
	}

	override public void disableBrick()
	{
		isEnabled = false;
		stopShooting();
	}

	private void ShootArrow()
	{
		// Shoot an Arrow
		if (enabledDirection.Count > 0)
		{
			foreach (LevelTools.DirectionList dir in enabledDirection)
			{
				List<Arrow> AvailableArrows = linkedArrow.FindAll( (Arrow obj) => obj.Busy == false);
				if (AvailableArrows.Count > 0)
				{
					launchArrow = AvailableArrows[AvailableArrows.Count -1];
					launchArrow.giveDirection(dir);
				}
			}
		}
	}

	public void stopShooting()
	{
		CancelInvoke("ShootArrow");
	}

	
	private void GameStart()
	{
		if (this != null)
		{

		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			
		}
	}
	
	private void EndGame()
	{
		if (this != null)
		{

		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{

		}
	}
}
