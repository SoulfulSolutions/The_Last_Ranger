using UnityEngine;
using System.Collections;

public class BonusDisplay : MonoBehaviour 
{
	public GameObject text;
	SaveHorse horse;

	// Use this for initialization
	void Start () 
	{
		horse = GameObject.FindGameObjectWithTag ("Animal").GetComponent<SaveHorse> ();
		StartCoroutine (Display());
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	IEnumerator Display()
	{
		yield return new WaitForSeconds (2);
		horse.isSaved = false;
		text.SetActive (false);
		StopCoroutine (Display ());
	}
}
