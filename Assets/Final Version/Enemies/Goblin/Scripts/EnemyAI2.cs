using UnityEngine;
using System.Collections;

public class EnemyAI2 : MonoBehaviour {

public float Distance;
public Transform target;
public float lookAtDistance = 25.0f;
public float attackRange = 15.0f;
public float moveSpeed = 5.0f;
public float Damping = 6.0f;

public void Update ()
{
    // Enemy is behaving through this but does not enable collision and gravity
    Distance = Vector3.Distance(target.position, transform.position);
	
	if (Distance < lookAtDistance)
	{
		lookAt();
	}
	
	
	if (Distance < attackRange)
	{
		attack ();
	}
}

public void lookAt ()
{
    var rotation = Quaternion.LookRotation(target.position - transform.position);
	transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping); // smoothen rotation of enemy at any frame you are running at
}

 // enemy must move forward by the set movespeed and not be affected by frame rate
public void attack ()
{
	transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
}
}
