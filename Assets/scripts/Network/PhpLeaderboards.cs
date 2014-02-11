﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhpLeaderboards : MonoBehaviour
{
	private string secretKey = "lavapools014.."; // Edit this value and make sure it's the same as the one stored on the server
	public string addScoreURL = "http://4edges-games.com/games/lavapools/log.php?"; //be sure to add a ? to your url
	public string highscoreURL = "http://4edges-games.com/games/lavapools/display.php";
	public List<UserLeaderboard> ListUser;
	private List<LBEntry> ListEntries;
	private int lbLength =  6;
	
	void Start()
	{
		ListUser = new List<UserLeaderboard>();
		ListEntries = new List<LBEntry>();
//		ScriptableObject.CreateInstance("UserLeaderboard");
		for (int i = 0 ; i <= 14 ; i++)
		{
			ListUser.Add(ScriptableObject.CreateInstance("UserLeaderboard") as UserLeaderboard);
		}
		for (int i = 1 ; i <= 15 ; i++)
		{
			GameObject _newEntry = Instantiate(Resources.Load("UI/LBEntry")) as GameObject;
			_newEntry.gameObject.transform.parent = transform;
			_newEntry.name = i.ToString();

			LBEntry _entry = _newEntry.GetComponent<LBEntry>();
			_entry.Rank = i.ToString();
		}
		StartCoroutine(GetScores());
	}


 	private void updateScores()
	{

	}

	private void gatherScores()
	{

	}
	
	// remember to use StartCoroutine when calling this function!
	IEnumerator PostScores(string name, int score)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
//		string hash = MD.Md5Sum(name + score + secretKey);
		string hash = "";
		
		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
		
		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done
		
		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}
	
	// Get the scores from the MySQL DB to display in a GUIText.
	// remember to use StartCoroutine when calling this function!
	IEnumerator GetScores()
	{
		WWW hs_get = new WWW(highscoreURL);
		yield return hs_get;
		
		if (hs_get.error != null)
		{
			print("There was an error getting the high score: " + hs_get.error);
		}
		else
		{
			string[] entries = hs_get.text.Split(']');
			Debug.Log ("Number of Entries = " + entries.Length);
			for (int i = 0; i < entries.Length -1 ; i++)
			{
				ListUser[i].ranking = i;
				ListUser[i].userName = entries[i].Split('|')[0];
				ListUser[i].userBestScore = int.Parse(entries[i].Split('|')[1]);		
//				print (user + score);
			}
		}
	}
	
	public void sortLeaderboard(ref List<UserLeaderboard> _listLb, int length)
	{
		length = lbLength;
		
		_listLb.Sort(delegate (UserLeaderboard x, UserLeaderboard y)
		             {
			if (x.userBestScore > y.userBestScore) return -1;
			if (x.userBestScore < y.userBestScore) return 1;
			else return 0;
		});
		
		foreach (UserLeaderboard _usr in _listLb)
		{
			_usr.ranking = _listLb.IndexOf(_usr) + 1;
		}
		for (int i = 0; i <= length - 1; i++)
		{
			
		}
	}

	public void displayLeaderboard(List<UserLeaderboard> _listLb, ref Label[] _lines)
	{
		int _lengthLb = lbLength;
		if (_listLb.Count < _lengthLb )
		{
			Debug.LogError("There are not enough entries");
		}
		for (int i = 0; i <= _lengthLb - 1; i++)
		{
			_lines[i].text = _listLb[i].ranking + " - " + _listLb[i].userName + " - " + _listLb[i].userBestScore;
		}
	}
	
}