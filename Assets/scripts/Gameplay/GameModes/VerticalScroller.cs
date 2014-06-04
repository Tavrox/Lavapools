using UnityEngine;
using System.Collections;

public class VerticalScroller : MonoBehaviour {

	public LevelManager levMan;
	public Camera mainCam;
	public GameObject CameraSticker;

	// Use this for initialization
	public void Setup (LevelManager _lev) 
	{
		levMan = _lev;
		mainCam = Camera.main;
		CameraSticker = GameObject.Find("CameraSticker");
		mainCam.transform.position = CameraSticker.transform.position;
		Debug.Log(mainCam.transform.position);
	}
	
	// Update is called once per frame
	void Update () 
	{
		stickCameraToY();
	}

	public void stickCameraToY()
	{
		mainCam.transform.position = new Vector3(mainCam.transform.position.x, levMan.transform.position.y, 0f);
	}
}
