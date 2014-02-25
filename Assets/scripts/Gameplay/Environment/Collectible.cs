using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour {

	public enum ListCollectible
	{
		BigGem,
		TinyGem
	};
	public ListCollectible typeCollectible;
	public int value;
	public LevelManager _levMan;
	public CollectiblePlaces _relatedPlace;
	public bool picked = false;
	private OTSprite _spr;
	private OTSprite _animSpr;

	// Use this for initialization
	public void Setup (LevelManager lm) {

		_levMan = lm;
		if (GetComponentInChildren<OTSprite>() != null)
		{
			_spr = GetComponentInChildren<OTSprite>();
		}
		if (GetComponentInChildren<OTAnimatingSprite>() != null)
		{
			_animSpr = GetComponentInChildren<OTAnimatingSprite>();
		}
		_spr.alpha = 0f;
	
	}

	public void Pop()
	{
		if (_spr != null)
		{
			new OTTween(_spr, 1f).Tween("alpha", 1f);
		}
		if (_animSpr != null)
		{
			new OTTween(_animSpr, 1f).Tween("alpha", 1f);
		}
	}

	public void Vanish()
	{
		if (picked == false)
		{
			picked = true;
			if (_spr != null)
			{
				new OTTween(_spr, 1f).Tween("alpha", 0f).Wait(2f);
				new OTTween(_spr, 0.5f, OTEasing.BackOut).Tween("depth", -15f);
				new OTTween(gameObject.transform, 1.5f, OTEasing.BackOut).Tween("localScale", new Vector3(1.5f, 1.5f, 1f)).PingPong();
				new OTTween(gameObject.transform, 1.5f, OTEasing.BackIn).Tween("position", new Vector3(0f,5.5f,0f));
			}
			if (_animSpr != null)
			{
				new OTTween(_animSpr, 1f).Tween("alpha", 0f).Wait(2f);
				new OTTween(_animSpr, 0.5f, OTEasing.BackOut).Tween("depth", -15f);
				new OTTween(gameObject.transform, 1.5f, OTEasing.BackOut).Tween("localScale", new Vector3(1.5f, 1.5f, 1f)).PingPong();
				new OTTween(gameObject.transform, 1.5f, OTEasing.BackIn).Tween("position", new Vector3(0f,5.5f,0f));
			}
			Destroy (gameObject, 3f);
		}
	}

}
