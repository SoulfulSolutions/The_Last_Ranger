using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Leaderboard : MonoBehaviour 
{
	public GameObject LeaderboardCanvas;
	public GameObject GameOverCanvas;
	public GameObject viewleaderboard;
	public GameObject morethan10;

	public Button back;
	public Button restart;
	public GameObject share;
	public Button quit;
	
	public Text display;
	public Text txtscore;
	
	
	
	private Dictionary<string, string> profile = null;
	
	// Use this for initialization
	void Start () 
	{
		if (FB.IsLoggedIn== false)
		{
			share.SetActive(false);
			viewleaderboard.SetActive(false);
		}
		
		Cursor.visible = true;
		GameOverCanvas.SetActive (true);
		LeaderboardCanvas.SetActive(  false);
		morethan10.SetActive (false);
		
		
		
		
		//LeaderboardCanvas = LeaderboardCanvas.GetComponent<GameObject>();
		//GameOverCanvas = GameOverCanvas.GetComponent<GameObject>();
		
		
		viewleaderboard = viewleaderboard.GetComponent<GameObject>();
		back = back.GetComponent<Button>();
		restart = restart.GetComponent<Button>();
		share = share.GetComponent<GameObject>();
		quit = quit.GetComponent<Button>();
		
		
		display = display.GetComponent<Text>();
		txtscore = txtscore.GetComponent<Text>();
		txtscore.text = ScoreManager.score.ToString();
		
		
	}
	
	
	public void ViewLeaderboard()
	{
		
		GameOverCanvas.SetActive(true);
		LeaderboardCanvas.SetActive (true);

		string url = "http://rangerdata.azurewebsites.net/";
		
		//when the button is clicked        
		//setup url to the ASP.NET webpage that is going to be called
		string customUrl = url + "Leaderboard/Create";
		
		//setup a form
		WWWForm form = new WWWForm();
		
		form.AddField("FBuserID", FB.UserId);
		form.AddField("Score", ScoreManager.score);
		//username
		//Call the server
		WWW www = new WWW(customUrl, form);
		StartCoroutine(WaitForRequest(www));
	}
	
	
	IEnumerator WaitForRequest(WWW www)
	{
		yield return www;
		GameOverCanvas.SetActive(false);
		
		if (www.error == null)
		{
			LeaderboardCanvas.SetActive(true);
			Debug.Log("Content of array :" + www.text);
			string[] info = www.text.Split(',');
			
			display.text =  "";
			
			
			
			if(info.Length>1)
			{
				
				GameObject.Find("txtrank1").GetComponent<Text>().text= "1";

				GameObject.Find("txtname1").GetComponent<Text>().text=info[0];
				GameObject.Find("txtscore1").GetComponent<Text>().text=info[1];
			}
			if(info.Length>3)
			{
				GameObject.Find("txtrank2").GetComponent<Text>().text= "2";
				GameObject.Find("txtname2").GetComponent<Text>().text=info[2];
				GameObject.Find("txtscore2").GetComponent<Text>().text=info[3];
			}
			if(info.Length>5)
			{
				GameObject.Find("txtrank3").GetComponent<Text>().text= "3";
				GameObject.Find("txtname3").GetComponent<Text>().text=info[4];
				GameObject.Find("txtscore3").GetComponent<Text>().text=info[5];
			}
			if(info.Length>7)
			{
				GameObject.Find("txtrank4").GetComponent<Text>().text= "4";
				GameObject.Find("txtname4").GetComponent<Text>().text=info[6];
				
				GameObject.Find("txtscore4").GetComponent<Text>().text=info[7];
			}
			if(info.Length>9)
			{
				GameObject.Find("txtrank5").GetComponent<Text>().text= "5";
				GameObject.Find("txtname5").GetComponent<Text>().text=info[8];
				GameObject.Find("txtscore5").GetComponent<Text>().text=info[9];
			}
			if(info.Length>11)
			{
				GameObject.Find("txtrank6").GetComponent<Text>().text= "6";
				GameObject.Find("txtname6").GetComponent<Text>().text=info[10];
				GameObject.Find("txtscore6").GetComponent<Text>().text=info[11];
			}
			if(info.Length>13)
			{
				GameObject.Find("txtrank7").GetComponent<Text>().text= "7";
				GameObject.Find("txtname7").GetComponent<Text>().text=info[12];
				GameObject.Find("txtscore7").GetComponent<Text>().text=info[13];
			}
			if(info.Length>15)
			{
				GameObject.Find("txtrank8").GetComponent<Text>().text= "8";
				GameObject.Find("txtname8").GetComponent<Text>().text=info[14];
				GameObject.Find("txtscore8").GetComponent<Text>().text=info[15];
			}
			if(info.Length>17)
			{
				GameObject.Find("txtrank9").GetComponent<Text>().text= "9";
				GameObject.Find("txtname9").GetComponent<Text>().text=info[16];
				GameObject.Find("txtscore9").GetComponent<Text>().text=info[17];
			}
			if(info.Length>19)
			{
				GameObject.Find("txtrank10").GetComponent<Text>().text= "10";
				GameObject.Find("txtname10").GetComponent<Text>().text=info[18];
				GameObject.Find("txtscore10").GetComponent<Text>().text=info[19];
			}

			if(info.Length>21)
			{
				morethan10.SetActive(true);
			}
			
			//            for (int i = 0; i < info.Length-1; i+=2)
			//            {
			//                display.text += info[i] + "\t" + info[i + 1] + "\n";
			//
			//                
			//            }
			
			
			
		}
		else
		{
			
			Debug.Log("WWW Error: " + www.error);
		}
	}
	
	public void Back()
	{
		GameOverCanvas.SetActive(true);
		LeaderboardCanvas.SetActive(false);
	}
	
	public void Restart()
	{
		Delete ();
		ScoreManager.score = 0;
		Application.LoadLevel ("Wave 1");
	}
	public void ShareScore()
	{
		FB.ShareLink ("http://ranger.azurewebsites.net/thelastranger/", "The Last Ranger", "I just played this awesome game and got a score of " + ScoreManager.score + ". Click here to beat my score!", "", null); 
		
	}
	public void QuitGame()
	{
		if (FB.IsLoggedIn)
			Application.LoadLevel ("Menu for facebook"); 
		else {
			Application.LoadLevel ("Menu");
		}
	}


	public void Delete()
	{
		string url = "http://rangerdata.azurewebsites.net/";
		
		
		string customUrl = url + "SaveGame/Delete";
		
		
		//setup a form
		WWWForm form = new WWWForm();
		
		//creates a random user
		
		form.AddField("FBuserID", FB.UserId);
		form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString());
		//Call the server
		WWW www = new WWW(customUrl, form);
		StartCoroutine(WaitForRequest1(www));
	}
	
	IEnumerator WaitForRequest1(WWW www)
	{
		yield return www;
		
		// check for errors
		if (www.error == null)
		{
			Debug.Log(www.text);
		}
		else
		{
			
			Debug.Log("WWW Error: " + www.error);
		}
	}
	
	
	
}
