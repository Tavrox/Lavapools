using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuThing : MonoBehaviour {
	
	public enum ListMenu
	{
		RespawnNotif,
	};
	public ListMenu menu;
	public bool animate = false;
	private OTSprite spr;
	private List<GameObject> menuObjects;
	public GameObject chosenAnchor;
	public int levelToLoad;
	
	// Use this for initialization
	void Start () {
		menuObjects = new List<GameObject>();

		if (GetComponentInChildren<OTSprite>() != null)
		{
			spr = GetComponentInChildren<OTSprite>();
		}
		if (animate == true)
		{
			InvokeRepeating("animateItem", 0f, 2.5f);
		}
	
	}

	private void OnMouseDown()
	{
		menuObjects = new List<GameObject>();
	}
	
	private void triggerCredits()
	{

	}

	IEnumerator loadLevel(int _lvl)
	{
		yield return new WaitForSeconds(0.3f);
		Debug.Log("loading level");
		Application.LoadLevel(_lvl);
	}

	private GameObject findObject(string _str)
	{
		GameObject result = GameObject.Find("Menu/" + _str);
		return result;
	}

	private void fadeOutObjects(List<GameObject> _objects)
	{
		foreach (GameObject _obj in _objects)
		{
			if (_obj != null)
			{
				OTSprite objectSprite = _obj.GetComponentInChildren<OTSprite>();
				_obj.collider.enabled = false;
				OTTween _tween = new OTTween(objectSprite,1f).Tween("alpha",0f);	
				if (objectSprite.alpha == 0)
				{
					_obj.SetActive(false);
				}
			}
		}
	}

	private void fadeInObjects(List<GameObject> _objects)
	{
		foreach (GameObject _obj in _objects)
		{
			if (_obj != null)
			{
				OTSprite objectSprite = _obj.GetComponentInChildren<OTSprite>();
				OTTween _tween = new OTTween(objectSprite,1f).Tween("alpha",1f);	
				if (objectSprite.alpha == 1)
				{
					_obj.SetActive(true);
				}
				_obj.collider.enabled = true;
			}
		}
	}

	private void goToAnchor(GameObject _Anchor)
	{
		Transform _cam = GameObject.Find("UI/Main Camera").transform.transform;
		if (_cam == null)
		{Debug.Log("Camera hasn't been found");}
		OTTween _tween = new OTTween(_cam,2f).Tween("position", new Vector3(_Anchor.transform.position.x, _Anchor.transform.position.y, _cam.position.z), OTEasing.StrongOut );
	}

	private void translateCamera(float posX)
	{
		Transform _cam = GameObject.Find("UI/Main Camera").transform.transform;
		OTTween _tween = new OTTween(_cam,2f).Tween("position", new Vector3(posX, _cam.position.y, _cam.position.z), OTEasing.StrongOut );
	}

	private void animateItem()
	{
		if (spr != null)
		{
			float randRange = Random.Range(0.1f,0.5f);
			OTTween _tween = new OTTween(spr,2.5f)
				.Tween("size", new Vector2(spr.size.x - randRange, spr.size.y - randRange) )
			.PingPong();
		}
	}
}
