using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour {

	public float rotationSpeed = 100.0f;
	public GameObject player;
	public ParticleSystem exp;

	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(0,rotationSpeed * Time.deltaTime,0));
	}
	void OnTriggerEnter(Collider col)
	{
		print ("hit");
		if (col.gameObject.tag == "Player") {
			col.gameObject.SendMessage ("MonkeyPickup");
			Destroy (gameObject);
		} else {

			player.SendMessage ("MonkeyPickup");
			ParticleSystem explosion = Instantiate(exp,transform.position,new Quaternion());
			Destroy(gameObject);
			Destroy(explosion,2);
		}
	}
}
