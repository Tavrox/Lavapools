using UnityEngine;
using System.Collections;

public class InputManager : ScriptableObject {

	public float X_AxisPos_Sensibility;
	public float X_AxisNeg_Sensibility;
	public float Y_AxisPos_Sensibility;
	public float Y_AxisNeg_Sensibility;
	public string EnterButton = "joystick button 0";

	public KeyCode KeyUp = KeyCode.UpArrow;
	public KeyCode KeyDown = KeyCode.DownArrow;
	public KeyCode KeyLeft = KeyCode.LeftArrow;
	public KeyCode KeyRight = KeyCode.RightArrow;
	public KeyCode KeyEnter =  KeyCode.Return;

}
