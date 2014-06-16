using UnityEngine;
using System.Collections;

public class Bird : PatrolBrick {

	private float displayWaveTime = 0.5f;
	private OTAnimatingSprite WavesSpr;
	public float _diffX = 0f;
	public float _diffY = 0f;
	public float _angle = 0f;

	public void Setup () 
	{
		base.Setup();
		type = typeList.Bird;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		if (brickPath != null)
		{
			brickPath.relatedBrick.Add(this);
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


	public void rotateTowardWp(Vector3 targ ,Transform _trsf)
	{
		_diffX = targ.x - _trsf.transform.position.x;
		_diffY = _trsf.transform.position.y - targ.y;
		_angle = Mathf.Atan2( _diffX, _diffY) * Mathf.Rad2Deg;
		_trsf.rotation = Quaternion.Euler(0f, 0f, _angle - 90);
//		new OTTween(_trsf, 0.5f).Tween("rotation", Quaternion.Euler(0f, 0f, _angle - 90));
	}

	
	public void fadeDelay()
	{
		new OTTween(WavesSpr, LevelManager.GlobTuning.fadeAfterDelay).Tween("alpha", 0f);
	}

	public override void enableBrick ()
	{
		base.enableBrick();
		rotateTowardWp(currentWP.transform.position, transform);
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
