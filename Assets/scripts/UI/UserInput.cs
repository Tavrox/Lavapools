using UnityEngine;
using System.Collections;

public class UserInput : TextUI {

	private string initTxt;
	private bool hasFocus = false;
	private Vector3 mousePos;
	private BoxCollider _bx;
	private Rect _rec;

	void Start()
	{
		initTxt = "YourName";
		_bx = GetComponent<BoxCollider>();
		_rec = new Rect(gameObject.transform.position.x - _bx.size.x, gameObject.transform.position.y - _bx.size.y,5f,5f) ;
	}

	void Update()
	{
		_mesh.text = text;
		_mesh.color = color;
		mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		if (mousePos.x < _rec.xMax && mousePos.x > _rec.xMin && mousePos.y > _rec.yMin && mousePos.y < _rec.xMax && Input.GetMouseButtonDown(0))
		{
			hasFocus = true;
		}
		else if (Input.GetMouseButtonDown(0))
		{
			hasFocus = false;
		}
		if (hasFocus)
		{
			color = Color.white;
			if (Input.inputString == "\b")
			{
				print ("Remove");
				if (text.Length >0)
				{
					text = text.Remove(text.Length-1);
				}
			}
			else
			{
				if (text.Length < 10)
				{
					text += Input.inputString;
				}
			}
		}
		else
		{
			color = Color.red;
		}
	}
}
