using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelThumbnail : MonoBehaviour
{
	private OTSprite Preview;
	public OTSprite Locker;
	public PhpLeaderboards tinyLeaderboard;

	public GameSetup.LevelList nameLv;
	public bool isStartSlot = false;
	public bool isEndSlot = false;
	public bool Locked = false;
	public LevelInfo Info;
	public MiscButton linkedPlayBtn;

	public void Setup(GameSetup.LevelList _set, bool _isLocked)
	{
		nameLv = _set;
		Locked = _isLocked;
		if (nameLv != GameSetup.LevelList.None)
		{
			Info = Instantiate(Resources.Load("Tuning/Levels/" +  nameLv.ToString())) as LevelInfo;
		}
		gameObject.name = _set.ToString();
		Locker = FETool.findWithinChildren(gameObject, "Lock").GetComponentInChildren<OTSprite>();
		if (Locked == true)
		{
			Locker.alpha = 1f;
		}
		else
		{
			Locker.alpha = 0f;
		}
		linkedPlayBtn = GetComponentInChildren<MiscButton>();
	}

	public LevelThumbnail FindThumbAround(List<LevelThumbnail> _listTh, LevelThumbnail _thumb, LevelChooserButton.DirectionList _dir)
	{
		LevelThumbnail res = null;
		LevelThumbnail lastThumb = _listTh[_listTh.Count-1];

		int nextThumb = _listTh.FindIndex(th => th.name == _thumb.name);
		if (_dir == LevelChooserButton.DirectionList.Left)
		{
			res = _listTh[nextThumb-1];
		}
		else
		{
			res = _listTh[nextThumb+1];
		}

		if (res == null)
		{
			Debug.LogError("Thumb not found");
		}
		return res;
	}	
}