using UnityEngine;
using System.Collections;

public class enterwater : MonoBehaviour {
	private AudioSource watersound;
	// Use this for initialization
	bool isInWater;
	void Start () {

		watersound = GameObject.FindGameObjectWithTag ("water").GetComponent<AudioSource> ();
		watersound.Stop();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	void OnTriggerEnter(Collider c)
	{
		isInWater = true;
		if (c.tag == "Player") 
		{

			watersound.Play();
		}


	}
	void OnTriggerExit(Collider c)
	{
		isInWater = false;
		if (c.tag == "Player") 
		{
			watersound.Stop();
		}
		
		
	}
}
