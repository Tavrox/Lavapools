using UnityEngine;
using System.Collections;

public class RespawnUI : MonoBehaviour {
	
	public TextUI _playerInput;
	public TextUI _playerScore;
	public TextUI _playerRank;

	public void Setup()
	{
		_playerInput = FETool.findWithinChildren(this.gameObject, "Input/PlayerInput").GetComponent<TextUI>();
		_playerScore = FETool.findWithinChildren(this.gameObject, "Input/PlayerScore").GetComponent<TextUI>();
	}

	public void UpdateScore(float score)
	{
		_playerScore.text = score.ToString();
	}
}
