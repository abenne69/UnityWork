using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	
	// Update is called once per frame
	void Update()
	{
		var x = Input.GetAxis("Horizontal") * Time.deltaTime * 550.0f;
		var z = Input.GetAxis("Vertical") * Time.deltaTime * 50.0f;

		transform.Rotate(0, x, 0);
		transform.Translate(0, 0, z);
	}

}
