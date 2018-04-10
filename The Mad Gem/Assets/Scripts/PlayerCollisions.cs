using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{

    bool canClimb;
    public bool gun = false;
    float distance = 0.2f;
    public GameObject player;
    public AudioClip splash;
    public AudioClip swim;
	public AudioClip gunshot;
    private bool goingIn= true;
    private bool goingOut = false;
    public int health = 20;
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.y < 15)
        {
            var fps = player.GetComponent<FirstPersonController>();
            fps.m_GravityMultiplier = .5f;
            if (Input.GetButtonDown("swim"))
            {
                fps.m_StickToGroundForce = 0f;
                fps.m_MoveDir.y = 4f;
                player.GetComponent<AudioSource>().PlayOneShot(swim);
            }
        }
        else
        {
            var fps = player.GetComponent<FirstPersonController>();
            fps.m_GravityMultiplier = 2f;
            fps.m_StickToGroundForce = 10f;
        }
        if (player.transform.position.y < 15 && player.transform.position.y > 14.8 && goingIn)
        {
            player.GetComponent<AudioSource>().PlayOneShot(splash);
            goingIn = false;
            goingOut = true;
        }
        if (player.transform.position.y < 15 && player.transform.position.y > 14.8 && goingOut)
        {
            player.GetComponent<AudioSource>().PlayOneShot(splash);
            goingIn = true;
            goingOut = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
            {
                if (hit.collider.gameObject.tag == "NPC"||hit.collider.gameObject.tag == "chitulu")
                {
                    hit.collider.SendMessage("recieveDamage");
                    print("hit him");
						
                }
            }
        }
        if (gun)
        {
            if (Input.GetMouseButtonDown(0))
            {
				player.GetComponent<AudioSource> ().PlayOneShot (gunshot);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 800))
                {
                    if (hit.collider.gameObject.tag == "NPC" || hit.collider.gameObject.tag == "chitulu")
                    {
                        Destroy(hit.collider.gameObject);
						print ("good kill");
                    }
                }
            }
        }
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "ladder")
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                float y = player.transform.position.y;
                y += distance;
                player.transform.position = new Vector3(player.transform.position.x, y, player.transform.position.z);
            }

        }
    }
    void recieveDamage()
    {
        health--;
        if(health == 0)
        {
            SceneManager.LoadScene("Island");
        }
    }
    void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.tag == "level")
        {
            SceneManager.LoadScene("IslandReturn");
        }
    }    
}
