using UnityEngine;
using System.Collections;

public class VerticalScroller : MonoBehaviour {

	public LevelManager levMan;
	public Camera mainCam;
	public ColliderKiller Killer;

	public float currSlideSpeed;
	public float initSlideSpeed;
	public float boostSlideSpeed;
	
	public GameObject CameraSticker;
	public Vector3 initPosCam;
	public ScrollerHelper ScrHelp;

	// Use this for initialization
	public void Setup (LevelManager _lev) 
	{
		levMan = _lev;

		currSlideSpeed = LevelManager.LocalTuning.LinkedVertical.sliderSpeed / 1000f;
		initSlideSpeed = currSlideSpeed;
		boostSlideSpeed = currSlideSpeed * 4f;

		mainCam = Camera.main;

		CameraSticker = new GameObject("CameraSticker");
		CameraSticker.transform.parent = this.transform;
		CameraSticker.transform.position = mainCam.transform.position;
		createKiller();
		initPosCam = mainCam.transform.position;

		GameObject Helper = Instantiate(Resources.Load("Tools/ScrollerHelper")) as GameObject;
		ScrHelp = Helper.GetComponent<ScrollerHelper>();
		ScrHelp.Setup(this);
		Helper.transform.position = Vector3.zero;
		Helper.transform.position += initPosCam;
		Helper.transform.position += new Vector3 (0f, 5f,0f);
		Helper.transform.parent = this.transform;

		GameObject gameo = GameObject.Find("VerticalPlaceKeeper");
		gameo.GetComponent<VerticalPlacekeeper>().Setup(this);
		gameo.transform.parent = transform;

		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;
		GameEventManager.EndGame += EndGame;
	}

	public void createKiller()
	{
		GameObject _killer = new GameObject("VerticalKiller");
		_killer.AddComponent<BoxCollider>();
		_killer.AddComponent<ColliderKiller>();
		Killer = _killer.GetComponent<ColliderKiller>();
		BoxCollider colli = Killer.GetComponent<BoxCollider>();
		colli.size = new Vector3(100f, 1f, 0f);
		Killer.transform.position = new Vector3( mainCam.transform.position.x, mainCam.transform.position.y - 5.65f, 0f);
		Killer.transform.parent = this.transform;
	}

	public void stickCameraToY()
	{
		mainCam.transform.position = new Vector3(mainCam.transform.position.x, levMan._player.transform.position.y, -1000f);
		Killer.transform.position = new Vector3( mainCam.transform.position.x, mainCam.transform.position.y - 5.65f, 0f);
	}

	public void autoMoveCamera()
	{
		mainCam.transform.position += new Vector3(0f, currSlideSpeed, 0f);;
		Killer.transform.position = new Vector3( mainCam.transform.position.x, mainCam.transform.position.y - 5.65f, 0f);
		if (ScrHelp != null)
		{
			ScrHelp.transform.position = new Vector3( Killer.transform.position.x, mainCam.transform.position.y + 5, 0f);
		}
		if (levMan.menuManager != null)
		{
			levMan.menuManager.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, levMan.menuManager.transform.position.z);
		}
	}

	public void stopScroll()
	{
		CancelInvoke("autoMoveCamera");
	}

	
	private void GameStart()
	{
		if (this != null)
		{
//			InvokeRepeating("autoMoveCamera", 0f, 0.01f);
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			stopScroll();
		}
	}
	
	private void EndGame()
	{
		if (this != null)
		{
			stopScroll();
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			mainCam.transform.position = initPosCam;
			InvokeRepeating("autoMoveCamera", 0f, 0.01f);
		}
	}
}
