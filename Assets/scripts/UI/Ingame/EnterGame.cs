using UnityEngine;
using System.Collections;

public class EnterGame : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			GameEventManager.TriggerRespawn("Enter Game");
		}
	
	}
}
