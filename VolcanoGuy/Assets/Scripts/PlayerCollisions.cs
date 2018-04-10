using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollisions : MonoBehaviour {
	GameObject currentDoor;
	public AudioClip lockedSound;
	AudioSource audio;
	public Light doorLight;
	public Text textHints;

	void Start () {
		audio = GetComponent<AudioSource>();
	}

	void Update(){
	
		RaycastHit hit;
		if(Physics.Raycast (transform.position, transform.forward,
			out hit, 3)) {
			if(hit.collider.gameObject.tag=="playerDoor"){
				currentDoor = hit.collider.gameObject;
				if (Inventory.charge == 4) {
					currentDoor.SendMessage ("DoorCheck");
					if(GameObject.Find("PowerGUI")){
						Destroy(GameObject.Find("PowerGUI"));
						doorLight.color = Color.green;
					}
				} else {
					textHints.SendMessage("ShowHint", "This door won't budge.. \nguess it needs fully charging - maybe more power cells will help...");
					audio.PlayOneShot(lockedSound);
					currentDoor.gameObject.SendMessage("HUDon");
				}
			}
		}


	}

}