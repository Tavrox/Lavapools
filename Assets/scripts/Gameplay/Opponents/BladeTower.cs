using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BladeTower : OppTower {

	public List<BladePart> FbList = new List<BladePart>();
	public List<List<BladePart>> FbDirList = new List<List<BladePart>>();
	public List<BladePart> FbListUp = new List<BladePart>();
	public List<BladePart> FbListDown = new List<BladePart>();
	public List<BladePart> FbListRight = new List<BladePart>();
	public List<BladePart> FbListLeft = new List<BladePart>();

	public GameObject FbHead;
	public GameObject parUp;
	public GameObject parLeft;
	public GameObject parRight;
	public GameObject parDown;
	public GameObject Hat;
	public float marginUp;
	public float marginDown;
	public float marginLeft;
	public float marginRight;
	private Vector3 moveMarUp;
	private Vector3 moveMarDown;
	private Vector3 moveMarLeft;
	private Vector3 moveMarRight;
	private float currRot;

	private int towerLength;


	// Use this for initialization
	public void Setup () 
	{
		base.Setup();
		type = typeList.BladeTower;

		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		FbHead = FETool.findWithinChildren(gameObject, "Head");
		parUp = FETool.findWithinChildren(gameObject, "Head/Blades/up");
		parLeft = FETool.findWithinChildren(gameObject, "Head/Blades/left");
		parRight = FETool.findWithinChildren(gameObject, "Head/Blades/right");
		parDown = FETool.findWithinChildren(gameObject, "Head/Blades/down");
		Hat = FETool.findWithinChildren(gameObject, "Hat");

		moveMarUp = new Vector3(0f, marginUp,0f);
		moveMarLeft = new Vector3(marginLeft * -1f, 0f,0f);
		moveMarRight = new Vector3(marginRight, 0f,0f);
		moveMarDown = new Vector3(0f, marginDown * -1f,0f);

	}

	public void triggerRotation()
	{
		InvokeRepeating("pivotHead", 0f, 0.05f);
	}

	override public void enableBrick()
	{
		isEnabled = true;
		speed = getSpeed(this, _bricksSpeed);
		triggerRotation();
	}
	
	override public void disableBrick()
	{
		isEnabled = false;
		CancelInvoke("pivotHead");
	}

	public void setupBladePart(int _length = 0, bool _additionMode = false)
	{
//		_additionMode = true ? towerLength = _length : towerLength += _length ; 
		towerLength = (_additionMode) ? _length: towerLength + _length;
		destroyAllBladePart(FbList);

		foreach (LevelTools.DirectionList dirChosen in enabledDirection)
		{
			createBladePart(towerLength, dirChosen);
		}
		FbDirList.Add(FbListLeft);
		FbDirList.Add(FbListDown);
		FbDirList.Add(FbListRight);
		FbDirList.Add(FbListUp);

		moveFireballs(FbDirList);
		createBladeKiller(FbList);
	}

	public void swapRotation(bool _swapRot)
	{
		if (_swapRot == true)
		{
			speed = speed * -1f;
		}
	}

	public void pivotHead()
	{
		Vector3 vec = new Vector3(0f,0f,speed);
		FbHead.transform.Rotate(vec);
		Hat.transform.Rotate(vec);
	}

	public void Update()
	{
//		Vector3 vec = new Vector3(0f,0f,speed);
//		transform.Rotate(vec);
	}

	public void destroyAllBladePart(List<BladePart> _list)
	{
		foreach (BladePart fb in _list)
		{
			Destroy(fb.gameObject);
		}
		FbListDown.Clear();
		FbListUp.Clear();
		FbListRight.Clear();
		FbListLeft.Clear();
		FbDirList.Clear();
		_list.Clear();
	}

	public void createBladePart(int _nbFireballs, LevelTools.DirectionList _dir)
	{
		for (int i = 0; i < _nbFireballs ; i++)
		{
			GameObject gameo = Instantiate(Resources.Load("Bricks/Opponent/BladePart")) as GameObject;
			BladePart fb = gameo.GetComponent<BladePart>();
			fb.Direction = _dir;
			switch (fb.Direction)
			{
			case LevelTools.DirectionList.Down :
			{
				FbListDown.Add(fb);
				fb.transform.parent = parDown.transform;
				break;
			}
			case LevelTools.DirectionList.Up :
			{
				FbListUp.Add(fb);
				fb.transform.parent = parUp.transform;
				break;
			}
			case LevelTools.DirectionList.Right :
			{
				FbListRight.Add(fb);
				fb.transform.parent = parRight.transform;
				break;
			}
			case LevelTools.DirectionList.Left :
			{
				FbListLeft.Add(fb);
				fb.transform.parent = parLeft.transform;
				break;
			}
			}
			fb.transform.localPosition = new Vector3(0f,0f,0f);
//			fb.transform.localPosition = new Vector3(0f,0f,0f);
			FbList.Add(fb);
		}
	}

	private void moveFireballs(List<List<BladePart>> listDir)
	{
		foreach (List<BladePart> _list in listDir)
		{
			foreach (BladePart fb in _list)
			{
				int currInd = _list.IndexOf(fb) + 2;
				switch (fb.Direction)
				{
				case LevelTools.DirectionList.Down :
				{
					fb.transform.localPosition += (moveMarDown * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,180f));
					break;
				}
				case LevelTools.DirectionList.Up :
				{
					fb.transform.localPosition += (moveMarUp * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,0f));
					break;
				}
				case LevelTools.DirectionList.Right :
				{
					fb.transform.localPosition += (moveMarRight * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,270f));
					break;
				}
				case LevelTools.DirectionList.Left :
				{
					fb.transform.localPosition += (moveMarLeft * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,90f));
					break;
				}
				}
			}
		}
	}

	private void createBladeKiller(List<BladePart> _listpt)
	{
		foreach (BladePart pt in _listpt)
		{
			pt.startKiller();
		}
	}
	
	
	private void GameStart()
	{
		if (this != null)
		{
			
		}
	}
	
	private void GameOver()
	{
		if (this != null)
		{
			MasterAudio.StopAllPlaylists();
			CancelInvoke("checkScore");
		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{
			towerLength = 0;
		}
	}

}
