using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;
using UnityEngine.UI;

public class loginReader : MonoBehaviour {

    public TextAsset dictionary;//we assign xmlfie

    private string LanguageName;//will show current language
    private int CurrentLanguage =0;//will change the language

    string btnFb;
    string btnCus;

    public Text fblogin;
    public Text custlogin;
     
	public Toggle chkEnglish;
	public Toggle chkIsizulu;


    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    // above we create a list called languges


    Dictionary<string, string> obj=new Dictionary<string, string>();//we call this dictionary object

    void Awake()//will check the reader method before the game starts
    {
        Reader();
		PlayerPrefs.DeleteKey ("lang");
		chkEnglish.isOn = true;
    }

    void Start()
    {
        fblogin = fblogin.GetComponent<Text>();
        custlogin = custlogin.GetComponent<Text>();
	
    }
    private void Update()//updates every frame
    {
//		Debug.Log ("Curr : "+CurrentLanguage.ToString ());
        languages[CurrentLanguage].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers
        languages[CurrentLanguage].TryGetValue("btnfb", out btnFb);
        languages[CurrentLanguage].TryGetValue("btnCus", out btnCus);

		 fblogin.text = btnFb;
		custlogin.text = btnCus;

		PlayerPrefs.SetInt ("lang", CurrentLanguage);
    }

	public void setLangEnglish()
	{
    	if (chkEnglish.isOn) 
		{
			CurrentLanguage = 0;
			chkIsizulu.isOn = false;
		} 
		else 
		{
			chkEnglish.isOn = false;
			chkIsizulu.isOn = true;
			CurrentLanguage = 1;
		}
	}

	public void setIsizulu()
	{
		CurrentLanguage = 1;
		Debug.Log (" new curr lang : " + CurrentLanguage);

		if (chkIsizulu.isOn) 
		{
			CurrentLanguage = 1;
			chkEnglish.isOn = false;
		} 
		else 
		{
			chkIsizulu.isOn = false;
			chkEnglish.isOn = true;
			CurrentLanguage = 0;
		}
	}

//    void OnGUI()
//    {
//     
//    }

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
                    if (value.Name == "btnfb")
                    {
                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "btnCus")
                    {
                        Debug.Log("customer if "+value.InnerText);
                        obj.Add(value.Name, value.InnerText);
                    }
                }
                languages.Add(obj);//Add dictionary to the languageslist
            }
        }
    }

}
