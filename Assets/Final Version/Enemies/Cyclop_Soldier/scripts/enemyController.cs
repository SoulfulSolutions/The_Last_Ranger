using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class enemyController : MonoBehaviour 
{


  public Transform Target; // a variable which will store target (player) info
  public CharacterController charcontroller; // a variable which will store cyclop (enemy)  info

private float attackTime; //variable which will store when an enemy should play again attack animation again 

private Animation anim; // variable which will store all animation
private AudioSource sourc; // variable which will store all audio source
private int cychealth = 100; // cyclop health
	private PlayerHealth player;
  
    public void Start ()
    {
        
	    attackTime = Time.time;
        anim = GetComponent<Animation>(); //getting all animation components
        sourc = GetComponent<AudioSource>();
		player = GameObject.Find("Player").GetComponent<PlayerHealth>();
        sourc.Stop();
    }


public void Update ()
{
	if(cychealth >=0)
	{

        float Distance = Vector3.Distance(Target.position, transform.position); // assigning a distance of player

	    float lookAtDistance = 25.0f; // a variable which store the value of of when the cyclop should follow player
        float chaseRange = 15.0f;
        float attackRange = 1.5f;

		if (Distance < lookAtDistance) // check if player is close  
		{
			lookAt(); // calling a method which changes direction of cyclop
		}
			
		if (Distance < attackRange)
		{
			attack(); // calling method which perform attack
		}
		else if (Distance < chaseRange)
		{
			chase (); // calling a method which chase player 
		}  
	}

    
}

public void lookAt ()
{
    // ally your animals must use this method
	var rotation = Quaternion.LookRotation(Target.position - transform.position);
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 6.0f); // 6.0 dumping
}

public void chase ()
{
//	renderer.material.color = Color.red;
    Vector3 moveDirection = Vector3.zero; //initialising move direction to default value
    anim.CrossFade("run"); //playing run animination
    moveDirection = transform.forward; //  moving forward
    moveDirection *= 5.0f; // move speed
	moveDirection.y -= 20.0f * Time.deltaTime; // moving in the direction of Y.. 20.0f is gravity
    charcontroller.Move(moveDirection * Time.deltaTime);
}

public void attack ()
{
	if (Time.time > attackTime) 
	{
			player.HandleHealth(7f);
	    anim.CrossFade("attack_1"); // play attack animination
		attackTime = Time.time + 1f; // repeat attack after 1 second
	}
}



void OnTriggerEnter(Collider c)
{

    if (c.tag == "bullet") // checking if player has shot cyclop 
    {
        cychealth -= 10; //decreasing cyclop health by 10 when get shot 

        sourc.Play();
        if (cychealth == 0)
        { 
            anim.CrossFade("death"); //playing dying animination
            sourc.Stop(); // stop playing pain sound
            Destroy(GameObject.Find("cyclop_soldier"),3f); //making cyclop disappear after 3 seconds 
        }
    }
}

  
}
