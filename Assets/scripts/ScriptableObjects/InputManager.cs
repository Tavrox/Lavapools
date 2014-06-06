using UnityEngine;
using System.Collections;

public class InputManager : ScriptableObject {

	public float BigAxisPos;
	public float BigAxisNeg;

	public float SmallAxisPos;
	public float SmallAxisNeg;

	public float BtnAxis;

	public string EnterButton = "joystick button 0";
	public string BackButton = "joystick button 1";
	public string TriggerLeftButton = "joystick button 4";
	public string TriggerRightButton = "joystick button 5";

	public KeyCode KeyUp = KeyCode.UpArrow;
	public KeyCode KeyDown = KeyCode.DownArrow;
	public KeyCode KeyLeft = KeyCode.LeftArrow;
	public KeyCode KeyRight = KeyCode.RightArrow;
	public KeyCode KeyEnter =  KeyCode.Return;

}
