using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;

public class login : MonoBehaviour 
{
	private string url = "http://rangerdata.azurewebsites.net/";
	private InputField loginname;
	private InputField loginpassword;
	public static string username;
	public static string password;
	public Canvas Logincanvas;
	
	public GameObject responceHolder;
    public GameObject ErrorHolder;
	public Text txterror;
	public Text ResertUsernameText;
	public GameObject ResertPasswordPanel;
	string [] _usernames;
	//keeping the login username and password through out all scenes
	void Awake()
	{
		// DontDestroyOnLoad(gameObject); 
	}
	
	
	// Use this for initialization
	void Start()
	{
		PlayerPrefs.DeleteKey ("Load");
		LoadUsername ();
        ErrorHolder = GameObject.Find("ErrorHolder");
      //  ErrorHolder.SetActive(false);
		PlayerPrefs.DeleteKey("Load");
		loginname = GameObject.Find("txtUsername").GetComponent<InputField>();
		loginpassword = GameObject.Find("txtPassword").GetComponent<InputField>();
		username = loginname.text;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
			Debug.Log(username);
	}
	
	public void log_in()
	{
		if ((loginname.text == "") || (loginpassword.text == "")) {
			responceHolder.SetActive (true);
			txterror.text = "Please do not leave any blanks";
		} else if (checkUserName () == false) 
		{


				responceHolder.SetActive(true);
				txterror.text="Username does not exist!";

		}
			else 
		{

			WWWForm form = new WWWForm ();
		
			string customUrl = url + "CustomLogin/Details";
		
			form.AddField ("username", loginname.text);
			form.AddField ("password", loginpassword.text);
		
			WWW www = new WWW (customUrl, form);
			StartCoroutine (WaitForRequestLogin (www));
		}
	}


	
	
	IEnumerator WaitForRequestLogin(WWW www)
	{
		string[] loginDetails;
		string responcepassword;
		yield return www;
		// DontDestroyOnLoad(gameObject);
		// check for errors
		if (www.error == null)
		{
			Debug.Log(www.text);
			loginDetails = www.text.Split(',');
			responcepassword = decyption(loginDetails[1]);
			
			if (loginpassword.text == responcepassword)
			{
				PlayerPrefs.SetString("un", loginname.text);
				
				//write data returned from ASP.NET
				Logincanvas.enabled = false;
				Application.LoadLevel(3);
				Debug.Log(www.text);
			}
            else if (loginpassword.text != responcepassword)
            {
                responceHolder.SetActive(true);
				txterror.text="Password incorrect";

            }

			
            Debug.Log(www.text);
          }
			else
			{
			Debug.Log("WWW Error: " + www.error);
			responceHolder.SetActive(true);
			txterror.text="Error Connecting. Check your network!";
			}
    
	}

	public void ShowResertTextBox()
	{
		ResertPasswordPanel.SetActive(true);
	}
	public void checkUsername()
	{
		string username = ResertUsernameText.text;
		bool value = false;
		for (int n = 0; n < _usernames.Length; n++)
		{
			if ((_usernames[n].ToLower()).Equals(username.ToLower()))
				value = true;
		}
		if (ResertUsernameText.text == "") 
		{
			responceHolder.SetActive(true);
			txterror.text="Please do not leave any Blanks";
		}
		else if (value == true) 
		{
			PlayerPrefs.SetString ("ResertPass", ResertUsernameText.text);
			Application.LoadLevel ("Resert Password");
		} 
		else
		{
			ResertPasswordPanel.SetActive(false);
			responceHolder.SetActive(true);
			txterror.text="Username "+username+" does not exist!";
		}


	}
	public void LoadUsername()
	{
		WWWForm form = new WWWForm();
		string customUrl = url + "CustomLogin/AllUsernames";
		
		form.AddField("Username", " ");
		WWW www = new WWW(customUrl, form);
		StartCoroutine(WaitForRequestLoadUsernames(www));
	}
	
	IEnumerator WaitForRequestLoadUsernames(WWW www)
	{
		yield return www;
		// check for errors
		if (www.error == null)
		{
			_usernames = www.text.Split(',');
			Debug.Log(www.text);
		}
		else
		{
			Debug.Log("WWW Error: " + www.error);
			responceHolder.SetActive(true);
			txterror.text="Error Connecting. Check your network!";
		}
	}

	bool checkUserName()
	{


		bool value = false;
		//if(_usernames!=null)
		for (int n = 0; n < _usernames.Length; n++)
		{
			if ((_usernames[n].ToLower()).Equals(loginname.text.ToLower()))
				value = true;
		}
		
		return value;
	}


    public void buttonOk()
    {
		responceHolder.SetActive(false);

		}


    public void PasswordOkButton()
    {
        responceHolder.SetActive(false);

	}
	

	public string decyption(string pw)
	{
		string encyptionkey = "soulfulkey";
		byte[] encrBytes = Convert.FromBase64String(pw);
		
		using (Aes encyptor = Aes.Create())
		{
			Rfc2898DeriveBytes pdB = new Rfc2898DeriveBytes(encyptionkey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
			encyptor.Key = pdB.GetBytes(32);
			encyptor.IV = pdB.GetBytes(16);
			
			
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, encyptor.CreateDecryptor(), CryptoStreamMode.Write))
				{
					cs.Write(encrBytes, 0, encrBytes.Length);
					cs.Close();
				}
				pw = Encoding.Unicode.GetString(ms.ToArray());
			}
		}
		return pw;
	}
	
}
