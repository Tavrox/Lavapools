using UnityEngine;
using System.Collections;

public class Trailer : MonoBehaviour {

	public float z1 = 2.36f;
	public float z2 = 0.69f;
	public float z3 = -0.12f;
	public float z4 = -0.769f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKey(KeyCode.Keypad1))
		{
			triggZoom(z1);
		}
		if (Input.GetKey(KeyCode.Keypad2))
		{
			triggZoom(z2);
		}
		if (Input.GetKey(KeyCode.Keypad3))
		{
			triggZoom(z3);
		}
		if (Input.GetKey(KeyCode.Keypad4))
		{
			triggZoom(z4);
		}
		if (Input.GetKey(KeyCode.Keypad0))
		{
			triggZoom(0f);
		}
		if (Input.GetKey(KeyCode.Keypad5))
		{
			triggZoom(z1);
		}
	}

	private void triggZoom(float newval)
	{
		OTView view = GameObject.Find("Frameworks/OT/View").GetComponent<OTView>();
		new OTTween(view, 0.75f, OTEasing.QuartOut).Tween("zoom", newval).Wait(3f);
	}


}
