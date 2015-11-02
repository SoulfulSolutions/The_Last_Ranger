using UnityEngine;
using System.Collections;

public class gameVolContr : MonoBehaviour {


	private AudioSource aud;
	// Use this for initialization
	void Awake () 
	{
		aud = GameObject.Find ("soundObject").GetComponent<AudioSource> ();
		aud.volume = PlayerPrefs.GetFloat("gamevol",1);
		Debug.Log ("Volume here : "+PlayerPrefs.GetFloat ("lastGameVal"));

		AudioListener.volume = PlayerPrefs.GetFloat ("mastervol");
		Debug.Log(PlayerPrefs.GetFloat("lastGameVal"));
	}
	
	// Update is called once per frame
	void Update () 
	{

	}
}
