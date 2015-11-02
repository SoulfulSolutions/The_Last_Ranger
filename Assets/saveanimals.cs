using UnityEngine;
using System.Collections;

public class saveanimals : MonoBehaviour {


	private Animation anim;
	private Vector3 _moveDirection = Vector3.zero;
	public CharacterController Charcontroller;
	// Use this for initialization
	public Transform player;
	private float distance;
	void Start () 
	{
		anim = gameObject. GetComponent<Animation>();
	}

	private bool follow = false;
	// Update is called once per frame
	void Update () 
	{
			if (Vector3.Distance (player.position, transform.position) <= 25.0f) {
			distance = Vector3.Distance (player.position, transform.position); //checks the distance between enemy and the player
			
			if (distance < 20.0f) 
			{
				if (Input.GetKey(KeyCode.F) || follow == true)
				{
					LookAt (player);
					follow = true;
					Chase ();
					if (distance < 3f)
					{
						follow = false;
					}
					
				}
			}


		}
	}

	public void Chase ()
	{

			anim.CrossFade ("Walk");

			_moveDirection = transform.forward;
			_moveDirection *= 7;
			
			_moveDirection.y -= 20.0f * Time.deltaTime;
			Charcontroller.Move (_moveDirection * Time.deltaTime);


	}
	public void LookAt (Transform tag)
	{
		var rotation = Quaternion.LookRotation (tag.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * 6.0f);
	}


}
