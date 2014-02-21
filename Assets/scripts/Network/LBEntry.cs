using UnityEngine;
using System.Collections;

public class LBEntry : MonoBehaviour {

	public float gapOneTwo;
	public float gapTwoThree;
	public int size = 10;
	public TextMesh Rank;
	public TextMesh UserName;
	public TextMesh Score;

	public void Setup()
	{
		Rank = FETool.findWithinChildren(gameObject, "Rank").GetComponent<TextMesh>();
		UserName = FETool.findWithinChildren(gameObject, "Username").GetComponent<TextMesh>();
		Score = FETool.findWithinChildren(gameObject, "Score").GetComponent<TextMesh>();

		Rank.text = "01";
		UserName.text = "TEST";
		Score.text = "1000000";
	}

	public void UpdateScore(string _rank, string _score, string _username )
	{
		Rank.text = _rank;
		UserName.text = _username;
		Score.text = _score;
	}
}
