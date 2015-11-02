using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LoadGame : MonoBehaviour {
	
	public GameObject Save1Objet;
	public GameObject Save2Objet;
	public GameObject Save3Objet;
	public GameObject Save4Objet;
	public GameObject Save5Objet;
	
	public GameObject ConfirmLoadPanel;  //confirmation message box
	public GameObject PausePanel;        //pause panel
	
	public string[] Saves;               //array that will store all the saved games from the server
	string loadsceneNo;                  //this is the scen number to load when the gamer selects one of the saved games
	
	private string url = "http://rangerdata.azurewebsites.net/";
	
	float loadtimer=0.3f;
	void Start () 
	{
		PlayerPrefs.DeleteKey("Load");  //delete this player prefeb value at the biginning.
		//This will be used through the waves scene to check if the wave shouuld load saved game or just run from initial point
		LoadGameStates ();				//Calls the load game states method
	}
	
	void Update () 
	{
		loadtimer -= Time.deltaTime;
		
		//		if (loadtimer < 0) 
		//		{
		//			Debug.Log("It came here");
		//			LoadGameStates ();
		//			loadtimer=0.3f;
		//		}
		
		LoadGameStates ();
		
		//		if (Saves.Length > 1)			 //checks if the array is carring data of the 1st saved game
		//		{
		//			Save1Objet.SetActive (true); //if the condition is true then enable the object that is holding all the objects to display data of the first saved game in an array
		//			if(Save1Objet.activeInHierarchy==true)//if the object is active on the hierarchy the display data on the following objects. This is just so we dont get errors
		//			{
		//			GameObject.Find ("Save1lblWaveNo").GetComponent<Text> ().text = Saves [1];
		//			GameObject.Find ("Save1lblName").GetComponent<Text> ().text = Saves [2];
		//			GameObject.Find ("Save1lblScore").GetComponent<Text> ().text = Saves [3];
		//			GameObject.Find ("Save1lblDate").GetComponent<Text> ().text = Saves [4];
		//			
		//			}
		//		}
		//		
		//		if (Saves.Length > 6) {
		//			Save2Objet.SetActive (true);
		//			if(Save2Objet.activeInHierarchy==true)
		//			{
		//			
		//			GameObject.Find ("Save2lblWaveNo").GetComponent<Text> ().text = Saves [6];
		//			GameObject.Find ("Save2lblName").GetComponent<Text> ().text = Saves [7];
		//			GameObject.Find ("Save2lblScore").GetComponent<Text> ().text = Saves [8];
		//			GameObject.Find ("Save2lblDate").GetComponent<Text> ().text = Saves [9];
		//			}
		//		}
		//		
		//		if (Saves.Length > 11) {
		//			Save3Objet.SetActive (true);
		//			if(Save3Objet.activeInHierarchy==true)
		//			{
		//			GameObject.Find ("Save3lblWaveNo").GetComponent<Text> ().text = Saves [11];
		//			GameObject.Find ("Save3lblName").GetComponent<Text> ().text = Saves [12];
		//			GameObject.Find ("Save3lblScore").GetComponent<Text> ().text = Saves [13];
		//			GameObject.Find ("Save3lblDate").GetComponent<Text> ().text = Saves [14];
		//			}
		//		}
		//		
		//		if (Saves.Length > 16) {
		//			Save4Objet.SetActive (true);
		//			if(Save4Objet.activeInHierarchy==true)
		//			{
		//			
		//			GameObject.Find ("Save4lblWaveNo").GetComponent<Text> ().text = Saves [16];
		//			GameObject.Find ("Save4lblName").GetComponent<Text> ().text = Saves [17];
		//			GameObject.Find ("Save4lblScore").GetComponent<Text> ().text = Saves [18];
		//			GameObject.Find ("Save4lblDate").GetComponent<Text> ().text = Saves [19];
		//			}
		//		}
		//		
		//		if (Saves.Length > 21) {
		//			Save5Objet.SetActive (true);
		//			if(Save5Objet.activeInHierarchy==true)
		//			{
		//			
		//			GameObject.Find ("Save5lblWaveNo").GetComponent<Text> ().text = Saves [21];
		//			GameObject.Find ("Save5lblName").GetComponent<Text> ().text = Saves [22];
		//			GameObject.Find ("Save5lblScore").GetComponent<Text> ().text = Saves [23];
		//			GameObject.Find ("Save5lblDate").GetComponent<Text> ().text = Saves [24];
		//			}
		//		}
		
	}
	
	//the following are the method for each button. since we going to be displaying up to 5 saved game. each button is for each saved game 
	public void pressLoad1()                 // this will be executed if the first button is pressed
	{
		loadsceneNo = Saves [1];            //taking the wave number of the first game from the array of the saved games     
		PlayerPrefs.SetString("Load", Saves[0]);//this is the gameID from the server database. This will help use when we want to load enemies and player to know enemies and player of which saved game to load. Also taking it from the array
		if (Application.loadedLevelName .Contains( "Wave")) //checks if we are in wave, because we don'tt show confirm load when loading the game from the menu..
		{
			ConfirmLoadPanel.SetActive (true);        //sets the confirmation box to true
			gameObject.SetActive (false);			//sets the game object to false, which is the Pause Panel.
		}
		
	}
	public void pressLoad2()
	{
		loadsceneNo = Saves[6];
		PlayerPrefs.SetString("Load", Saves[5]);
		if (Application.loadedLevelName .Contains( "Wave")) 
		{
			ConfirmLoadPanel.SetActive (true);
			gameObject.SetActive (false);
		}
	}
	public void pressLoad3()
	{
		loadsceneNo = Saves[11];
		PlayerPrefs.SetString("Load", Saves[10]);
		if (Application.loadedLevelName.Contains( "Wave")) 
		{
			ConfirmLoadPanel.SetActive (true);
			gameObject.SetActive (false);
		}
	}
	public void pressLoad4()
	{
		loadsceneNo = Saves[16];
		PlayerPrefs.SetString("Load", Saves[15]);
		if (Application.loadedLevelName .Contains( "Wave"))
		{
			ConfirmLoadPanel.SetActive (true);
			gameObject.SetActive (false);
		}
	}
	public void pressLoad5()
	{
		loadsceneNo =Saves[21];
		PlayerPrefs.SetString("Load", Saves[20]);
		if (Application.loadedLevelName .Contains( "Wave"))
		{
			ConfirmLoadPanel.SetActive (true);
			gameObject.SetActive (false);
		}
	}
	
	
	public void LoadConfirmedYes()  //this method will be called on the Confirm load button Yes on the confirm Box
	{
		Application.LoadLevel (loadsceneNo);  //this will load the wave that we stored in LoadsceNo when the player clicks between the 5 load buttons
	}
	public void loadConfirmNo()//this will be called on the confirm load button NO on the confirm box
	{
		gameObject.SetActive (true);
		ConfirmLoadPanel.SetActive (false);
	}
	
	public void pressedCancel()//this will be called when the user clicks cancel on the panel that displays all the saved games
	{
		PausePanel.SetActive(true);
		gameObject.SetActive (false);
	}
	
	public void LoadGameStates()
	{
		
		
		
		string customUrl = url + "SaveGame/Details";
		
		//setup a form
		WWWForm form = new WWWForm();
		
		form.AddField("FBuserID", FB.UserId);
		form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString());
		
		//Call the server
		WWW www = new WWW(customUrl, form);
		StartCoroutine(LoadPlayerWaitForRequest(www));
	}
	
	
	IEnumerator LoadPlayerWaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null) 
		{      //write data returned from ASP.NET
			
			Debug.Log (www.text);
			Saves = www.text.Split (',');
			
			
			if (Saves.Length > 1)			 //checks if the array is carring data of the 1st saved game
			{
				Save1Objet.SetActive (true); //if the condition is true then enable the object that is holding all the objects to display data of the first saved game in an array
				if(Save1Objet.activeInHierarchy==true)//if the object is active on the hierarchy the display data on the following objects. This is just so we dont get errors
				{
					GameObject.Find ("Save1lblWaveNo").GetComponent<Text> ().text = Saves [1];
					GameObject.Find ("Save1lblName").GetComponent<Text> ().text = Saves [2];
					GameObject.Find ("Save1lblScore").GetComponent<Text> ().text = Saves [3];
					GameObject.Find ("Save1lblDate").GetComponent<Text> ().text = Saves [4];
					
				}
			}
			
			if (Saves.Length > 6) {
				Save2Objet.SetActive (true);
				if(Save2Objet.activeInHierarchy==true)
				{
					
					GameObject.Find ("Save2lblWaveNo").GetComponent<Text> ().text = Saves [6];
					GameObject.Find ("Save2lblName").GetComponent<Text> ().text = Saves [7];
					GameObject.Find ("Save2lblScore").GetComponent<Text> ().text = Saves [8];
					GameObject.Find ("Save2lblDate").GetComponent<Text> ().text = Saves [9];
				}
			}
			
			if (Saves.Length > 11) {
				Save3Objet.SetActive (true);
				if(Save3Objet.activeInHierarchy==true)
				{
					GameObject.Find ("Save3lblWaveNo").GetComponent<Text> ().text = Saves [11];
					GameObject.Find ("Save3lblName").GetComponent<Text> ().text = Saves [12];
					GameObject.Find ("Save3lblScore").GetComponent<Text> ().text = Saves [13];
					GameObject.Find ("Save3lblDate").GetComponent<Text> ().text = Saves [14];
				}
			}
			
			if (Saves.Length > 16) {
				Save4Objet.SetActive (true);
				if(Save4Objet.activeInHierarchy==true)
				{
					
					GameObject.Find ("Save4lblWaveNo").GetComponent<Text> ().text = Saves [16];
					GameObject.Find ("Save4lblName").GetComponent<Text> ().text = Saves [17];
					GameObject.Find ("Save4lblScore").GetComponent<Text> ().text = Saves [18];
					GameObject.Find ("Save4lblDate").GetComponent<Text> ().text = Saves [19];
				}
			}
			
			if (Saves.Length > 21) {
				Save5Objet.SetActive (true);
				if(Save5Objet.activeInHierarchy==true)
				{
					
					GameObject.Find ("Save5lblWaveNo").GetComponent<Text> ().text = Saves [21];
					GameObject.Find ("Save5lblName").GetComponent<Text> ().text = Saves [22];
					GameObject.Find ("Save5lblScore").GetComponent<Text> ().text = Saves [23];
					GameObject.Find ("Save5lblDate").GetComponent<Text> ().text = Saves [24];
				}
			}
			
			
			
		} else {
			Debug.Log ("WWW Error: " + www.error);
		}
	}
}
