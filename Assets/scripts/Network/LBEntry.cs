using UnityEngine;
using System.Collections;
[ExecuteInEditMode]

public class LBEntry : UIThing {

	private float _offX = -171.8f;
	private float _offY = -552.68f;
	public float gapOneTwo;
	public float gapTwoThree;
	public int size = 10;
	public GUISkin skin;

	private GUISkin[] skins = new GUISkin[3];
	public string Rank;
	public string UserName;
	public string Score;
	private Color[] color = new Color[3];

	void OnGUI()
	{
		GUI.depth = Mathf.RoundToInt(transform.position.z);
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);

		Rank = "01";
		Score = "TAVROX";
		Score = "1000000";

		skin = Resources.Load("Skins/Score") as GUISkin;

		skins[0] = Resources.Load("Skins/ScoreRank") as GUISkin;
		skins[1] = Resources.Load("Skins/ScoreUser") as GUISkin;
		skins[2] = Resources.Load("Skins/ScorePoints") as GUISkin;
		if (skins[0] != null)
		{
			skins[0].label.normal.textColor = FETool.TuningDoc.ColRank;
			skins[1].label.normal.textColor = FETool.TuningDoc.ColPlayer;
			skins[2].label.normal.textColor = FETool.TuningDoc.ColScore;

			skin.label.fontSize = size;
			
			skin.label.normal.textColor = color[0];
			GUI.skin = skins[0];
			GUI.Label(new Rect(point.x + _offX, Screen.currentResolution.height - point.y + _offY, 200, 200), Rank);
			skin.label.normal.textColor = color[1];
			GUI.skin = skins[1];
			GUI.Label(new Rect(point.x + _offX + gapOneTwo, Screen.currentResolution.height - point.y + _offY, 200, 200), UserName);
			skin.label.normal.textColor = color[2];
			GUI.skin = skins[2];
			GUI.Label(new Rect(point.x + _offX + gapTwoThree, Screen.currentResolution.height - point.y + _offY, 200, 200), Score);
		}
	}
}
