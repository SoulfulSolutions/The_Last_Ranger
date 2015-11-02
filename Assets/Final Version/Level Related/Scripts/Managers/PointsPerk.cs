using UnityEngine; 
using UnityEngine.UI;
using System;
using System.Collections;

public class PointsPerk : MonoBehaviour
{
	GameObject The_Perk;//the perk we looking at currently
	PerksManager perks;//referencing the PerkManager class
	Bullet_hit enemy;//referencing the Bullet_Hit class 

	float timer = 10;//timer variable
	public Image perkImage;//image for current perk
	bool startTime = false;
	public LifePerk life;
	public PowerUpPerk power;
	
	// Use this for initialization
	void Start ()
	{
		The_Perk = GameObject.Find ("Double Points");
		perks    = GameObject.Find ("Perks Holder").GetComponent<PerksManager> ();
		enemy    = GameObject.FindGameObjectWithTag ("shotEnemy").GetComponent<Bullet_hit> ();
		perkImage.enabled = false;
		life.perkImage.enabled = false;
		power.perkImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (startTime == true) 
		{
			ManagePerkTimer();
		}
	}

	void OnTriggerEnter(Collider col)//when collision with perk happens
	{
		if (col.tag == "Player")//if player collides with perk
		{
			startTime = true;
			enemy.ActivateDoublePoints();//running appropriate mehtod
			perkImage.enabled = true;
			life.perkImage.enabled = false;
			power.perkImage.enabled = false;
			gameObject.SetActive(false);//make the perk dissaper
			Debug.Log ("Perk Array Lenght : " + perks.Perks.Length);
		}
	}

	void ManagePerkTimer()
	{
		timer -= (Time.deltaTime);
		Debug.Log ("Timer value is : " + timer);

		if (timer <= 0) {
			perkImage.enabled = false;
			enemy.goblinPlus = 1;
			enemy.cyclPlus = 3;
			enemy.trollPlus = 5;
			startTime = false;
		} 
		else {
			timer -= (Time.deltaTime);
			Debug.Log ("Timer value is : " + timer);
		}
	}

//	IEnumerator PerkLifeTime()
//	{
//		perkImage.enabled = true;
//		yield return new WaitForSeconds (1);
//		timer--;//9
//
//		yield return new WaitForSeconds (1);
//		timer--;//8
//
//		yield return new WaitForSeconds (1);
//		timer--;//7
//
//		yield return new WaitForSeconds (1);
//		timer--;//6
//
//		yield return new WaitForSeconds (1);
//		timer--;//5
//
//		yield return new WaitForSeconds (1);
//		timer--;//4
//	
//		yield return new WaitForSeconds (1);
//		timer--;//3
//
//		yield return new WaitForSeconds (1);
//		timer--;//2
//
//		yield return new WaitForSeconds (1);
//		timer--;//1
//
//		yield return new WaitForSeconds (1);
//		perkImage.enabled = false;
//
//		enemy.goblinPlus = 1;
//		enemy.cyclPlus = 3;
//		enemy.trollPlus = 5;
//
//		Debug.Log("Perk has ended");
//		Debug.Log ("Goblin Point Value : "+enemy.goblinPlus);
//		Debug.Log ("Cyclops Point Value : "+enemy.cyclPlus);
//		Debug.Log ("Troll Point Value : "+enemy.trollPlus);
//		yield break;
//	}
}

