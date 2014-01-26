﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Waypoint : MonoBehaviour {

	public enum wpTypeList
	{
		DeadEnd,
		Crossroad,
		Loop
	}
	public wpTypeList wpType;
	public Waypoint nextWP;
}
