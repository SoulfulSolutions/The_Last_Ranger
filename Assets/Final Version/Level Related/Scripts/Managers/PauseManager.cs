using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
	public Canvas PauseMenu; //Create a variable for the Pause Menu Canvas
	public Canvas QuitMenu; //Create a variable for the Quit Menu Canvas
	public Button Resume; //Create a variable for the Resume Button
	public Button Restart; //Create a variable for the Restart Button
	public Button Quit;  //Create a variable for the Quit Button
	public  bool _paused = false, _waited = true;  //Create a paused and waited variable.
	
	public GameObject loadPanel;
	public GameObject ConfirmQuit;
	private void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		PauseMenu = PauseMenu.GetComponent<Canvas>(); //Getting the Pause Menu Component and placing it into the created variable
		QuitMenu = GetComponent<Canvas>(); //Getting the Quit Menu Component and placing it into the created variable
		Resume = Resume.GetComponent<Button>(); //Getting the Resume Button Component and placing it into the created variable
		Restart = Restart.GetComponent<Button>(); //Getting the Restart Button Component and placing it into the created variable
		Quit = Quit.GetComponent<Button>(); //Getting the Quit Button Component and placing it into the created variable
		
		QuitMenu.enabled = false; //Disabling the Quit Menu Canvas on Load
		PauseMenu.enabled = false; //Disabling the Pause Menu Canvas on Load
	}
	
	private void waiting()
	{
		_waited = true;
	}
	
	public void ViewLoadGame()
	{
		loadPanel.SetActive (true);
		GameObject.Find ("PausePanel").SetActive(false);
		
	}
	private void Update()
	{
		
		if (_paused==true)
		{
			PauseMenu.enabled = true;
		}
		else if(_paused==false)
		{
			PauseMenu.enabled = false;
		}
		
		
		// if(_waited)
		{
			if (Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P)) //if the escape key is pressed
			{
				Cursor.visible = true;
				PauseMenu.enabled = true;
				
				
				if (_paused)
				{
					_paused = false; //if game is paused, unpause it
				}
				else
				{
					_paused = true;//if game is unpaused, pause it 
				}
				
				_waited = false;
				Invoke("waiting", 0.3f);
			}
			
			if(_paused)
			{
				Time.timeScale = 0; //Set the Game timescale to 0. Pauses the game at that time
			}
			else
			{
				Time.timeScale = 1; //Set the Game timescale to 1. Plays the game 
			}
		}       
	}
	
	public void RestartGame()
	{
		ScoreManager.score = 0;	
	
		Application.LoadLevel("Wave 1");//Reload the level
	}
	
	public void ResumeGame()
	{
		_paused = false; //unpause the game
		_waited = false;
		
		//   PauseMenu.enabled = false; //disable pause menu
		
	}
	
	public void QuitButton()
	{
		ConfirmQuit.SetActive (true);
		Debug.Log ("Sya le method iryt");//enable quit menu
	}
	
	public void YesButton()
	{
		if (FB.IsLoggedIn)
			Application.LoadLevel ("Menu for facebook");//Goes to the main menu
		else
			Application.LoadLevel ("Menu");

		ScoreManager.score = 0;
	}
	
	public void NoButton()
	{
		PauseMenu.enabled = true; //enable pause menu
		QuitMenu.enabled = false; //disable quit menu
	}
	
	public void setPause(bool p)
	{
		_paused = p;
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
		StartCoroutine(WaitForRequest(www));
	}
	
	IEnumerator WaitForRequest(WWW www)
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
