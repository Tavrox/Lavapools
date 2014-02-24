﻿using UnityEngine;
using System.Collections;

public class UserInput : TextUI {

	private string initTxt;
	private bool hasFocus = false;
	private bool blinking = false;
	private Vector3 mousePos;
	private BoxCollider _bx;
	private Rect _rec;

	private Color _col1;
	private Color _col2;
	private float smallDelay = 1.25f;
	private bool canModify = false;

	void Start()
	{
		initTxt = "_NAME_";
		_bx = GetComponent<BoxCollider>();
		_rec = new Rect(gameObject.transform.position.x - _bx.size.x, gameObject.transform.position.y - _bx.size.y,5f,5f) ;
		_col1 = LevelManager.TuningDocument.ColRank;
		_col2 = LevelManager.TuningDocument.ColInput;
		color = _col1;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		InvokeRepeating("switchColor", 0f, 0.35f);
	}

	void Update()
	{
		_mesh.text = text;
		_mesh.color = color;
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (LevelManager.GAMESTATE == GameEventManager.GameState.GameOver && canModify == true)
		{
			if (mousePos.x < _rec.xMax && mousePos.x > _rec.xMin && mousePos.y > _rec.yMin && mousePos.y < _rec.xMax)
			{

			}
			else if (Input.GetMouseButtonDown(0))
			{
				color.a = 1f;
			}
			if (Input.anyKey == true && text == "_NAME_" && Input.GetKey(KeyCode.Space) != true  && Input.GetKey(KeyCode.Return) != true)
			{
				text = "";
				CancelInvoke("switchColor");
			}

			if (Input.inputString == "\b")
			{
				if (text.Length > 0)
				{
					text = text.Remove(text.Length-1);
				}
			}
			else
			{
				if (text.Length < 8 && Input.inputString != "<" && Input.inputString != ">" 
				    && Input.inputString != "$" && Input.inputString != "*"
				    && Input.inputString != "."  && Input.inputString != " "
				    && Input.inputString != "\n" && Input.inputString != "\r")
				{
					text += Input.inputString;
					text.Replace(" ", "_");
				}
			}
		}
	}

	private void switchColor()
	{
		if (color == _col1)
		{
			color = _col2;
		}
		else
		{
			color = _col1;
		}
	}

	private void Respawn()
	{
		CancelInvoke("switchColor");
		canModify = false;
	}

	private void GameOver()
	{
		StartCoroutine("EnableInput");
		InvokeRepeating("switchColor", 0f, 0.5f);
	}

	IEnumerator EnableInput()
	{
		yield return new WaitForSeconds(smallDelay);
		canModify = true;
	}

	private void BlinkName()
	{
		new OTTween(this, 1f).Tween("color", new Color(200f, 200f, 200f, 0.2f)).PingPong();
	}
}
