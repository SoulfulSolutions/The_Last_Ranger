using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System;
public class Register : MonoBehaviour
{


    private InputField Regname;
    private static InputField Regpassword;
    private InputField Answer;
    private bool button = false;

    GameObject ErrorHolder;
    public Text btnok;

    public Canvas Regcanvas;

    public GameObject q1;
    public GameObject q2;
    public GameObject q3;
    public GameObject q4;
    public GameObject q5;

    public Text btnselect;
    public GameObject txtanswer;
	public GameObject regPanel;
    public GameObject responceHolder;
    public Text errortext;
    string url;
    string selectedQuestion = "";

	public GameObject SuccessOject;
	public Text successText;
    string[] _usernames;
    //keeping the login username and password through out all scenes
    void Awake()
    {

        DontDestroyOnLoad(gameObject); 
    }

    void Start()
    {
		if (PlayerPrefs.GetInt ("lang") == 1)
		{
			btnselect.text = "Khetha umbuzo";
		} 
		else 
		{	
		}	
	

		url = "http://rangerdata.azurewebsites.net/";
        ErrorHolder = GameObject.Find("ErrorHolder");
//        ErrorHolder.SetActive(false);
        Regname = GameObject.Find("txtUsername").GetComponent<InputField>();
        Regpassword = GameObject.Find("txtPassword").GetComponent<InputField>();
        Answer = GameObject.Find("txtanswer").GetComponent<InputField>();
        button = GameObject.Find("Button").GetComponent<Button>();

        LoadUsername();



    }

    public void LoadUsername()
    {
        WWWForm form = new WWWForm();
        string customUrl = url + "CustomLogin/AllUsernames";

        form.AddField("Username", " ");
        WWW www = new WWW(customUrl, form);
        StartCoroutine(WaitForRequestLoadUsernames(www));
    }
	public void backtologin()
	{gameObject.SetActive (false);
		Application.LoadLevel ("Custom Login");
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
        }
    }


    bool checkUserName()
    {
        bool value = false;
        for (int n = 0; n < _usernames.Length; n++)
        {
            if ((_usernames[n].ToLower()).Equals(Regname.text.ToLower()))
                value = true;
        }

        return value;
    }

    public void checkUsernameError()
    {

        responceHolder.SetActive(false);
    }
	public void BTNOK()
	{
		responceHolder.SetActive(false);
	}
	public void BTNSuccess()
	{
		SuccessOject.SetActive (false);
		gameObject.SetActive (false);
		Application.LoadLevel(4);
	}

    public void register()
    {
        Debug.Log("register is called");

        if ((Regname.text == "") || (Regpassword.text == "") || (Answer.text == "") || (button == false) || selectedQuestion == "") {
			errortext.text = "Do not leave any blanks";
			responceHolder.SetActive(true);
			// UnityEditor.EditorUtility.DisplayDialog("This fiels cannot be left empty", "Please enter username or password!!!", "ok");
			Debug.Log ("Enter username or password");


		} 
		else if (Regpassword.text.Length < 5) 
		{
			errortext.text = "Password must not be less than 5 letters";
			responceHolder.SetActive(true);
		}
        else if (checkUserName())
        {
            errortext.text = "Username " + Regname.text + " is already taken";
            responceHolder.SetActive(true);
          //  yield return new WaitForSeconds(2);

        }

        else
        {
            Debug.Log("its here");
            string password = encrypt(Regpassword.text);
            string answer = encrypt(Answer.text);

            WWWForm form = new WWWForm();
            string customUrl = url + "CustomLogin/Create";

            form.AddField("Username", Regname.text);
            form.AddField("password", password);
            form.AddField("question", selectedQuestion);
            form.AddField("answer", answer);
            WWW www = new WWW(customUrl, form);
            StartCoroutine(WaitForRequestRegister(www));
        }

        Debug.Log("Your password is : " + Regpassword.text);
        Debug.Log("Your encypted password is : " + encrypt(Regpassword.text));
        Debug.Log("Your decypted password is : " + decyption(encrypt(Regpassword.text)));
    }


  
    IEnumerator WaitForRequestRegister(WWW www)
    {
        yield return www;
        DontDestroyOnLoad(gameObject);

        // check for errors
        if (www.error == null)
        {      //write data returned from ASP.NET
            //   Regcanvas.enabled = false;
            //  
		
            successText.text = www.text;
            SuccessOject.SetActive(true);
			//regPanel.SetActive(false);

            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
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

    public void select()
    {
        txtanswer.SetActive(false);
        button = true;
    }
    //select one button at a time and placing it in the first button
    public void pressed_school()
    {
        q1.SetActive(false);
		if (PlayerPrefs.GetInt("lang") == 0)
        btnselect.text = "What is the name of your 1st school?";
		else
			btnselect.text = "lithini igama lesikole sakho sokugcina";
		selectedQuestion = btnselect.text;
        txtanswer.SetActive(true);
    }

    public void pressed_maiden()
    {
        q1.SetActive(true);
        q2.SetActive(false);
       // btnselect.text = "What is your mother's maiden name?";
		if (PlayerPrefs.GetInt("lang") == 0)
			btnselect.text = "What is your mother's maiden name?";
		else
			btnselect.text = "sithini isibongo sikanina";
        selectedQuestion = btnselect.text;
        txtanswer.SetActive(true);
    }

    public void pressed_born()
    {
        q1.SetActive(true);
        q2.SetActive(true);
        q3.SetActive(false);
		if (PlayerPrefs.GetInt("lang") == 0)
			btnselect.text = "In which city were you born?";
		else
			btnselect.text = "Wazalelwa kwelipi idolobha";
		selectedQuestion = btnselect.text;
        txtanswer.SetActive(true);
    }

    public void pressed_colour()
    {
        q1.SetActive(true);
        q2.SetActive(true);
        q3.SetActive(true);
        q4.SetActive(false);

		if (PlayerPrefs.GetInt("lang") == 0)
			btnselect.text = "What is your favourite colour?";
		else
			btnselect.text = "Uthanda muphi umbala?";
        selectedQuestion = btnselect.text;
        txtanswer.SetActive(true);
    }

    public void pressed_car()
    {
        q1.SetActive(true);
        q2.SetActive(true);
        q3.SetActive(true);
        q4.SetActive(true);
        q5.SetActive(false);

		if (PlayerPrefs.GetInt("lang") == 0)
			btnselect.text = "What is your dream car?";
		else
			btnselect.text = "Iyiphi imoto yakho yamaphupho?";
        selectedQuestion = btnselect.text;
        txtanswer.SetActive(true);
    }

    //ok button from the error message label

    public void buttonOk()
    {
        ErrorHolder.SetActive(false);

    }

    public void UsernameButton()
    {
        responceHolder.SetActive(false);
        Regname.text ="";
       // btnselect.text = "";
        Answer .text="";
       
}
}

    