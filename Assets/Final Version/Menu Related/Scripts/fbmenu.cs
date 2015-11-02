using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class fbmenu : MonoBehaviour {


	public GameObject UIUsername;
	private Dictionary<string, string> profile=null;

	void Start()
	{
		//UIUsername = GameObject.FindWithTag("untag"). GetComponent<GameObject> ();
	}
	void Awake()
	{
		if (FB.IsLoggedIn)
			StartCoroutine (getPic ());

		FB.API("/me?fields=id,first_name",Facebook.HttpMethod.GET , uncallback);
	}

	void uncallback(FBResult res)
	{
		if (res.Error != null) {
			Debug.Log ("Error with getting first name ");
			FB.API ("/me?fields=id,first_name", Facebook.HttpMethod.GET, dealsWithUserName);
			return;
		}
		
		profile = Util.DeserializeJSONProfile (res.Text);
		
		Text usermsg = UIUsername.GetComponent<Text> ();
//		Debug.Log (usermsg.text);
		//usermsg.text = profile ["first_name"];
		
		string fbusername = profile ["first_name"];

		CreateFBUser (fbusername);
		if(GameObject.Find ("txtusername")!=null)
			GameObject.Find ("txtusername").GetComponent<Text> ().text ="Hello , "+ fbusername;
	}

	Texture2D pictex = null;
	bool showPic = false;
	void OnGUI()
	{
		if (showPic)
		{
			GUI.DrawTexture(new Rect(900, 0, 128, 128), pictex);
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

    }

	public void invite()
	{
		FB.AppRequest ("Hey check out this new game called The Last Ranger ",
		               null,
		               null,
		               null,
		               200,
		               "",
		              "Invite your friends to play The Last Ranger",
		               null);
	}

	
	public void Logout()
	{
		FB.LogOut ();
		PlayerPrefs.DeleteAll ();
		Application.LoadLevel ("Login Select");

	}

		public void CreateFBUser(string username)
		{
		string url = "http://rangerdata.azurewebsites.net/";
			
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
					//LoadSettings();
					Debug.Log(www.text);
				}
				Debug.Log(www.text);
			} 
			else 
			{
				
				Debug.Log("WWW Error: "+ www.error);
			}


}
	}
