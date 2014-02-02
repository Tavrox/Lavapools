using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Leaderboards : MonoBehaviour {

	private XmlDocument xmlDoc;
	private XmlNodeList scoreNodes;
	private string url;
	private WWW www;
	public List<UserLeaderboard> listLb;
	private Label[] scoreLines;
	private int lbLength =  6;

	// Use this for initialization
	IEnumerator Start () {
		
		url = "http://4edges-games.com/games/lavapools/leaderboard.xml";
		www = new WWW(url);
		yield return www;

		XmlWriter _writer = XmlWriter.Create("lol.xml");
		_writer.WriteComment("prout man");

		scoreLines = new Label[10];
		scoreLines = GetComponentsInChildren<Label>();
		if (www.error == null && www.isDone)
		{
			xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(www.text);
		
			xmlDoc.CreateElement("lol");
			_writer.WriteRaw("OMAGAD");
			xmlDoc.Save(_writer);

			scoreNodes = xmlDoc.SelectNodes("leaderboard/mode");
			writeInLeaderboard(xmlDoc, scoreNodes);
			listLb = getUsersData(scoreNodes);
			InvokeRepeating("updateLb", 1f, 30f);
			sortLeaderboard(ref listLb, lbLength);
			displayLeaderboard(listLb, ref scoreLines);
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
			foreach (XmlNode _childNode in node.ChildNodes)
			{
//				UserLeaderboard _usr = new UserLeaderboard();
				UserLeaderboard _usr = (UserLeaderboard)ScriptableObject.CreateInstance("UserLeaderboard");
				
				_usr.name 		= 			_childNode.SelectSingleNode("name").InnerText;
				_usr.userID 	= int.Parse(_childNode.Attributes.GetNamedItem("id").Value);
				_usr.userName	= 			_childNode.SelectSingleNode("name").InnerText;
				_usr.userScore 	= int.Parse(_childNode.SelectSingleNode("score").InnerText);
				_usr.timestamp 	= 			_childNode.SelectSingleNode("timestamp").InnerText;
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

	public void displayLeaderboard(List<UserLeaderboard> _listLb, ref Label[] _lines)
	{
		int _lengthLb = lbLength;
		if (_listLb.Count < _lengthLb )
		{
			Debug.LogError("There are not enough entries");
		}
		for (int i = 0; i <= _lengthLb - 1; i++)
		{
			_lines[i].text = _listLb[i].ranking + " - " + _listLb[i].userName + " - " + _listLb[i].userScore;
		}
	}

	public void sortLeaderboard(ref List<UserLeaderboard> _listLb, int length)
	{
		length = lbLength;

		_listLb.Sort(delegate (UserLeaderboard x, UserLeaderboard y)
     	{
			if (x.userScore > y.userScore) return -1;
			if (x.userScore < y.userScore) return 1;
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

	public void writeInLeaderboard(XmlDocument _doc, XmlNodeList _node)
	{
//		XmlNode newNode = _doc.CreateNode(XmlNodeType.Element, "ok", "test");
//		print (newNode.Name);
//		_doc.CreateElement("troll");
//		_doc.DocumentElement.AppendChild(newNode);
//		print ("done");
//		xmlDoc.Save(www.url);
		
	}
}


