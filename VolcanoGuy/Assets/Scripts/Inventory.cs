using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	public static int charge = 0;
	public AudioClip collectSound;

	public Texture2D[] hudCharge;
	public RawImage chargeHudGUI;

	public RawImage matchGUI;

	bool haveMatches = false;

	// Generator
	public Texture2D[] meterCharge;
	public Renderer meter;

	public Text textHints;

	bool fireIsLit = false;

	// Use this for initialization
	void Start () {
		charge = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CellPickup(){
		HUDon();
		AudioSource.PlayClipAtPoint(collectSound, transform.position);
		charge++;
		meter.material.mainTexture = meterCharge[charge];
		chargeHudGUI.texture = hudCharge[charge];
	}
	void HUDon(){
		if(!chargeHudGUI.enabled){
			chargeHudGUI.enabled = true;
		}
	}
	void MatchPickup(){
		chargeHudGUI.enabled = false;
		haveMatches = true;
		AudioSource.PlayClipAtPoint(collectSound, transform.position);
		matchGUI.enabled = true;
	}
	void OnControllerColliderHit(ControllerColliderHit col){
		if (col.gameObject.name == "campfire" && !fireIsLit) {
			LightFire (col.gameObject);
		} else if(col.gameObject.name == "campfire" && !haveMatches && !fireIsLit){
				textHints.SendMessage("ShowHint", "I could use this campfire to signal for help.. \nif only I could light it..");
					
		}
	}
	void LightFire(GameObject campfire){
		ParticleSystem[] fireEmitters;
		fireEmitters = campfire.GetComponentsInChildren<ParticleSystem>();
		foreach (ParticleSystem emitter in fireEmitters) {
			emitter.Play ();	
		}
		campfire.GetComponent<AudioSource> ().enabled = true;
		Destroy(matchGUI);
		haveMatches=false;
		fireIsLit=true;
	}

}
