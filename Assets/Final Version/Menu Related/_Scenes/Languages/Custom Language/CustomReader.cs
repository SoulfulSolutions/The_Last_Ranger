using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using UnityEngine.UI;

public class CustomReader : MonoBehaviour {

    public TextAsset dictionary;//we assign xmlfie

    public string LanguageName;//will show current language
    public int CurrentLanguage;//will change the language

    string lblCustlog;
    string lblUsername;
    string lblPassword;
    string txtUserName;
    string txtPassword;
	string btnLogin;
    string lblAcco;
    string btnCreate;
	string btnReset;


    public Text LcustloginText;
    public Text LUsernameText;
    public Text LPasswordText;
    public Text txUsernameText;
    public Text TxPasswordsText;
    public Text BLoginText;
    public Text LbAccoText;
    public Text BCreateText;
	public Text btnresetText;
	
    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    // above we create a list called languges
    Dictionary<string, string> obj;//we call this dictionary object

    void Awake()//will check the reader method before the game starts
    {
        Reader();
		CurrentLanguage = PlayerPrefs.GetInt ("lang");
    }

    void Start()//taking the text in the gui and changing it
    {
        LcustloginText = LcustloginText.GetComponent<Text>();
       LUsernameText = LUsernameText.GetComponent<Text>();
       LPasswordText = LPasswordText.GetComponent<Text>();
       txUsernameText = txUsernameText.GetComponent<Text>();
       TxPasswordsText = TxPasswordsText.GetComponent<Text>();
       BLoginText = BLoginText.GetComponent<Text>();
       LbAccoText = LbAccoText.GetComponent<Text>();
       BCreateText = BCreateText.GetComponent<Text>();
		btnresetText = btnresetText.GetComponent<Text> ();
    }
    private void Update()//updates every frame
    {

		//	Debug.Log ("Customer : "+languages.Count);
        languages[CurrentLanguage].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers

        languages[CurrentLanguage].TryGetValue("lblCustlog", out lblCustlog);
        languages[CurrentLanguage].TryGetValue("lblUsername", out lblUsername);
        languages[CurrentLanguage].TryGetValue("lblPassword", out lblPassword);
        languages[CurrentLanguage].TryGetValue("txtUsername", out txtUserName);
        languages[CurrentLanguage].TryGetValue("txtPassword", out txtPassword);
		languages[CurrentLanguage].TryGetValue("btnLogin", out btnLogin);

        languages[CurrentLanguage].TryGetValue("lblAcco", out lblAcco);
        languages[CurrentLanguage].TryGetValue("btnCreate", out btnCreate);
		languages[CurrentLanguage].TryGetValue("btnReset", out btnReset);





    }

    void OnGUI()
    {
        LcustloginText.text = lblCustlog;
        LUsernameText.text = lblUsername;
        LPasswordText.text = lblPassword;
        txUsernameText.text = txtUserName;
        TxPasswordsText.text = txtPassword;
        BLoginText.text = btnLogin;
        LbAccoText.text = lblAcco;
        BCreateText.text = btnCreate;
		btnresetText.text = btnReset;

    }



    void Reader()
    {
        XmlDocument xmldoc = new XmlDocument();// we make an xmldocument
        {
            xmldoc.LoadXml(dictionary.text);//attach dicctionary text to xml document

            XmlNodeList languagesList = xmldoc.GetElementsByTagName("language");
            //make list that will check every node that is called language

            foreach (XmlNode languagevalueNode in languagesList)//check every xmlnode language value in languages list
            {
                XmlNodeList languageContent = languagevalueNode.ChildNodes;//every language will have its own content eg button
                obj = new Dictionary<string, string>();

                foreach (XmlNode value in languageContent)//check every single value in value list for current language
                {
                    if (value.Name == "Name")
                    {
                        obj.Add(value.Name, value.InnerText);//check if the name is the same as the one in the xml file and add it

                    }
                    if (value.Name == "lblCustlog")
                    {
                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblUsername")
                    {
                        
                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "lblPassword")
                    {
                        
                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "txtUsername")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "txtPassword")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
					if (value.Name == "btnLogin")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "lblAcco")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnCreate")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

					if (value.Name == "btnReset")
					{
						obj.Add(value.Name, value.InnerText);
					}

                }
                languages.Add(obj);//Add dictionary to the languageslist
            }
        }
    }
}
