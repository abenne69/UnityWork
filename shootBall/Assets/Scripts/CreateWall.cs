using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWall : MonoBehaviour {
	
	GameObject prefab;
	// Use this for initialization
	void Start () {
		prefab = Resources.Load("CubeWall") as GameObject;
		float cubeWidth = prefab.transform.localScale.x;
		float cubeHeight = prefab.transform.localScale.y; 
		//reference for the next instances
		float referenceX = -2.938817f;
		float referenceY = -2.109755f;
		float referenceZ = 3.623962e-05f;

		for (int i = 0; i <= 5; i++) {
			for (int j = 0; j <= 6; j++) {
				Vector3 position_ = new Vector3 (referenceX +
				                    j * cubeWidth, referenceY + i * cubeHeight, 0);
				// create the cube first...
				GameObject cube = Instantiate (prefab) as GameObject;
				// and then set its position:
				cube.transform.position = position_;
				//show their names
				cube.name = "" + i + "-" + j;
			} 
		}
	}
	
}
