using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject bullet;
	public GameObject view;

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0))
		{
			GameObject ball = GameObject.Instantiate (bullet);
			ball.GetComponent<Transform> ().position = this.transform.position;
			Rigidbody rb = ball.GetComponent<Rigidbody> ();
			Vector3 direction = view.transform.forward;
			rb.velocity = direction * 25;
		}
	}
}
