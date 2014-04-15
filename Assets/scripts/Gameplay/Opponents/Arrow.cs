using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

	private Vector3 target;
	public float speed;
	private LevelTools.DirectionList arrDirection;
	private float Angle;
	private float Padding = 1f;

	void OnTriggerEnter(Collider _oth)
	{
		if (_oth.CompareTag("Player"))
		{
			GameEventManager.TriggerGameOver(LevelTools.KillerList.Arrow);
		}
	}

	public void giveDirection(LevelTools.DirectionList _dir)
	{
		arrDirection = _dir;
		switch (arrDirection)
		{
			case LevelTools.DirectionList.Up :
			{
			target = new Vector3(0f,1f,0f);
			transform.Rotate(new Vector3(0f,0f,90f));
			transform.position += new Vector3(0f, Padding, 0f);
			break;
			}
			case LevelTools.DirectionList.Down :
			{
			target = new Vector3(0f,-1f,0f);
			transform.Rotate(new Vector3(0f,0f,270f));
			transform.position += new Vector3(0f, (Padding * -1f), 0f);
			break;
			}
			case LevelTools.DirectionList.Left :
			{
			target = new Vector3(-1f,0f,0f);
			transform.Rotate(new Vector3(0f,0f,180f));
			transform.position += new Vector3((Padding * -1f), 0f, 0f);
			break;
			}
			case LevelTools.DirectionList.Right :
			{
			target = new Vector3(1f,0f,0f);
			transform.Rotate(new Vector3(0f,0f,0f));
			transform.position += new Vector3(Padding, 0f, 0f);
			break;
			}
		}
		InvokeRepeating("UpdateMovement", 0f, 0.01f);
	}

	public void cancelMovement()
	{
		CancelInvoke("UpdateMovement");
	}
	
	public void UpdateMovement()
	{
		transform.position += speed * target * Time.deltaTime;
	}
}
