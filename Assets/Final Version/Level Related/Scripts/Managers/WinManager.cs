using UnityEngine; 
using UnityEngine.UI;
using System.Collections;

public class WinManager : MonoBehaviour {

	public GameObject WinCanvas;

	public GameObject restart;
	public GameObject viewleaderboard;
	public GameObject share;
	public GameObject quit;
	public Text score;
	// Use this for initialization
	void Start () 
	{	

		if (FB.IsLoggedIn == false)
		{
			share.SetActive(false);
			viewleaderboard.SetActive(false);
		}

		score.text = ScoreManager.score.ToString();
	}

	// Update is called once per frame
	void Update () 
	{	
		Time.timeScale = 1;
	}

	public void Pressed_Restart()
	{
		Application.LoadLevel (7);
	}

	public void Pressed_Share_Score()
	{
		FB.ShareLink ("http://last3.azurewebsites.net/lastranger/", "The Last Ranger", "I just played this awesome game and got a score of " + ScoreManager.score + ". Click here to beat my score!", "", null); 
	}

	public void Pressed_Leaderboard()
	{
		Application.LoadLevel (11);
	}

	public void QuitGame()
	{
		if (FB.IsLoggedIn)
			Application.LoadLevel ("Menu for facebook"); 
		else {
			Application.LoadLevel ("Menu");
		}
	}
}
