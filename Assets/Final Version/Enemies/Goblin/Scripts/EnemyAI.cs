using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

public class EnemyAI : MonoBehaviour 
{
	private float _distance;
	public float goblinHealth = 100f;
	public float trollHealth = 200f;
	public float cyclopHealth = 150f;
	
	public float LookAtDistance = 25.0f;
	public float ChaseRange = 15.0f;
	public float AttackRange = 2.0f;
	public float MoveSpeed = 5.0f;
	public float Damping = 6.0f;
	public float AttackRepeatTime = 2f;
	public float AttackDamage;
	public CharacterController Charcontroller;
	public float Gravity = 20.0f;
	
	public static int cyclPlus = 3;
	public static int trollPlus = 5;
	public static int goblinPlus = 1;
	
	private float _attackTime;
	private Vector3 _moveDirection = Vector3.zero;
	private Animation _anim;
	private PlayerHealth _playerHealth;
	private AnimalHealth _animalHealth;
	
	//enemy targets
	 public  List<GameObject> Targets; //all the animals
	private Transform Target;	  //one target an enemy must go to out of all the animals
	private Transform Player;	  //enemy target
	
	private AudioSource _goblinSource;
	private AudioSource _cyclopsound;
	private AudioSource _trollsound;
	
	public static int _goblinCount = 3;
	public static int _cyclopsCount = 4;
	public static int _trollCount = 5;
	
	AnimWander animwander;
	wave1stat w;

	public bool isAttacking=false;
	public void Start ()
	{
		w = GameObject.Find("Wave Stats").GetComponent<wave1stat> ();
		animwander=gameObject.GetComponent<AnimWander>();
		_goblinSource = GetComponent<AudioSource>();
		_trollsound = GetComponent<AudioSource>();
		_cyclopsound = GetComponent<AudioSource>();
		
		_playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

		//	  	 Target = GameObject.Find("Player").transform;d

		Target = null;
		
		Targets = GameObject.FindGameObjectsWithTag("Animal").ToList();
		_anim = gameObject. GetComponent<Animation>();
		Player = GameObject.Find ("Player").transform;
	}
	RaycastHit myHit;
	public void Update ()
	{


		gameObject.GetComponent<Rigidbody>().AddForce(transform.up * -1);
		

		for (int x=0; x<Targets.Count; x++) { //it will run trought each animal in the array

			if(Targets[x].activeInHierarchy==false)
			{
				Targets[x].tag = "Untagged";

			
				Targets.RemoveAt(x);
				Target=null;
			}

		}
	
		
		health ();

		//checks if the enemy is closer to the player... otherwise it must wander around or chase animals
		if (Vector3.Distance (Player.position, transform.position) <= LookAtDistance)
		{
			_distance = Vector3.Distance (Player.position, transform.position); //checks the distance between enemy and the player
			
			if (_distance < LookAtDistance) 
			{
				LookAt (Player);
			}
			
			if (_distance < ChaseRange && _distance > AttackRange) 
			{
				Chase ();
			}
			
			if (Vector3.Distance (Player.position, transform.position) < AttackRange && _distance != 0.0f) 
			{	
				Target=Player;
				Attack ();
			
			} 

			
		} 
		else if(Vector3.Distance (Player.position, transform.position) >LookAtDistance)//this is if the player is not close to the enemy
		{
			for (int x=0; x<Targets.Count; x++) //it will run trought each animal in the array
			{
					
				
				if(Targets[x].activeInHierarchy==true)//checks if the target is active
				if (Vector3.Distance (Targets [x].transform.position, transform.position) <= LookAtDistance) //checks the distance of which is the closest animal out of all
				{
					_distance = Vector3.Distance (Targets [x].transform.position, transform.position);//distance of that closest animal

					Target = Targets [x].transform;
				

					if (_distance < LookAtDistance) //if that distance is less than the look at distance then look at the animal
					{
						animwander.enabled = false; //de activate the script for animal wandering
						LookAt (Target);//look at the target
					}
				}
			}
			
			if (_distance < ChaseRange && _distance > AttackRange) 
			{
				animwander.enabled = false;
				Chase ();
			} 
			else if
				(_distance < AttackRange && _distance != 0.0f) 
			{
				if(Target!=null && Vector3.Distance (Target.position, transform.position)<AttackRange)
				{
				_animalHealth=Target.GetComponent<AnimalHealth>();

				animwander.enabled = false;
				if(_animalHealth.animalHealth>0)
				Attack ();
					isAttacking=true;
				}
				else
				{
					animwander.enabled = true;
					PlayIdle();
				}
			}
			else
			{
				animwander.enabled = true;
		//		Debug.Log ("call wandering script " + gameObject.name);
				PlayIdle ();
			}
		
		}
	}
	
	public void LookAt (Transform tag)
	{
		isAttacking=false;
		animwander.enabled = false;
		
		var rotation = Quaternion.LookRotation (tag.position - transform.position);
		transform.rotation = Quaternion.Slerp (transform.rotation, rotation, Time.deltaTime * Damping);
		
	}
	
	public void Chase ()
	{
		isAttacking=false;
		//	if (goblinHealth > 0)
		// Debug.Log(gameObject.tag);
		if (gameObject.tag == "Goblin") {
			_anim.CrossFade ("run");
			
			
			_moveDirection = transform.forward;
			_moveDirection *= MoveSpeed;
			
			_moveDirection.y -= Gravity * Time.deltaTime;
			Charcontroller.Move (_moveDirection * Time.deltaTime);
		}
		
		//if (trollHealth > 0)
		if (gameObject.tag == "TrollEnemy") {
			_anim.CrossFade ("Run");
			
			
			_moveDirection = transform.forward;
			_moveDirection *= MoveSpeed;
			
			_moveDirection.y -= Gravity * Time.deltaTime;
			Charcontroller.Move (_moveDirection * Time.deltaTime);
		}
		
		//if (cyclopHealth > 0)
		if (gameObject.tag == "CyclopEnemy") {
			_anim.CrossFade ("run");
			
			
			_moveDirection = transform.forward;
			_moveDirection *= MoveSpeed;
			
			_moveDirection.y -= Gravity * Time.deltaTime;
			Charcontroller.Move (_moveDirection * Time.deltaTime);
		}
		// /////////////////////////////////////////////////////
		if (gameObject.tag == "skeleton") {
			_anim.CrossFade ("run");
			
			
			_moveDirection = transform.forward;
			_moveDirection *= MoveSpeed;
			
			_moveDirection.y -= Gravity * Time.deltaTime;
			Charcontroller.Move (_moveDirection * Time.deltaTime);
		}
	}
	
	public void Attack ()
	{
		if (Time.time > _attackTime) 
		{	
			if(Target!=null)
			if(Target.tag=="Player")
			{
		//	Debug.Log(Target.name +"  is attecked by "+ gameObject.name);
			_playerHealth.HandleHealth (AttackDamage);
			}
			else if(Target.tag=="Animal")
			{

				_animalHealth.HandleHealth(AttackDamage);
				Debug.Log(Target.name +" is attecked by "+gameObject.name);
			}
	
			
			if (gameObject.tag == "Goblin") 
			{
				_anim.CrossFade ("attack1");
				_attackTime = Time.time + AttackRepeatTime;
			}
			
			
			if (gameObject.tag == "TrollEnemy")
			{
				_anim.CrossFade ("Attack_01");
				_attackTime = Time.time + AttackRepeatTime;
			}
			
			
			if (gameObject.tag == "CyclopEnemy") 
			{
				_anim.CrossFade ("attack_1");
				_attackTime = Time.time + AttackRepeatTime;
			}
			
			// //////////////////////////////////////////
			if (gameObject.tag == "skeleton") 
			{
				
				_anim.CrossFade ("attack");
				
				_attackTime = Time.time + AttackRepeatTime;
			}
		}
	}
	
	void PlayIdle()
	{
		isAttacking=false;
		if (gameObject.tag == "Goblin") 
		{
			animwander.enabled=true;
			//_anim.CrossFade ("idle");
			
		}
		
		
		if (gameObject.tag == "TrollEnemy")
		{
			animwander.enabled=true;
			
		}
		
		
		if (gameObject.tag == "CyclopEnemy") 
		{
			animwander.enabled=true;
			
		}
	}



	public void health()
	{
		if (goblinHealth <= 0)
		{


			_anim.CrossFade("death");
			Destroy(gameObject,2);

			//			waveController.DecrementGoblin();
		}
		if (trollHealth <= 0)
		{
			//w.setEnermiesKilled(1);
			Debug.Log("troll dead : "+w.getNoEnermKiiled());
			_anim.CrossFade("Die");
			Destroy(gameObject,2);
			Debug.Log("Decrement Troll");
			//	waveController.DecrementTroll();
		}
		
		if (cyclopHealth <= 0)
		{
			//w.setEnermiesKilled(1);
			Debug.Log("c dead : "+w.getNoEnermKiiled());
			_anim.CrossFade("death");
			Destroy(gameObject, 1);
			//Debug.Log("");
			//waveController.DecrementCyclops();
			
		}
		
	}

	public void stat()
	{
		if (goblinHealth <= 0)
		{
			w.setEnermiesKilled(1);
		}
		if (trollHealth <= 0)
		{
			w.setEnermiesKilled(1);
			Debug.Log("troll dead : "+w.getNoEnermKiiled());

		}
		
		if (cyclopHealth <= 0)
		{
			w.setEnermiesKilled(1);
		}
	}
	
	void OnTriggerEnter(Collider g)
	{
		if (g.tag == "bullet")
		{
			
			if (gameObject.tag == "Goblin")
			{
				ScoreManager.score += goblinPlus;
				//                Debug.Log("You got " + ScoreManager.score + "points");
				_goblinSource.Play();
			}
			
			
			if (gameObject.tag == "TrollEnemy")
			{
				_trollsound.Play();
				ScoreManager.score += trollPlus;
			}
			
			
			if (gameObject.tag == "CyclopEnemy")
			{
				_cyclopsound.Play();
				ScoreManager.score += cyclPlus;
			}
		}
	}
	
	
	
	
}
