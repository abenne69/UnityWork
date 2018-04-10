using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowTrigger : MonoBehaviour {
	public RawImage crosshair;
	public Text textHints;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider col){
		if(col.gameObject.tag == "Player"){
			if(!CoconutWin.haveWon){
				textHints.SendMessage("ShowHint", "\n There's a power cell attached to this game, \nmaybe I'll win it if I can knock down all the targets...");
			}
			CoconutLauncher.canThrow=true;
			crosshair.enabled=true;
		}
	}
	void OnTriggerExit(Collider col){
		if(col.gameObject.tag == "Player"){
			CoconutLauncher.canThrow=false;
			crosshair.enabled=false;
		}
	}
}
