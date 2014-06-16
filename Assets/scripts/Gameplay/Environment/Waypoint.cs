using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public enum TypeList
	{
		Normal,
		Initial,
		GoToAndStop
	};
	public TypeList WPType;
	[HideInInspector] public int id;
	[HideInInspector] public bool activated = true;
	public Waypoint nextWP;
	[HideInInspector] public WaypointManager linkedManager;
	public bool passedUpon = false;
	public float EDITOR_Resizer;

	public void Setup()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
		linkedManager = transform.parent.GetComponent<WaypointManager>();
		id = int.Parse(name);
	}
	public void lateSetup()
	{
		nextWP = linkedManager.findNextWaypoint(this);
	}

	void OnTriggerEnter(Collider _other)
	{
		if (activated && _other.GetComponent<PatrolBrick>() != null && linkedManager.relatedBrick != null && linkedManager.relatedBrick.Contains(_other.GetComponent<LevelBrick>()) )
		{
			PatrolBrick _collBrick = _other.GetComponent<PatrolBrick>();
			if (_collBrick.type == LevelBrick.typeList.Carpet && _collBrick.GetComponent<Carpet>().Stopped == false)
			{
				_collBrick.GetComponent<Carpet>().ReachAndStop();
			}
			passedUpon = true;
			StartCoroutine("delayRetrigger");
			Waypoint findWp = linkedManager.findNextWaypoint(this);
			_collBrick.GoToWaypoint(findWp);
			if (_collBrick.type == LevelBrick.typeList.Bird)
			{
//				_collBrick.GetComponent<Bird>().rotateTowardWp(findWp.transform.position, _collBrick.transform);
			}
		}
	}

	IEnumerator delayRetrigger()
	{
		yield return new WaitForSeconds(0.5f);
		passedUpon = false;
	}

	public void setupCollider(Vector3 resize)
	{
		BoxCollider _coll = GetComponent<BoxCollider>();
		_coll.size = resize;
	}
}
