using UnityEngine;
using System.Collections;

public class RespawnUI : MonoBehaviour {

	private PlayerData Pdata;
	private SubMenu parentSub;
	public TextUI _playerInput;
	public TextUI _playerScore;
	public TextUI _playerRank;

	public void Setup(SubMenu _sub)
	{
		GameEventManager.GameOver += GameOver;
		parentSub = _sub;
		Pdata = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
		_playerInput = FETool.findWithinChildren(gameObject, "Input/PlayerInput").GetComponent<TextUI>();
		_playerScore = FETool.findWithinChildren(gameObject, "Input/PlayerScore").GetComponent<TextUI>();
		_playerInput.text = Pdata.PROFILE.Player_Name;
	}

	public void UpdateScore(float score)
	{
		if (this != null)
		{
			_playerScore.text = score.ToString();
		}
	}

	public void GameOver()
	{
		if (this != null)
		{
			UpdateScore(parentSub._menuMan._levman.score);
		}
	}
}
