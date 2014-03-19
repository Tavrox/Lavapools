﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public enum TypeList
	{
		Normal,
		Initial,
		Switchpoint
	};
	public TypeList WPType;
	[HideInInspector] public int id;
	[HideInInspector] public bool activated = true;
	public Waypoint nextWP;
	[HideInInspector] public WaypointManager linkedManager;
	public bool passedUpon = false;
	public BoxCollider colli;

	public void Setup()
	{
		linkedManager = transform.parent.GetComponent<WaypointManager>();
		id = int.Parse(name);
	}
	public void lateSetup()
	{
		if (linkedManager.type != LevelBrick.typeList.Fields)
		{
			nextWP = linkedManager.findNextWaypoint(this);
		}
	}

	void OnTriggerEnter(Collider _other)
	{
		if (activated && _other.GetComponent<PatrolBrick>() != null && passedUpon == false)
		{
			passedUpon = true;
			StartCoroutine("delayRetrigger");
			PatrolBrick _collBrick = _other.GetComponent<PatrolBrick>();
//			print ("[" + _collBrick.type + " VS " + LevelBrick.typeList.Fields + "]");

			if (_collBrick.type != LevelBrick.typeList.Fields && linkedManager.relatedBrick != null)
			{
//				print ("[" + linkedManager.relatedBrick + " VS " + _collBrick + "]");
//				if (linkedManager.relatedBrick != _collBrick)
//				{
					if (_collBrick.type == linkedManager.relatedBrick.type && _collBrick.brickPathId == linkedManager.id)
					{
						_collBrick.GoToWaypoint(nextWP);
					}
//				}
			}
		}
	}

	IEnumerator delayRetrigger()
	{
		yield return new WaitForSeconds(2f);
		passedUpon = false;
	}

	public void setupCollider(Vector3 resize)
	{
		BoxCollider _coll = GetComponent<BoxCollider>();
		_coll.size = resize;
	}
}
