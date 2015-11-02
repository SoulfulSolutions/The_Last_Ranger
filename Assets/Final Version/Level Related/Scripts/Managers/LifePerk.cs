using UnityEngine; 
using UnityEngine.UI;
using System;
using System.Collections;

public class LifePerk : MonoBehaviour 
{
	GameObject The_Perk;
	PerksManager perks;
	PlayerHealth player;
	float timer = 3;
	public Image perkImage;
	bool startTime = false;

	public PowerUpPerk power;
	public PointsPerk points;

	// Use this for initialization
	void Start ()
	{
		The_Perk = GameObject.Find ("Life");
		perks = GameObject.Find ("Perks Holder").GetComponent<PerksManager> ();
		player = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
		perkImage.enabled = false;
		power.perkImage.enabled = false;
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
			player.ActivateLifePerk();
			perkImage.enabled = true;
			power.perkImage.enabled = false;
			points.perkImage.enabled = false;
			gameObject.SetActive(false);
			Debug.Log ("Perk Array Lenght : " + perks.Perks.Length);
		}

	}

	void ManagePerkTimer()
	{
		timer = timer - Convert.ToInt32(Time.deltaTime);
		Debug.Log ("Timer value is : " + timer);	
		
		if (timer <= 0) 
		{
			perkImage.enabled = false;
			Debug.Log("Perk has ended [Life]");
			startTime = false;
		}
		else{
			timer -= (Time.deltaTime);
			Debug.Log ("Timer value is : " + timer);
		}
	}

//	IEnumerator PerkLifeTime()
//	{
//		perkImage.enabled = true;
//		yield return new WaitForSeconds (1);
//		timer--;//2
//		Debug.Log ("Timer Value : "+timer);
//
//		yield return new WaitForSeconds (1);
//		Debug.Log ("Timer Value : "+timer);
//		timer--;//1
//		
//		yield return new WaitForSeconds (1);
//		Debug.Log ("Timer Value : "+timer);
//		perkImage.enabled = false;
//		Debug.Log("Perk has ended [Life]");
//		yield break;
//	}
}