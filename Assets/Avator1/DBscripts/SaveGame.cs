using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveGame : MonoBehaviour {

	//public Button Save; //Create a variable for the Resume Button
	public Text txtSaveName;
    public string user;
	SavePlayerPos savepp;
    SaveEnemyPos saveEp;
    private SavePlayerPos savePerkPos;


	[HideInInspector]
	public string Responce;


	// Use this for initialization
	void Start () 
	{

        savepp = GameObject.FindGameObjectWithTag("Player").GetComponent<SavePlayerPos>();
		
		user = FB.UserId;
	}
	
	// Update is called once per frame
	void Update () 
	{
//		if(Input.GetKeyDown(KeyCode.X))
//		{
//			SaveGameState();
//			//saveCanvas.enabled=true;
//		}
	}

	public void buttonSave ()
	{
		//saveCanvas.enabled = false;
	}
	public void SaveGameState()
	{

		string url = "http://rangerdata.azurewebsites.net/";
		
		//when the button is clicked        
		//setup url to the ASP.NET webpage that is going to be called
		string customUrl = url + "SameGame/Create";
		
		//setup a form
		WWWForm form = new WWWForm();
		
		//Setup the paramaters
		
		//creates a random user
		
		form.AddField("GameName","Not Saved");
		form.AddField("GameScore"," ");
		form.AddField("GameWaveNo"," ");
		form.AddField("username","LastRanger0");

		//Call the server
		WWW www = new WWW(customUrl, form);
		StartCoroutine( WaitForRequest(www));

	    savepp.SavePosi ();
//        saveEp.SavePosi();
        savePerkPos.SavePosi();

	}

	public void EditSaveGameState() // GamerID to be a accessable
	{
		
		string url = "http://rangerdata.azurewebsites.net/";
		
		//when the button is clicked        
		//setup url to the ASP.NET webpage that is going to be called
		string customUrl = url + "SaveGame/Create";
		
		//setup a form
		WWWForm form = new WWWForm();
		
		//Setup the paramaters
		
		//creates a random user
		
		form.AddField("GameName","Not Saved");
		form.AddField("GameScore"," ");
		form.AddField("GameWaveNo"," ");
		form.AddField("username","LastRanger0");
		
		//Call the server
		WWW www = new WWW(customUrl, form);
		StartCoroutine( WaitForRequest(www));
		
		savepp.SavePosi ();
//        saveEp.SavePosi();
        savePerkPos.SavePosi();
	    

	}
	
	public IEnumerator WaitForRequest(WWW www)
	{
		yield return www;       
		// check for errors
		Responce = www.text;
		if (www.error == null)
		{      //write data returned from ASP.NET
			Debug.Log(Responce);
		} 
		else 
		{
			Debug.Log("WWW Error: "+ www.error);
		}
	}
	
	}

