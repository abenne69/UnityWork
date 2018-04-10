using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (AudioSource))]

public class TargetCollision : MonoBehaviour {
	bool beenHit = false;
	Animation targetRoot;
	AudioSource audio;
	public AudioClip hitSound;
	public AudioClip resetSound;
	public float resetTime = 3.0f;

	// Use this for initialization
	void Start () {
		targetRoot = transform.parent.transform.parent.GetComponent<Animation>();
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision theObject) {
		if (beenHit == false && theObject.gameObject.name == "coconut") {
			StartCoroutine("targetHit");
		}
	}
	IEnumerator targetHit(){
		audio.PlayOneShot(hitSound);
		targetRoot.Play("down");
		beenHit=true;
		CoconutWin.targets++;
		yield return new WaitForSeconds(resetTime);
		audio.PlayOneShot(resetSound);
		targetRoot.Play("up");
		beenHit=false;
		CoconutWin.targets--;
	}
}
