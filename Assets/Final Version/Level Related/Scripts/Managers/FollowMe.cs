using UnityEngine;
using System.Collections;

public class FollowMe : MonoBehaviour 
{
	public Transform house_transform;
	Transform horse_transform;
    Transform player;

	GameObject the_house;
	Animator anim;
	NavMeshAgent nav;
	bool pressed_F = false;

	// Use this for initialization
	void Start () 
	{
		house_transform = GameObject.FindGameObjectWithTag ("mat").transform;
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		nav = gameObject.GetComponent<NavMeshAgent> ();
		anim = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () 
	{
		if (Vector3.Distance(player.position, transform.position) <= 20.0f)
		{
			if(Input.GetKeyUp(KeyCode.F) || Input.GetKeyDown(KeyCode.F))
			{
				pressed_F = true;
			}
		}

		if (pressed_F) 
		{
			nav.SetDestination(house_transform.position);
			anim.SetBool("Walk",true);
			Debug.Log("Horse is going home");
		}
	}	
}
