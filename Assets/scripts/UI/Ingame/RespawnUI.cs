using UnityEngine;
using System.Collections;

public class RespawnUI : MonoBehaviour {

	private PlayerData Pdata;
	public TextUI _playerInput;
	public TextUI _playerScore;
	public TextUI _playerRank;

	public void Setup()
	{
		Pdata = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
		_playerInput = FETool.findWithinChildren(this.gameObject, "Input/PlayerInput").GetComponent<TextUI>();
		_playerScore = FETool.findWithinChildren(this.gameObject, "Input/PlayerScore").GetComponent<TextUI>();
		_playerInput.text = Pdata.PROFILE.Player_Name;
	}

	public void UpdateScore(float score)
	{
		_playerScore.text = score.ToString();
	}
}
