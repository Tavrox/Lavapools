using UnityEngine;
using System.Collections;

public class LevelChooserButton : MonoBehaviour {

	private LevelChooser chooser;
	public enum ButtonList
	{
		Arrows,
		Play,
		Leaderboard
	};
	public enum DirectionList
	{
		Right,
		Left
	};
	public DirectionList direction;

	// Use this for initialization
	public void Setup (LevelChooser _ch, DirectionList _direc) {
		chooser = _ch;
		direction = _direc;
	}

	void OnMouseDown()
	{
		chooser.SwipeThumbnail(direction);
	}
	
	// Update is called once per frame
	void Update () {

	}
}
