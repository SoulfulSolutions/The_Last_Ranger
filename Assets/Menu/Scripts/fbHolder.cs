using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.MiniJSON;


public class fbHolder : MonoBehaviour {

	public GameObject UIloggedIn;
	public GameObject UInotLoggedIn;

	public GameObject UIUsername;


	public GameObject btnlogin;




	//CreateUsers createuser;


	private Dictionary<string, string> profile=null;

    public static bool menuContr = false;
    bool firsttime = false;
	 void Awake()
	{
		FB.Init (SetInit, onHideUnity);
		
		UIloggedIn.SetActive (false);
		UInotLoggedIn.SetActive (false);
		
	}

     Texture2D pictex = null;
     bool showPic = false;
     void OnGUI()
     {
         if (showPic)
         {
             GUI.DrawTexture(new Rect(970, 0, 128, 128), pictex);
         }
     }
	void start()
	{
	//	createuser=gameObject.GetComponent<CreateUsers>();

		UIloggedIn.SetActive (false);
		UInotLoggedIn.SetActive (false);
	}

	private void SetInit()
	{
		Debug.Log ("fb initial done");
		enabled = true;
		if (FB.IsLoggedIn) 
		{
			Debug.Log ("fb logged in");
			dealsWithMenu(true);
		} 

	}

	private void onHideUnity(bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}


	public void FBlogin()
	{
	//	FB.API ("/me/permissions", Facebook.HttpMethod.GET, callback);
		FB.LogInWithReadPermissions("email", AuthCallback);
	}

	//public void callback(FBResult res)
	//{
	//	Debug.Log (res.Text);

//	}

	void AuthCallback(FBResult result)
	{
		if (FB.IsLoggedIn) 
		{

			dealsWithMenu(true);

		} 
		else 
		{
			Debug.Log ("login failed");
			dealsWithMenu(false);
		}
	}

	public void dealsWithMenu(bool isLoggedIn)
	{
		if (isLoggedIn) 
		{
			UIloggedIn.SetActive(true);
			UInotLoggedIn.SetActive(false);
			btnlogin.SetActive (false);
		
			StartCoroutine(getPic());
//			picHolder.SetActive(true);
			FB.API ("/me?fields=id,first_name"        , Facebook.HttpMethod.GET , dealsWithUserName);
		} 
		else 
		{

			UIloggedIn.SetActive(false);
		
			//sUInotLoggedIn.SetActive(true);
			btnlogin.SetActive (false);
		

		
		}
	}

	public IEnumerator getPic()
	{
		var www = new WWW ("http://graph.facebook.com/" + FB.UserId + "/picture?type=square");
		yield return www;


        pictex = new Texture2D(25, 25);
        www.LoadImageIntoTexture(pictex);

        showPic = true;
    }
 		//something changed




	void dealsWithUserName(FBResult res)
	{
		if (res.Error != null) {
			Debug.Log("Error with getting first name "  );
			FB.API("/me?fields=id,first_name",Facebook.HttpMethod.GET , dealsWithUserName);
			return;
		}

		profile = Util.DeserializeJSONProfile (res.Text);

		Text usermsg = UIUsername.GetComponent<Text> ();

		usermsg.text = profile ["first_name"];

		string fbusername = profile ["first_name"];

	CreateFBUser (fbusername);	 
	}


    public void invite()
    {
        FB.AppRequest("Hey check out this new game called The Last Ranger",
                        null,
                        null, 
                        null,
                        50,
                        string.Empty,
                        "Invite your friends to play the last ranger",
                        callb
                        );
    }

    public void callb(FBResult res)
    {
        Util.Log("appRequestCallback");
        if (res != null)
        {

            var responseObject = Json.Deserialize(res.Text) as Dictionary<string, object>;
            object obj = 0;
            if (responseObject.TryGetValue("cancelled", out obj))
            {
                Util.Log("Request cancelled");
            }
            else if (responseObject.TryGetValue("request", out obj))
            {
                //AddPopupMessage("Request Sent", ChallengeDisplayTime);
                Debug.Log("Request sent!!!!!!!!!!!");
                Util.Log("Request sent");
            }
        }
    }
	public void Logout()
	{
		FB.LogOut ();
        UIloggedIn.SetActive(false);
        UInotLoggedIn.SetActive(false);
        btnlogin.SetActive(true);
        UIUsername.SetActive(false);
	}

	//when the back button of the nickname canvas is pressed this method will be called
	public void backbuttonPressed()
	{
		
//		NicknameCanv.SetActive (false);
		btnlogin.SetActive (true);

	}


	public void LoadSettings()
	{

		string url = "http://lrgame.azurewebsites.net/";


		
		//when the button is clicked        
		//setup url to the ASP.NET webpage that is going to be called
		string customUrl = url + "Gamer/Details";
		
		//setup a form
		WWWForm form = new WWWForm();
		
		//creates a random user
		form.AddField("FBuserID",FB.UserId);
		
		//Call the server
		WWW www = new WWW(customUrl, form);
		StartCoroutine(LoadWaitForRequest(www));
	}

	IEnumerator LoadWaitForRequest(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{    
			string [] settings=www.text.Split(',');
			//write data returned from ASP.NET
			Debug.Log(www.text);
			//creating an object of the settings controller
//			SettingsController sett=new SettingsController();

			//sett.setVol(float.Parse(settings[1]),bool.Parse( settings[0]));

		//	PlayerPrefs.GetFloat("volValue",float.Parse(settings[1]));

			Debug.Log(settings.Length+ www.text);
		}
		else
		{
			Debug.Log("WWW Error: " + www.error);
		}
	}



	public void CreateFBUser(string username)
	{
		string url = "http://lrgame.azurewebsites.net/";
		
		//when the button is clicked        
		//setup url to the ASP.NET webpage that is going to be called
		string customUrl = url + "Gamer/Create";
		
		//setup a form
		WWWForm form = new WWWForm();
		
		//creates a random user
		form.AddField("GamerUsername",username);
		form.AddField("FBuserID",FB.UserId);
		
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
			if(FB.UserId!=null)
			if (www.text.Contains("UserExist"))
			{
				LoadSettings();
				Debug.Log(www.text);
			}
			Debug.Log(www.text);
		} 
		else 
		{
			
			Debug.Log("WWW Error: "+ www.error);
		}


	}








/*	Dictionary<string, string> DeserializeJSONProfile(string response)
	{ 
		var responseObject = Json.Deserialize (response) as Dictionary<string ,object>;
		object nameH;
		var profile = new Dictionary<string,string> ();
		if (responseObject.TryGetValue ("first_name", out nameH)) {
			profile["first_name"] = (string)nameH;
		}
		return profile;
	} */


}
