using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveHorse : MonoBehaviour 
{

	GameObject the_horse;
	public bool isSaved = false;
	public GameObject bonus;
	wave1stat wave;

	// Use this for initialization
	void Start ()
	{
		wave = GameObject.Find ("Wave Stats").GetComponent<wave1stat> ();
		the_horse = GameObject.FindGameObjectWithTag ("Animal");
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "mat") 
		{
			isSaved = true;
			bonus.SetActive(true);
			gameObject.SetActive(false);
			Debug.Log ("Animal Saved");
			ScoreManager.score += 10;
			wave.setNoAnimalsS(1);
		}
	}
}
