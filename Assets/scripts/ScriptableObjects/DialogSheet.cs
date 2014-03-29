using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class DialogSheet : ScriptableObject {

	private GameSetup.languageList currLanguage;
	private XmlDocument Doc;
	public TextAsset dialog_file; 
	public Dictionary<string, string> translated_texts; // First is ID, Second is Translation

	public void SetupTranslation(GameSetup.languageList _lang)
	{
		currLanguage = _lang;
		translated_texts = fillDicoText(currLanguage);
//		Debug.Log (translated_texts +""+_lang);
	}
	public string TranslateSingle(TextUI _txt)
	{
		string result = "lol";
		if ( translated_texts.ContainsKey(_txt.DIALOG_ID) != false)
		{
			result = translated_texts[_txt.DIALOG_ID];
		}
		else
		{
			Debug.Log(_txt.DIALOG_ID + " couldn't be found");
			result = "NOT FOUND";
		}
		return (result);
	}
	public void TranslateAll(ref TextUI[] _arrTxt)
	{
//		TextUI[] allTxt = GameObject.FindObjectsOfType(typeof(TextUI)) as TextUI[];
		foreach (TextUI _tx in _arrTxt)
		{
			if (translated_texts.ContainsKey(_tx.DIALOG_ID) != false && _tx.dontTranslate == false)
			{		
				_tx.text = translated_texts[_tx.DIALOG_ID];
			}
			else
			{
				if (_tx.dontTranslate == false)
				{
					Debug.Log(_tx.DIALOG_ID + " couldn't be found");
					_tx.text = "NOT FOUND";
				}
			}
			
		}
	}

	private Dictionary<string, string> fillDicoText(GameSetup.languageList _lang)
	{
		Doc = new XmlDocument();
//		Debug.Log("Loading" +_lang.ToString());
		Doc.LoadXml(dialog_file.text);

		Dictionary<string, string> translate = new Dictionary<string, string>();
		XmlNodeList TextNode = Doc.SelectNodes("texts");

		foreach (XmlNode node in TextNode)
		{
			XmlNodeList entries = node.SelectNodes("entry");
			foreach (XmlNode entry in entries)
			{
				string entryID = entry.Attributes.GetNamedItem("id").Value;
				XmlNodeList LangEntry = entry.SelectNodes(_lang.ToString());
				string entryTranslation = LangEntry.Item(0).InnerText;
				entryTranslation = entryTranslation.Replace("	", "");
				entryTranslation = entryTranslation.Replace("/n", "\n");

				if (translate.ContainsKey(entryID) == false)
				{
					translate.Add(entryID, entryTranslation);
				}
			}
		}




		//
		//		XmlWriter _writer = XmlWriter.Create("lol.xml");
		//		_writer.WriteComment("prout man");
		//
		//		scoreLines = new Label[10];
		//		scoreLines = GetComponentsInChildren<Label>();
		//		if (www.error == null && www.isDone)
		//		{
		//			
//		scoreNodes = xmlDoc.SelectNodes("leaderboard/mode");
		//			writeInLeaderboard(xmlDoc, scoreNodes);
//		List<UserLeaderboard> listUser = new List<UserLeaderboard>();
		//		foreach (XmlNode node in _nodelist)
		//		{
		//			foreach (XmlNode _childNode in node.ChildNodes)
		//			{
		////				UserLeaderboard _usr = new UserLeaderboard();
		//				UserLeaderboard _usr = (UserLeaderboard)ScriptableObject.CreateInstance("UserLeaderboard");
		//				
		//				_usr.name 		= 			_childNode.SelectSingleNode("name").InnerText;
		//				_usr.userID 	= int.Parse(_childNode.Attributes.GetNamedItem("id").Value);
		//				_usr.userName	= 			_childNode.SelectSingleNode("name").InnerText;
		//				_usr.userBestScore 	= int.Parse(_childNode.SelectSingleNode("score").InnerText);
		//				_usr.timestamp 	= 			_childNode.SelectSingleNode("timestamp").InnerText;
		//				if (node.Attributes.GetNamedItem("type").Value == GameModes.gameTypeList.Arcade.ToString())
		//				{
		//					_usr.typeReg = GameModes.gameTypeList.Arcade;
		//				}
		//				if (node.Attributes.GetNamedItem("type").Value == GameModes.gameTypeList.Story.ToString())
		//				{
		//					_usr.typeReg = GameModes.gameTypeList.Story;
		//				}
		//
		//				listUser.Add(_usr);
		//			}
		//		}
		return (translate);
	}
}
