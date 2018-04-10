using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMount : MonoBehaviour {
	bool canMount = false;
	public GameObject player;
	public GameObject parentObj = null;


	bool isMounted = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (canMount && Input.GetButtonUp("Fire3")) {
			print("MountING");
			isMounted = true;
			Mount (parentObj);
		} else if(!canMount && isMounted && Input.GetButtonUp("Fire3")) {
			isMounted = false;
			UnMount (parentObj);
		}
	}


	//Invoked when a button is pressed.
	/*public void SetParent(GameObject newParent)
	{
		//Makes the GameObject "newParent" the parent of the GameObject "player".
		player.transform.parent = newParent.transform;

		//Display the parent's name in the console.
		Debug.Log("Player's Parent: " + player.transform.parent.name);

		// Check if the new parent has a parent GameObject.
		if (newParent.transform.parent != null)
		{
			//Display the name of the grand parent of the player.
			Debug.Log("Player's Grand parent: " + player.transform.parent.parent.name);
		}
	}

	public void DetachFromParent()
	{
		// Detaches the transform from its parent.
		transform.parent = null;
	}*/
	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Horse"){
			canMount = true;
			print("MOUNTABLE");
			parentObj = hit.gameObject;
		}
	}
	void OnTriggerExit(Collider hit)
	{
		if (hit.gameObject.tag == "Horse") {
			canMount = false;
			parentObj = null;
		}
	}
	void Mount(GameObject hit){
		canMount = false;
		transform.position = (hit.transform.position)+(hit.transform.up)+hit.transform.up+hit.transform.up;
		var relativePos = hit.transform.position - transform.position; 
		var rotation = Quaternion.LookRotation(relativePos); 
		transform.rotation = rotation;
		transform.parent = hit.transform;
		this.GetComponent<Rigidbody> ().useGravity = false;
		this.GetComponent<Rigidbody> ().isKinematic = true;

		print("Mounted!");
		hit.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
		this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;



	}
	void UnMount(GameObject hit){
		canMount = false;
		transform.position += Vector3.one;
		transform.parent = null;
		this.GetComponent<Rigidbody> ().useGravity = true;
		this.GetComponent<Rigidbody> ().isKinematic = true;
		hit.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = false;
		this.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController> ().enabled = true;
	
	}
}
