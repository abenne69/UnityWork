using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutLauncher : MonoBehaviour {
	public AudioClip throwSound;
	AudioSource audio;

	public Rigidbody coconutPrefab;
	public float throwSpeed = 30.0f;

	public static bool canThrow = false;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonUp("Fire1") && canThrow ){
			audio.PlayOneShot(throwSound);
			Rigidbody newCoconut = Instantiate(coconutPrefab,
				transform.position, transform.rotation) as Rigidbody;
			newCoconut.name = "coconut";
			newCoconut.velocity = transform.forward * throwSpeed;
			Physics.IgnoreCollision(transform.root.GetComponent<Collider>(),
				newCoconut.GetComponent<Collider>(), true);
		}
	}
}
