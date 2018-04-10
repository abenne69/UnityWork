using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateSword : MonoBehaviour {
	//public GameObject sword;
	public GameObject sword;
	Animation anim;
	public AudioClip clip1;
	public AudioClip clip2;
	public AudioClip clip3;

	AudioClip[] audios;

	int index = 0;


	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = sword.GetComponent<AudioSource> ();
		anim = sword.GetComponent<Animation> ();
		audios = new AudioClip[3];
		audios [0] = clip1; audios [1] = clip2; audios [2] = clip3;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2")){
			audio.PlayOneShot(audios[index]);
			anim.Play ("Sword|SwordAction");
			index++;
			if (index == 3)
				index = 0;
		}
	}
}
