using UnityEngine;
using System.Collections;

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
	
	
}