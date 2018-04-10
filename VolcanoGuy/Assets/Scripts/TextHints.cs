using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHints : MonoBehaviour {

	public Text gui;
	float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(gui.enabled){
			timer += Time.deltaTime;
			if(timer >=4){
				gui.enabled = false;
				timer = 0.0f;
			} 
		}
	}
	void ShowHint(string message){
		gui.text = message;
		if (!gui.enabled) {
			gui.enabled = true;
		}
	}


}
