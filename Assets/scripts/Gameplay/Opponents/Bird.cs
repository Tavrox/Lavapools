using UnityEngine;
using System.Collections;

public class Bird : PatrolBrick {

	private float displayWaveTime = 0.5f;
	private OTAnimatingSprite WavesSpr;

	public void Start () 
	{
		base.Setup();
		type = typeList.Bird;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		if (brickPath != null)
		{
			brickPath.relatedBrick = this;
			brickPathId = brickPath.id;
		}
		else
		{
			Debug.Log("The path of "+gameObject.name+" is missing.");
		}
		setupPath();
		if ( FETool.findWithinChildren(gameObject, "Waves") != null)
		{
			WavesSpr = FETool.findWithinChildren(gameObject, "Waves").GetComponentInChildren<OTAnimatingSprite>();
			WavesSpr.alpha = 0f;
		}
	}


	public void turnToward(GameObject _target)
	{

	}

	
	public void fadeDelay()
	{
//		new OTTween(WavesSpr, LevelManager.GlobTuning.fadeAfterDelay).Tween("alpha", 0f);
	}

	public override void enableBrick ()
	{
		base.enableBrick();
		fadeDelay();
	}
	
	private void GameStart()
	{
		if (this != null)
		{
			WavesSpr.alpha = 1f;
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			WavesSpr.alpha = 0f;
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			WavesSpr.alpha = 1f;
		}
	}
}
