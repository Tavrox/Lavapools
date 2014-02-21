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
		initTxt = "YourNa";
		_bx = GetComponent<BoxCollider>();
		_rec = new Rect(gameObject.transform.position.x - _bx.size.x, gameObject.transform.position.y - _bx.size.y,5f,5f) ;
		InvokeRepeating("RainbowInput", 0f, 0.1f);
	}

	void Update()
	{
		_mesh.text = text;
		_mesh.color = color;
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (mousePos.x < _rec.xMax && mousePos.x > _rec.xMin && mousePos.y > _rec.yMin && mousePos.y < _rec.xMax)
		{
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
			CancelInvoke("RainbowInput");
			if (Input.inputString == "\b")
			{
				if (text.Length > 0)
				{
					text = text.Remove(text.Length-1);
				}
			}
			else
			{
				if (text.Length < 8 && Input.inputString != "<" && Input.inputString != ">" && Input.inputString != "$" && Input.inputString != "*" && Input.inputString != "."  && Input.inputString != " " )
				{
					text += Input.inputString;
				}
			}
		}
		else
		{
			color = LevelManager.TuningDocument.ColPlayer;
			InvokeRepeating("RainbowInput", 0f, 0.1f);
		}
	}

	private void RainbowInput()
	{
		Color[] _col = new Color[4];
		_col[0] = Color.yellow;
		_col[1] = Color.green;
		_col[2] = Color.red;
		_col[3] = Color.blue;
		color = _col[Random.Range(0, 3)];
	}

}
