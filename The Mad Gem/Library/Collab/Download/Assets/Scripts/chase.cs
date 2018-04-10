using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour {

	public Transform player;
	Animation anim;
    public GameObject p;
	string attack;
	int index = 0;
    public int health;
    float hit;
    public ParticleSystem blood;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		if (gameObject.tag == "NPC") {
			attack = "Lumbering";
		} else {
			attack = "attack_2";
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = player.position - this.transform.position;
		float angle = Vector3.Angle (direction, this.transform.forward);

		if (Vector3.Distance (player.position, this.transform.position) < 20 && angle <200) {
			
			direction.y = 0;

			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
			anim.CrossFade ("idle");

			if (direction.magnitude > 10) {
				this.transform.Translate (0, 0, 0.3f);
				anim.CrossFade ("walk");
			} else {
                anim.CrossFade (attack);
                if(hit > 6)
                {
                    p.SendMessage("recieveDamage");
                }
                else
                {
                    hit += Time.deltaTime;
                }
				index++;
				if (index == 2) {
					index = 0;
				}
			}



		} else {
			anim.CrossFade ("idle");
		}
	}
    void recieveDamage()
    {
        health--;
        print("damage");
        if(health == 5)
        {
            anim.CrossFade("stunned_idle");
        }
        if (health ==0)
        {
            Instantiate(blood, transform.position, new Quaternion());
            Destroy(gameObject);
        }
    }
}
