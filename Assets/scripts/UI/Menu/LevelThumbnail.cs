using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelThumbnail : MonoBehaviour
{
	private OTSprite _preview;
	public GameSetup.LevelList name;
	public bool isStartSlot = false;
	public bool isEndSlot = false;

	public void Setup(GameSetup.LevelList _set)
	{
		name = _set;
	}

	public LevelThumbnail FindThumbAround(List<LevelThumbnail> _listTh, LevelThumbnail _thumb)
	{
		LevelThumbnail res = null;
		LevelThumbnail lastThumb = _listTh[_listTh.Count-1];

		if (_thumb.name != lastThumb.name)
		{
			int nextThumb = _listTh.FindIndex(th => th.name == _thumb.name);
			res = _listTh[nextThumb+1];
		}
		else
		{
			res = _listTh[0];
		}
		if (res == null)
		{
			Debug.LogError("Thumb not found");
		}
		return res;
	}
	public LevelThumbnail findPreviousWaypoints(List<LevelThumbnail> _listTh, LevelThumbnail _thumb)
	{
		LevelThumbnail res = null;
		LevelThumbnail lastThumb = _listTh[_listTh.Count-1];
		
		if (_thumb.name != lastThumb.name)
		{
			int nextThumb = _listTh.FindIndex(th => th.name == _thumb.name);
			res = _listTh[nextThumb-1];
		}
		else
		{
			res = _listTh[0];
		}
		if (res == null)
		{
			Debug.LogError("Thumb not found");
		}
		return res;
	}
	
	
}