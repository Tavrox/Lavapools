using UnityEngine;
using System.Collections;

public class VerticalLevelSetup : ScriptableObject 
{
	[Range (1f,10f)] public float sliderSpeed;
	public bool ScrollerHelper = false;
	public bool ScrollerStopper = false;


}
