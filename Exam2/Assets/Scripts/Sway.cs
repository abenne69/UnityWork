using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour {

	float timeToMove = 0.001f;
	float direction = 0.20f;
	int directionCounter = 0;
	
	// Update is called once per frame
	void Update () {
		timeToMove -= Time.deltaTime;
		if (timeToMove <= 0) {
			timeToMove = 0.01f;
			if (directionCounter >= 50) {
				direction *= -1;
				directionCounter = 0;
			}
			move ();
		}

	}
	void move()
	{
		float z = this.transform.position.z;
		z += direction;
		directionCounter++;
		this.transform.position = new Vector3 (this.transform.position.x,this.transform.position.y,z);
	}
}
