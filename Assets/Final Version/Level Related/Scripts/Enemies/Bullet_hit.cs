using UnityEngine;
using System.Collections;

public class Bullet_hit : MonoBehaviour {
	
	private GameObject bullet;
	private float health ;
	GameObject gameobject;
	
	public int damage = 10;
	public int goblinPlus = 1;
	public int trollPlus = 5;
	public int cyclPlus = 3;
	
	// Update is called once per frame
	
	
	void start()
	{
		bullet = GameObject.Find ("BulletHole(Clone)");

	}
	
	void Update () 
	{
		Destroy (GameObject.Find ("BulletHole(Clone)"), 0.1f);
	}
	
	void OnTriggerEnter(Collider thecollider)
	{

		if (gameObject.name.Contains ("Horse") && thecollider.tag == "bullet") 
		{
			gameObject.GetComponent<AnimalHealth>().animalHealth-=10;
			if(ScoreManager.score > 0)
			{
				ScoreManager.score -= 10;
			}
			else
				ScoreManager.score = 0;
		}


		if (thecollider.tag == "bullet")
		{
			if (gameObject.name == "bulletDetectGoblin")
			{
				Debug.Log ("you have shot the box " + gameObject.name);
				
				health =gameObject.GetComponentInParent<EnemyAI>().goblinHealth -= damage;
				ScoreManager.score += goblinPlus;
				
			
			} 
			
			if (gameObject.name == "bulletDetectCyclone") 
			{
				Debug.Log ("you have shot the box " + gameObject.name);
				
				health = gameObject.GetComponentInParent<EnemyAI>().cyclopHealth -= damage;
				ScoreManager.score += cyclPlus;
				
//			
			}  
			
			if (gameObject.name == "bulletDetectTroll") 
			{
				Debug.Log ("you have shot the box " + gameObject.name);
				ScoreManager.score += trollPlus;
				health = gameObject.GetComponentInParent<EnemyAI>().trollHealth -= damage;
				

			}
		}
	}
	
	public void ActivatePowerUpPerk()
	{
		damage = damage * 2;
	}
	
	
	public void ActivateDoublePoints()
	{
		goblinPlus = goblinPlus * 2;
		trollPlus = trollPlus * 2;
		cyclPlus = cyclPlus * 2;
	}
}


