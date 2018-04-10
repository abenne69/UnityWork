using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {

	float distance = 0.2f;
	public GameObject player;
	float lowestpoint;
	// Use this for initialization
	void Start () {
		lowestpoint = player.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		
		if (hit.gameObject.tag == "ladder") {
			print ("Ladder!!!!!!!!!!!!!!!!!!!!!");
			if (Input.GetAxis ("Vertical") > 0) {
				float y = player.transform.position.y;
				y += distance;
				player.transform.position = new Vector3 (player.transform.position.x, y, player.transform.position.z);
			} else if (Input.GetAxis ("Vertical") < 0) {
				float y = player.transform.position.y;
				y -= distance;
				player.transform.position = new Vector3 (player.transform.position.x, y, player.transform.position.z);
			}
		}

	}
}
