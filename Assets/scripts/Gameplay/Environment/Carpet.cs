using UnityEngine;
using System.Collections;

public class Carpet : PatrolBrick {

	private float DistToWp;
	public bool Stopped = false;

	public void Setup () 
	{
		base.Setup();
		type = typeList.Carpet;
		
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		GameEventManager.Respawn += Respawn;

		if (brickPath != null)
		{
			brickPath.relatedBrick.Add(this);
		}
		else
		{
			Debug.Log("The path of "+gameObject.name+" is missing.");
		}
		InvokeRepeating("checkWpDistance", 0f, 0.05f);
		setupPath();
	}

	private void checkWpDistance()
	{
//		DistToWp = Vector3.Distance(gameObject.transform.position , brickPath.findNextWaypoint(currentWP).transform.position);
//		print (DistToWp);
		if (Stopped == false)
		{
//			if (DistToWp < 1f)
//			{
//				print ("stop");
//	//			StartCoroutine("StopAndGo");
//			}
//			else if (DistToWp < 4f)
//			{
//				print ("slowin");
//				speed = initSpeed / 1.5f;
//			}
//			else
//			{
//				print ("normal");
//				speed = initSpeed;
//			}
		}
	}

	public void ReachAndStop()
	{
		Stopped = true;
		StartCoroutine(StopAndGo());
	}

	IEnumerator StopAndGo()
	{
		speed = 0f;
		yield return new WaitForSeconds (2f);
		Stopped = false;
		speed = initSpeed;
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

		}
	}
	
	private void Respawn()
	{
		if (this != null)
		{

		}
	}
}
