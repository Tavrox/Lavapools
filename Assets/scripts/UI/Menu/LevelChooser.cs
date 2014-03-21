using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelChooser : MonoBehaviour {

	private TextUI levelName;
	private LevelChooserButton _btnLeft;
	private LevelChooserButton _btnRight;
	private GameSetup SETUP;
	private PlayerData PLAYERDATA;

	private GameObject ThumbGO;
	private List<LevelThumbnail> Thumbs =  new List<LevelThumbnail>();

	private LevelThumbnail currThumb;
	private Vector3 gapThumbs = new Vector3(20f, 0f, -10f);
	
	public void Setup () 
	{
		SETUP = MainTitleUI.getSetup();
		PLAYERDATA = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
		ThumbGO = new GameObject("Thumbnails");
		ThumbGO.transform.parent = gameObject.transform;
		ThumbGO.transform.localPosition = new Vector3(0f,0f,0f);
		levelName = FETool.findWithinChildren(gameObject, "LevelTitle/LevelName").GetComponent<TextUI>();
		_btnLeft = FETool.findWithinChildren(gameObject, "SelectLeft").GetComponent<LevelChooserButton>();
		_btnRight = FETool.findWithinChildren(gameObject, "SelectRight").GetComponent<LevelChooserButton>();

		Thumbs.Clear();
		foreach (LevelInfo _lvl in PLAYERDATA.PROFILE.ActivatedLevels)
		{
			LevelThumbnail _th = CreateThumbnail(_lvl);
			Thumbs.Add(_th);
		}
		for (int j = 0; j < Thumbs.Count ; j++)
		{
//			Thumbs[j].gameObject.transform.localPosition = new Vector3(0f,0f,0f);
			Thumbs[j].gameObject.transform.localPosition = new Vector3(j * gapThumbs.x, 0f, gapThumbs.z);
		}

		Thumbs[0].isStartSlot = true;
		Thumbs[Thumbs.Count-1].isEndSlot = true;

		_btnLeft.Setup(this, LevelChooserButton.DirectionList.Left);
		_btnRight.Setup(this, LevelChooserButton.DirectionList.Right);
		currThumb = Thumbs[0];
		checkCurrThumb();
	}

	public void SwipeThumbnail(LevelChooserButton _btn)
	{
		Vector3 mod = new Vector3 (20f, 0f, 0f);
		if (_btn.direction == LevelChooserButton.DirectionList.Left)
		{
			new OTTween(ThumbGO.transform, _btn.twDuration, OTEasing.BounceOut).Tween("position", ThumbGO.transform.position + mod);
		}
		else
		{
			new OTTween(ThumbGO.transform, _btn.twDuration, OTEasing.BounceOut).Tween("position", ThumbGO.transform.position - mod);
		}
		currThumb = currThumb.FindThumbAround(Thumbs, currThumb, _btn.direction);
		levelName.text = currThumb.nameLv.ToString();
		checkCurrThumb();
	}

	private void checkCurrThumb()
	{
		_btnLeft.TriggerBtn(true);
		_btnRight.TriggerBtn(true);

		if (currThumb.isStartSlot == true)
		{
			_btnLeft.TriggerBtn(false);
		}
		if (currThumb.isEndSlot == true)
		{
			_btnRight.TriggerBtn(false);
		}
	}

	public LevelThumbnail CreateThumbnail(LevelInfo _info)
	{
		GameObject _thumb = Instantiate(Resources.Load("Tools/Thumb")) as GameObject;
		LevelThumbnail _lvl = _thumb.AddComponent<LevelThumbnail>();
		_thumb.transform.parent = ThumbGO.transform;
		_lvl.Setup(_info.LvlName, _info.locked);
		_thumb.gameObject.transform.localPosition = new Vector3(0f,0f, 0f);
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