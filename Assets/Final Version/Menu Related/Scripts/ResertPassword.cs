using UnityEngine;
using System.Collections;
using UnityEngine.UI ;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;


public class ResertPassword : MonoBehaviour {

	private string url = "http://rangerdata.azurewebsites.net/";
	public Text lblQuestion;
	public Text txtAnswer;
	public InputField txtNewPass;
	public InputField txtConfrimPass;

	public GameObject ResponceHolder;
	public Text ResponceText;


	public GameObject PasswordResponce;
	public Text passwordResponceText;
	string [] _loginDetails;

	// Use this for initialization
	void Start () 
	{
		LoadDetails ();

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void passwordChangedOK()
	{
		Application.LoadLevel ("Custom Login");
	}
	void LoadDetails()
	{
		WWWForm form = new WWWForm ();
		
		string customUrl = url + "CustomLogin/ResertPassword";
		
		form.AddField ("Username", PlayerPrefs.GetString("ResertPass"));
		
		WWW www = new WWW (customUrl, form);
		StartCoroutine (WaitForRequestLoadDetails (www));
	}

	IEnumerator WaitForRequestLoadDetails(WWW www)
	{
		yield return www;
	
		if (www.error == null)
		{
			_loginDetails=www.text.Split(',');
			lblQuestion.text = _loginDetails [1];
			Debug.Log(www.text);
			Debug.Log(decyption(_loginDetails[2]));
		}
		else
		{
			Debug.Log("WWW Error: " + www.error);
		}
		
	}

	bool checkUserName()
	{
		bool value = false;

		if ((decyption(_loginDetails[2]).ToLower()).Equals(txtAnswer.text.ToLower()))
				value = true;
		return value;
	}

	public void ResertPass()
	{
		if ((txtAnswer.text == "") || (txtNewPass.text == "")||(txtConfrimPass.text == "")) 
		{
			ResponceHolder.SetActive (true);
			ResponceText.text = "Please do not leave any blanks";
		}
		else if (checkUserName () == false) 
		{
			ResponceHolder.SetActive(true);
			ResponceText.text="Answer Is defferent from the one was submited";
		}
		else if(txtNewPass.text!=txtConfrimPass.text)
		{
			Debug.Log(txtNewPass.text+"  "+txtConfrimPass.text);
			ResponceHolder.SetActive(true);
			ResponceText.text="Password And Confirmation password does not match";
		}

		else if (txtNewPass.text.Length < 5) 
		{
			txtNewPass.text="";
			txtConfrimPass.text="";
			ResponceHolder.SetActive(true);
			ResponceText.text="Password must not be less than 5 letters";
			
		}


		else 
		{
			string password=encrypt(txtNewPass.text);
			
			WWWForm form = new WWWForm ();
			
			string customUrl = url + "CustomLogin/Edit";
			
			form.AddField ("Username", PlayerPrefs.GetString("ResertPass"));
			form.AddField ("Password",password );
			
			WWW www = new WWW (customUrl, form);
			StartCoroutine (WaitForRequestResert (www));
		}		
	}
	

	IEnumerator WaitForRequestResert(WWW www)
	{
		yield return www;

		if (www.error == null)
		{
			PasswordResponce.SetActive(true);
			passwordResponceText.text=www.text;
		}
		else
		{
			Debug.Log("WWW Error: " + www.error);
		}
		
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


	public string encrypt(string pw)
	{
		string encyptionkey = "soulfulkey";
		byte[] clearBytes = Encoding.Unicode.GetBytes(pw);
		
		using (Aes encyptor = Aes.Create())
		{
			Rfc2898DeriveBytes pdf = new Rfc2898DeriveBytes(encyptionkey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
			encyptor.Key = pdf.GetBytes(32);
			encyptor.IV = pdf.GetBytes(16);
			
			
			using (MemoryStream ms = new MemoryStream())
			{
				using (CryptoStream cs = new CryptoStream(ms, encyptor.CreateEncryptor(), CryptoStreamMode.Write))
				{
					cs.Write(clearBytes, 0, clearBytes.Length);
					cs.Close();
				}
				pw = Convert.ToBase64String(ms.ToArray());
			}
		}
		return pw;
		
	}



}
