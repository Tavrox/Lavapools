using UnityEngine;
using System.Collections;

public class UserInput : TextUI {

	private string initTxt;
	private bool hasFocus = false;
	private bool blinking = false;
	private Vector3 mousePos;
	private BoxCollider _bx;
	private Rect _rec;

	void Start()
	{
		initTxt = "_NAME_";
		_bx = GetComponent<BoxCollider>();
		_rec = new Rect(gameObject.transform.position.x - _bx.size.x, gameObject.transform.position.y - _bx.size.y,5f,5f) ;
	}

	void Update()
	{
		_mesh.text = text;
		_mesh.color = color;
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (mousePos.x < _rec.xMax && mousePos.x > _rec.xMin && mousePos.y > _rec.yMin && mousePos.y < _rec.xMax)
		{
			if (blinking != true)
			{
				blinking = true;
				BlinkName();
			}
			if (Input.GetMouseButtonDown(0))
			{
				hasFocus = true;
			}
		}
		else if (Input.GetMouseButtonDown(0))
		{
			color.a = 1f;
			hasFocus = false;
		}
		if (hasFocus == true)
		{
			if (blinking != true)
			{
				text = "_";
				blinking = true;
				BlinkName();
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
				    && Input.inputString != "\n" && Input.inputString != "%0d"
				    && Input.inputString != "\r")
				{
//					print ("[" + Input.inputString + "]");
					text += Input.inputString;
				}
			}
		}
		else
		{
			blinking = false;
			color = LevelManager.TuningDocument.ColPlayer;
		}
	}

	private void BlinkName()
	{
		new OTTween(this, 1f).Tween("color", new Color(200f, 200f, 200f, 0.2f)).PingPong();
	}
}
