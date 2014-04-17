using UnityEngine;
using System.Collections;

public class ParentMenu : MonoBehaviour 
{
	public bool padEntered = false;
	public PlayerData PLAYERDAT;
	public SubMenu currentActiveMenu;
	public MiscButton currFocusedbtn;

	public void Setup()
	{
		if (GameObject.FindGameObjectWithTag("PlayerData") == null)
		{
			GameObject _dataObj = Instantiate(Resources.Load("Presets/PlayerData")) as GameObject;
			PLAYERDAT = _dataObj.GetComponent<PlayerData>();
		}
		else
		{
			PLAYERDAT = GameObject.FindGameObjectWithTag("PlayerData").GetComponent<PlayerData>();
		}
		InvokeRepeating("checkPadAvailable", 0f, 1f);
	}
	
	
	void checkPadAvailable()
	{
		if (Input.GetJoystickNames().Length > 0)
		{
			padEntered = true;
//			changeState(MenuStates.Start);
			CancelInvoke("checkPadAvailable");
		}
	}
}
