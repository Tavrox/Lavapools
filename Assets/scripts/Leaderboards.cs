using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class Leaderboards : MonoBehaviour {

	private XmlDocument xmlDoc;
	private XmlNodeList scoreNodes;
	private string url;
	private WWW www;
	public List<UserLeaderboard> listLb;



	// Use this for initialization
	IEnumerator Start () {
		url = "http://4edges-games.com/games/lavapools/leaderboard.xml";
		www = new WWW(url);
		yield return www;
		if (www.error == null)
		{
			print (www.text);
			xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(www.text);
			scoreNodes = xmlDoc.SelectNodes("leaderboard/mode");
			listLb = getUsersData(scoreNodes);
			InvokeRepeating("updateLb", 1f, 30f);
		}
		else
		{
			print (www.error);
		}
//		_scoreNodes = xmlDoc.SelectNodes("dialogs/dialog");
//		_scoreNodes = xmlDoc.SelectNodes("dialogs/dialog");
	}
	
	// Update is called once per frame
	void updateLb () {
	
	}

	public List<UserLeaderboard> getUsersData(XmlNodeList _nodelist)
	{
		List<UserLeaderboard> listUser = new List<UserLeaderboard>();
		foreach (XmlNode node in _nodelist)
		{
			print (node.Name);
			foreach (XmlNode _childNode in node.ChildNodes)
			{
				UserLeaderboard _usr = new UserLeaderboard();
				
				_usr.name 		= 			_childNode.SelectSingleNode("name").InnerText;
				_usr.userID 	= int.Parse(_childNode.Attributes.GetNamedItem("id").Value);
				_usr.userName	= 			_childNode.SelectSingleNode("name").InnerText;
				_usr.userScore 	= int.Parse(_childNode.SelectSingleNode("score").InnerText);
				_usr.timestamp 	= 			_childNode.SelectSingleNode("timestamp").InnerText;
				_usr.ranking 	= int.Parse(_childNode.SelectSingleNode("ranking").InnerText);
//				print (_childNode.SelectSingleNode("ranking").InnerText);
				if (node.Attributes.GetNamedItem("type").Value == GameModes.gameTypeList.Arcade.ToString())
				{
					_usr.typeReg = GameModes.gameTypeList.Arcade;
				}
				if (node.Attributes.GetNamedItem("type").Value == GameModes.gameTypeList.Story.ToString())
				{
					_usr.typeReg = GameModes.gameTypeList.Story;
				}

				listUser.Add(_usr);
			}
		}
		return listUser;
	}
}
