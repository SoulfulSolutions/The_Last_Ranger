using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class GameOverReaderr : MonoBehaviour {

    public TextAsset dictionary;//we assign xmlfie

    public string LanguageName;//will show current language
    public int CurrentLanguage;//will change the language

    string lblGameO;
    string lblScore;
    string btnRestart;
	string btnViewL;
    string btnShare;
    string btnQuit;



    public Text LGameOText;
    public Text LScoreText;
    public Text BRestartText;
    public Text BViewLText;

    public Text BShareText;
    public Text BQuitText;






    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    // above we create a list called languges


    Dictionary<string, string> obj;//we call this dictionary object

    void Awake()//will check the reader method before the game starts
    {
        Reader();
        CurrentLanguage = PlayerPrefs.GetInt("lang");
    }

    void Start()//taking the text in the gui and changing it
    {
        LGameOText  = LGameOText.GetComponent<Text>();
        LScoreText = LScoreText.GetComponent<Text>();
        BRestartText = BRestartText.GetComponent<Text>();
        BViewLText = BViewLText.GetComponent<Text>();
        BShareText = BShareText.GetComponent<Text>();
        BQuitText = BQuitText.GetComponent<Text>();
       
    }
    private void Update()//updates every frame
    {
        languages[CurrentLanguage].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers

        languages[CurrentLanguage].TryGetValue("lblGameO", out lblGameO);
        languages[CurrentLanguage].TryGetValue("lblScore", out lblScore);
        languages[CurrentLanguage].TryGetValue("btnRestart", out btnRestart);
		languages[CurrentLanguage].TryGetValue("btnViewL", out btnViewL);
        languages[CurrentLanguage].TryGetValue("btnShare", out btnShare);
        languages[CurrentLanguage].TryGetValue("btnQuit", out btnQuit);

 

    }

    void OnGUI()
    {
        LGameOText.text = lblGameO;
        LScoreText.text = lblScore;
        BRestartText.text = btnRestart;
		BViewLText.text = btnViewL;
        BShareText.text = btnShare;
        BQuitText.text = btnQuit;
       
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
                    if (value.Name == "lblGameO")
                    {
                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblScore")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnRestart")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

					if (value.Name == "btnViewL")
					{	
						obj.Add(value.Name, value.InnerText);
					}

                    if (value.Name == "btnShare")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnQuit")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                }
                languages.Add(obj);//Add dictionary to the languageslist
            }
        }
    }
}
