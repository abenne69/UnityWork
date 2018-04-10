using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour {

	public Transform player;
	Animation anim;
    public GameObject p;
    public int health;
    float hit;
    public ParticleSystem blood;

	string attack;
	string idle;
	string walk;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
		if (gameObject.tag == "NPC") {
			attack = "Lumbering";
			idle = "Idle";
			walk = "Walk";
		} else {
			attack = "attack_2";
			idle = "idle";
			walk = "walk";
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
