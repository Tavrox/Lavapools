using UnityEngine;
using System.Collections;

public class FallingRock : MonoBehaviour {

	public float speed;
	public float delayBeforeRespawn;
	public Vector3 initpos;

	// Use this for initialization
	void Start () 
	{

		initpos = transform.position;
		InvokeRepeating("PermanentFall", 0f, delayBeforeRespawn + 3f);
	
	}

	private void PermanentFall()
	{
		StartCoroutine("Fall");
	}
	
	// Update is called once per frame
	void Update () {

		transform.position += new Vector3(Vector3.down.x * speed * Time.deltaTime, Vector3.down.y * speed * Time.deltaTime, 0f);
	}

	IEnumerator Fall()
	{
		yield return new WaitForSeconds(5f);
		transform.position = initpos;
	}
}
