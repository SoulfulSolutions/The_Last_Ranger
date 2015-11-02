using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using UnityEngine.UI;
using System.IO;

public class StoryReader : MonoBehaviour {

    public TextAsset dictionary;//we assign xmlfie

    public string LanguageName;//will show current language
    public int CurrentLanguage;//will change the language

    string lblStory1;
    string lblStory2;
    string btnSkip;
   



    public Text LStor1Text;
    public Text LStor2Text;
    public Text BSkipText;
    





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
        LStor1Text = LStor1Text.GetComponent<Text>();
        LStor2Text = LStor2Text.GetComponent<Text>();
        BSkipText = BSkipText.GetComponent<Text>();
       
    }
    private void Update()//updates every frame
    {
        languages[CurrentLanguage].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers

        languages[CurrentLanguage].TryGetValue("lblStory1", out lblStory1);
        languages[CurrentLanguage].TryGetValue("lblStory2", out lblStory2);
        languages[CurrentLanguage].TryGetValue("btnSkip", out btnSkip);
        






    }

    void OnGUI()
    {
        LStor1Text.text = lblStory1;
        LStor2Text.text = lblStory2;
        BSkipText.text = btnSkip;
        
        


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
                    if (value.Name == "lblStory1")
                    {
                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblStory2")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnSkip")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                  


                }
                languages.Add(obj);//Add dictionary to the languageslist
            }
        }
    }
}
