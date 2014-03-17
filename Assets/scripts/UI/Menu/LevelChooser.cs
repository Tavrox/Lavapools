using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelChooser : MonoBehaviour {

	private TextUI levelName;
	private LevelChooserButton _btnLeft;
	private LevelChooserButton _btnRight;
	private GameSetup SETUP;

	private GameObject ThumbGO;
	private List<LevelThumbnail> Thumbs =  new List<LevelThumbnail>();

	private LevelThumbnail currThumb;
	
	public void Setup () 
	{
		SETUP = MainTitleUI.getSetup();
		ThumbGO = new GameObject("Thumbnails");
		ThumbGO.transform.parent = gameObject.transform;
		levelName = FETool.findWithinChildren(gameObject, "LevelTitle/LevelName").GetComponent<TextUI>();
		_btnLeft = FETool.findWithinChildren(gameObject, "SelectLeft").GetComponent<LevelChooserButton>();
		_btnRight = FETool.findWithinChildren(gameObject, "SelectRight").GetComponent<LevelChooserButton>();

		Thumbs.Clear();
		foreach (GameSetup.LevelList _lvl in SETUP.ActivatedLevels)
		{
			LevelThumbnail _th = CreateThumbnail(_lvl);
			Thumbs.Add(_th);
		}
		for (int j = 0; j < Thumbs.Count ; j++)
		{
			Thumbs[j].gameObject.transform.position = new Vector3(j * 13.625f, -1f, -10f);
		}

		Thumbs[0].isStartSlot = true;
		Thumbs[Thumbs.Count-1].isEndSlot = true;

		_btnLeft.Setup(this, LevelChooserButton.DirectionList.Left);
		_btnRight.Setup(this, LevelChooserButton.DirectionList.Right);
		currThumb = Thumbs[0];
	}

	public void SwipeThumbnail(LevelChooserButton.DirectionList _dir)
	{
		Vector3 mod = new Vector3 (13.625f, 0f, 0f);
		if (_dir == LevelChooserButton.DirectionList.Left)
		{
			new OTTween(ThumbGO.transform, 0.8f, OTEasing.BounceOut).Tween("position", ThumbGO.transform.position + mod);
		}
		else
		{
			new OTTween(ThumbGO.transform, 0.8f, OTEasing.BounceOut).Tween("position", ThumbGO.transform.position - mod);
		}
	}

	public LevelThumbnail CreateThumbnail(GameSetup.LevelList _name)
	{
		GameObject _thumb = Instantiate(Resources.Load("Tools/Thumb")) as GameObject;
		LevelThumbnail _lvl = _thumb.AddComponent<LevelThumbnail>();
		_lvl.Setup( _name);
		_thumb.transform.parent = ThumbGO.transform;
		return _lvl;
	}

	/*
	public List<LevelSlot> CreateSlots(int numberOfSlots)
	{
		List<LevelSlot> _transList = new List<LevelSlot>();
		for (int i = -1 ; i < numberOfSlots ; i++)
		{
			GameObject _sl = new GameObject(i.ToString());
			_sl.transform.position = new Vector3(i * 13.625f, -1f, -10f);
			_sl.transform.parent = SlotGO.transform;
			_sl.AddComponent<LevelSlot>();
			_transList.Add(_sl.GetComponent<LevelSlot>());
		}
		return _transList;
	}
	*/
}