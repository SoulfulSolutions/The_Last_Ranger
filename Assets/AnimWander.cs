using UnityEngine;
using System.Collections;

public class AnimWander : MonoBehaviour {

	public float moveSpeed = 5;
	public float rotSpeed = 10;
	public int moveDir = 1;
	public float  tiempo;
	public float tiempo3;
	public bool  Walk;
	public Animation anim;
	// Rigidbody caca;
	public GameObject GOPos;
	public Vector3 localScale;
	double randon= 0.1;

	AnimWander animwander;
	
	void  Start ()
	{
		tiempo = Random.Range(-1, 400);

		tiempo3 = Random.Range(-1, 400);
	}
	
	public  void  Update (){
		
//		Debug.Log (gameObject.name + " is wondering");
		if (!Physics.Raycast (transform.position, transform.forward, 5)) 
		{
			transform.Translate (Vector3.forward * moveSpeed * Time.smoothDeltaTime);
		} else 
		{
			if (Physics.Raycast (transform.position, - transform.right, 1))
			{
				moveDir = 1;
			} else if (Physics.Raycast (transform.position, transform.right, 1)) 
			{
				moveDir = -1;
			}
			transform.Rotate (Vector3.up, 90 * rotSpeed * Time.smoothDeltaTime * moveDir);
		}
		
		//subracting from the temp variable
		tiempo -= (Time.deltaTime * 5);
		
		tiempo3 -= (Time.deltaTime * 5);
		
		//checks if the value has not reached zero. if so it will do a random number again
		if (tiempo <= 0) {
			tiempo = Random.Range (-1, 400);
		}
		if (tiempo3 <= 0) {
			tiempo3 = Random.Range (-1, 400);
		}
		
		//...............................................................................................................
		if (tiempo > 1 && tiempo < 100) {//walk
			Walk = true;
			moveSpeed = 3;
			if (gameObject.name.Contains ("Horse"))
				anim.CrossFade ("walk");
			if (gameObject.name.Contains ("Wolf_Black"))
				anim.CrossFade ("run");
			
			if (gameObject.name.Contains ("GOBLIN"))
				anim.CrossFade ("walk");// anim.SetBool("Walk", true);
			if (gameObject.name.Contains ("Troll1"))
				anim.CrossFade ("Walk");
			if (gameObject.name.Contains ("cyclop_soldier"))
				anim.CrossFade ("walk");
		}
		if (tiempo > 100 && tiempo < 110) {//rotate left
			Walk = true;
			transform.Rotate (Vector3.up, 90 * rotSpeed * Time.smoothDeltaTime * moveDir);
		
			
			if (gameObject.name.Contains ("Horse"))
				anim.CrossFade ("walk");
			if (gameObject.name.Contains ("Wolf_Black"))
				anim.CrossFade ("run");
			
			if (gameObject.name.Contains ("GOBLIN"))
				anim.CrossFade ("walk");// anim.SetBool("Walk", true);
			if (gameObject.name.Contains ("Troll1"))
				anim.CrossFade ("Walk");
			if (gameObject.name.Contains ("cyclop_soldier"))
				anim.CrossFade ("walk");
		}
		if (tiempo > 110 && tiempo < 190) {//run
			Walk = true;
			moveSpeed = 3;
			if (gameObject.name.Contains ("Horse"))
				anim.CrossFade ("walk");
			if (gameObject.name.Contains ("Wolf_Black"))
				anim.CrossFade ("run");
			
			if (gameObject.name.Contains ("GOBLIN"))
				anim.CrossFade ("run");// anim.SetBool("Walk", true);
			if (gameObject.name.Contains ("Troll1"))
				anim.CrossFade ("Run");
			if (gameObject.name.Contains ("cyclop_soldier"))
				anim.CrossFade ("run");
		}
		if (tiempo > 190 && tiempo < 200) {//rotate right
			Walk = true;
			moveSpeed = 2;
			transform.Rotate (Vector3.up, -90 * rotSpeed * Time.smoothDeltaTime * moveDir);

			if (gameObject.name.Contains ("Horse"))
				anim.CrossFade ("walk");
			if (gameObject.name.Contains ("Wolf_Black"))
				anim.CrossFade ("run");
			
			if (gameObject.name.Contains ("GOBLIN"))
				anim.CrossFade ("walk");// anim.SetBool("Walk", true);
			if (gameObject.name.Contains ("Troll1"))
				anim.CrossFade ("Walk");
			if (gameObject.name.Contains ("cyclop_soldier"))
				anim.CrossFade ("walk");
		}
		if (tiempo > 200 && tiempo < 250) {//idle
			if (tiempo > 300 && tiempo < 500) {
			Walk = false;
			moveSpeed = 0;
				if (gameObject.name.Contains ("Horse"))
					anim.CrossFade ("combat_idle");
				if (gameObject.name.Contains ("Wolf_Black"))
					anim.CrossFade ("barking");
				if (gameObject.name.Contains ("GOBLIN"))
					anim.CrossFade ("idle");
				if (gameObject.name.Contains ("Troll1"))
					anim.CrossFade ("Idle_02");
				if (gameObject.name.Contains ("cyclop_soldier"))
					anim.CrossFade ("idle");
			}
			if (tiempo > 250 && tiempo < 300) {//walk
				Walk = true;
				moveSpeed = 3;
				if (gameObject.name.Contains ("Horse"))
					anim.CrossFade ("walk");
				if (gameObject.name.Contains ("Wolf_Black"))
					anim.CrossFade ("run");
			
				if (gameObject.name.Contains ("GOBLIN"))
					anim.CrossFade ("walk");// anim.SetBool("Walk", true);
				if (gameObject.name.Contains ("Troll1"))
					anim.CrossFade ("Walk");
				if (gameObject.name.Contains ("cyclop_soldier"))
					anim.CrossFade ("walk");
		}
			if (tiempo > 300 && tiempo < 310) {//rot left
				Walk = true;
				transform.Rotate (Vector3.up, 90 * rotSpeed * Time.smoothDeltaTime * moveDir);

				if (gameObject.name.Contains ("Horse"))
					anim.CrossFade ("walk");
				if (gameObject.name.Contains ("Wolf_Black"))
					anim.CrossFade ("run");
				
				if (gameObject.name.Contains ("GOBLIN"))
					anim.CrossFade ("walk");// anim.SetBool("Walk", true);
				if (gameObject.name.Contains ("Troll1"))
					anim.CrossFade ("Walk");
				if (gameObject.name.Contains ("cyclop_soldier"))
					anim.CrossFade ("walk");
		
		}
			if (tiempo > 310 && tiempo < 350) {//run
				Walk = true;
				moveSpeed = 7;
				if (gameObject.name.Contains ("Horse"))
					anim.CrossFade ("walk");
				if (gameObject.name.Contains ("Wolf_Black"))
					anim.CrossFade ("run");
				
				if (gameObject.name.Contains ("GOBLIN"))
					anim.CrossFade ("run");// anim.SetBool("Walk", true);
				if (gameObject.name.Contains ("Troll1"))
					anim.CrossFade ("Run");
				if (gameObject.name.Contains ("cyclop_soldier"))
					anim.CrossFade ("run");
			
		}
			if (tiempo > 350 && tiempo < 400) {//idle
				if (tiempo > 300 && tiempo < 500) {
					Walk = false;
					moveSpeed = 0;
					if (gameObject.name.Contains ("Horse"))
						anim.CrossFade ("combat_idle");
					if (gameObject.name.Contains ("Wolf_Black"))
						anim.CrossFade ("barking");
					if (gameObject.name.Contains ("GOBLIN"))
						anim.CrossFade ("idle");
					if (gameObject.name.Contains ("Troll1"))
						anim.CrossFade ("Idle_01");
					if (gameObject.name.Contains ("cyclop_soldier"))
						anim.CrossFade ("idle");
				}
		}
			//...........................................................................................



		if (tiempo3 >= 500 && tiempo3 <= 501 && Walk == false) {
				cagar ();
			} else
		if (tiempo3 >= 450 && tiempo3 <= 451 && Walk == false) {
				cagar ();
			} else
		if (tiempo3 >= 400 && tiempo3 <= 400 && Walk == false) {
				cagar ();
		}
		
		
		}
	}
	
	void  cagar ()
	{
		//Instantiate(caca, GOPos.transform.position, GOPos.transform.rotation);
		//transform.localScale += Vector3(1,1,1);
	}
}