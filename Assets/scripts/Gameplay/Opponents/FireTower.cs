using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireTower : OppTower {

	public List<Fireball> FbList = new List<Fireball>();
	public List<List<Fireball>> FbDirList = new List<List<Fireball>>();
	public List<Fireball> FbListUp = new List<Fireball>();
	public List<Fireball> FbListDown = new List<Fireball>();
	public List<Fireball> FbListRight = new List<Fireball>();
	public List<Fireball> FbListLeft = new List<Fireball>();

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


	// Use this for initialization
	public void Setup () 
	{
		base.Setup();
		type = typeList.FireTower;

		FbHead = FETool.findWithinChildren(gameObject, "Head");
		parUp = FETool.findWithinChildren(gameObject, "Head/Fireballs/up");
		parLeft = FETool.findWithinChildren(gameObject, "Head/Fireballs/left");
		parRight = FETool.findWithinChildren(gameObject, "Head/Fireballs/right");
		parDown = FETool.findWithinChildren(gameObject, "Head/Fireballs/down");
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
		triggerRotation();
	}
	
	override public void disableBrick()
	{
		isEnabled = false;
		CancelInvoke("pivotHead");
	}

	public void stepChanger(int _length, string _dir, bool _swapRot)
	{
		destroyAllFireBalls(FbList);
		List<LevelTools.DirectionList> dirList = setupDirectionList(_dir);
		foreach (LevelTools.DirectionList dirChosen in dirList)
		{
			createFireBalls(_length, dirChosen);
		}
		FbDirList.Add(FbListLeft);
		FbDirList.Add(FbListDown);
		FbDirList.Add(FbListRight);
		FbDirList.Add(FbListUp);
		if (_swapRot == true)
		{
			speed = speed * -1f;
		}
		moveFireballs(FbDirList);
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

	public void destroyAllFireBalls(List<Fireball> _list)
	{
		foreach (Fireball fb in _list)
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

	public void createFireBalls(int _nbFireballs, LevelTools.DirectionList _dir)
	{
		for (int i = 0; i < _nbFireballs ; i++)
		{
			GameObject gameo = Instantiate(Resources.Load("Bricks/Opponent/Fireball")) as GameObject;
			Fireball fb = gameo.GetComponent<Fireball>();
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

	private void moveFireballs(List<List<Fireball>> listDir)
	{
		foreach (List<Fireball> _list in listDir)
		{
			foreach (Fireball fb in _list)
			{
				int currInd = _list.IndexOf(fb);
				switch (fb.Direction)
				{
				case LevelTools.DirectionList.Down :
				{
					fb.transform.localPosition = (moveMarDown * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,180f));
					break;
				}
				case LevelTools.DirectionList.Up :
				{
					fb.transform.localPosition = (moveMarUp * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,0f));
					break;
				}
				case LevelTools.DirectionList.Right :
				{
					fb.transform.localPosition = (moveMarRight * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,270f));
					break;
				}
				case LevelTools.DirectionList.Left :
				{
					fb.transform.localPosition = (moveMarLeft * currInd ) ;
					fb.transform.localRotation = Quaternion.Euler( new Vector3(0f,0f,90f));
					break;
				}
				}
			}
		}
	}

}
