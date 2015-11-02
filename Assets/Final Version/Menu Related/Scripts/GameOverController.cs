using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
	{
		Cursor.lockState = CursorLockMode.None;
	}
		public void Restart()
		{
		ScoreManager.score = 0;
		Application.LoadLevel("Wave 1");
		}
	public void Quit()
	{
		ScoreManager.score = 0;
		if (FB.IsLoggedIn)
			Application.LoadLevel ("Menu for facebook");
		else
			Application.LoadLevel ("Menu");
	}

	public void ShareScore()
	{
		FB.ShareLink ("http://last3.azurewebsites.net/lastranger/", "The Last Ranger", "I just played this awesome game and got a score of " + ScoreManager.score + ". Click here to beat my score!", "", null); 
		
	}

	}

