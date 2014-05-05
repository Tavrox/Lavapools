using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {
	
	private LevelTools.DirectionList arrDirection;
	private float Angle;
	private float Padding = 0f;
	private Vector3 target;
	public ArrowTower linkedTower;
	public float speed;
	public bool Busy = false;
	public float delayBeforeExtinction = 10f;
	private Vector3 initPos;

	public void Setup(ArrowTower _tw, float _sp)
	{
		linkedTower = _tw;
		speed = _sp;
		initPos = transform.position;
	}

	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player") == true)
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.Arrow);
		}
	}

	void Update()
	{
		if (Busy == true)
		{
			transform.position += new Vector3 ((speed * target.x) * Time.deltaTime, (speed * target.y) *  Time.deltaTime, 0f) ;
		}
		else
		{
			transform.localPosition = Vector3.zero;
		}
	}

	public void giveDirection(LevelTools.DirectionList _dir)
	{
		transform.position = linkedTower.transform.position;
		transform.localPosition = new Vector3(0f,0f,0f);
		arrDirection = _dir;
		Busy = true;
		switch (arrDirection)
		{
			case LevelTools.DirectionList.Up :
			{
			target = Vector3.up;
			transform.rotation = Quaternion.Euler( new Vector3(0f,0f,270f));
//			transform.position += new Vector3(0f, Padding, 0f);
			break;
			}
			case LevelTools.DirectionList.Down :
			{
			target = Vector3.down;
			transform.rotation = Quaternion.Euler( new Vector3(0f,0f,90f));
			break;
			}
			case LevelTools.DirectionList.Left :
			{
			target = Vector3.left;
			transform.rotation = Quaternion.Euler( new Vector3(0f,0f,180f));
			break;
			}
			case LevelTools.DirectionList.Right :
			{
			target = Vector3.right;
			transform.rotation = Quaternion.Euler( new Vector3(0f,0f,0f));
			break;
			}
		}
//		StartCoroutine("FadeAway");
	}

	public void Reset()
	{
		transform.position = initPos;
		Busy = false;
	}

	IEnumerator FadeAway()
	{
		yield return new WaitForSeconds(delayBeforeExtinction);
		if (Busy != false)
		{
			Busy = false;
		}
	}
}
