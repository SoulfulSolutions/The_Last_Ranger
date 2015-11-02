using UnityEngine; 
using UnityEngine.UI;
using System;
using System.Collections;

public class PowerUpPerk : MonoBehaviour 
{
	GameObject The_Perk;
	PerksManager perks;
	Bullet_hit enemy;
	float timer = 10;
	public Image perkImage;
	bool startTime = false;

	public LifePerk life;
	public PointsPerk points;

	// Use this for initialization
	void Start ()
	{
		The_Perk = GameObject.Find ("Power Up");
		perks    = GameObject.Find ("Perks Holder").GetComponent<PerksManager> ();
		enemy    = GameObject.FindGameObjectWithTag("shotEnemy").GetComponent<Bullet_hit> ();

		perkImage.enabled = false;
		life.perkImage.enabled = false;
		points.perkImage.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (startTime == true) 
		{
			ManagePerkTimer();
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player")
		{
			startTime = true;
			enemy.ActivatePowerUpPerk();
			perkImage.enabled = true;
			life.perkImage.enabled = false;
			points.perkImage.enabled = false;
			gameObject.SetActive(false);
			Debug.Log ("Perk Array Lenght : " + perks.Perks.Length);
		}
	}

	void ManagePerkTimer()
	{
		timer = timer - (Time.deltaTime);
		Debug.Log ("Timer value is : " + timer);
		
		if (timer <= 0) 
		{
			enemy.damage = 10;
			perkImage.enabled = false;
			Debug.Log("Perk has ended . Damage is :" + enemy.damage);
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
//		enemy.damage = 10;
//		perkImage.enabled = false;
//		Debug.Log("Perk has ended . Damage is :" + enemy.damage);
//		yield break;
//	}
}

