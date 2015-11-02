using UnityEngine;
using System.Collections;

public class AnimalHealth : MonoBehaviour 
{
	public float animalHealth=100;
	wave1stat wave;

	void Start()
	{
		wave = GameObject.Find ("Wave Stats").GetComponent<wave1stat> ();
	}
  	void Update()
	{
		if (animalHealth <= 0) {
			gameObject.SetActive(false);
			wave.setNoAnimalsD(1);
		}
	}
	public void HandleHealth(float damageAmount) //function to handle hea
	{
		// Reduce the current health by the damage amount.
		if(animalHealth > 0)
		{
			animalHealth -= (damageAmount);
			Debug.Log(animalHealth.ToString());
		}   

	}
}
