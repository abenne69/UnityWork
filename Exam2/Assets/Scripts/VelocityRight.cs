using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityRight : MonoBehaviour {
	//public GameObject tube;
	bool isColliding = false;
	Rigidbody rb;
	GameObject play;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isColliding) {
			 play.transform.position += this.transform.forward * 2;
			//rb.AddForce (this.transform.forward * 30);
		}
	}
	void OnTriggerEnter(Collider hit){
		if(hit.gameObject.tag == "Player"){
			isColliding = true;
			play = hit.gameObject;
			//rb = hit.gameObject.GetComponent<Rigidbody> ();
			/*rb.velocity = this.transform.forward * 30;
			hit.gameObject.transform.position += this.transform.forward * 30;
			print (rb.velocity);*/
		}
	}
	void OnTriggerExit(Collider hit)
	{
		isColliding = false;
		//print ("Yo this shit fuggin works doe");
	}
	/*void OnCollisionEnter (Collision collision)
	{
		if(collision.transform.name == ("Block"));
		{
			blockTransform = collision.transform;
			blockTransform.transform.parent = transform;
			blockTransform.transform.position = holdSlot.transform.position;
		}
	}*/

}
