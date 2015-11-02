using UnityEngine;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class ResetReader : MonoBehaviour {

	public TextAsset dictionary;//we assign xmlfie
	
	public string LanguageName;//will show current language
	public int CurrentLanguage;//will change the language
	
	string lblResP;
	string lblPles;
	string lblPass;
	string txtPass;
	string lblCPass;
	string txtCPass;
	string btnResP;

	
	
	
	public Text LResPText;
	public Text LPleasText;
	public Text LPassText;
	public Text TPassText;
	public Text LCPassText;
	public Text TCPassText;
	public Text BResPText;

	
	
	
	
	
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
		LResPText = LResPText.GetComponent<Text>();
		LPleasText = LPleasText.GetComponent<Text>();
		LPassText = LPassText.GetComponent<Text>();
		TPassText = TPassText.GetComponent<Text>();
		LCPassText = LCPassText.GetComponent<Text>();
	    TCPassText = TCPassText.GetComponent<Text>();
		BResPText = BResPText.GetComponent<Text>();
	}
	private void Update()//updates every frame
	{
		languages[CurrentLanguage].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers
		
		languages[CurrentLanguage].TryGetValue("lblResP", out lblResP);
		languages[CurrentLanguage].TryGetValue("lblPles", out lblPles);
		languages[CurrentLanguage].TryGetValue("lblPass", out lblPass);
		languages[CurrentLanguage].TryGetValue("txtPass", out txtPass);
		languages[CurrentLanguage].TryGetValue("lblCPass", out lblCPass);
		languages[CurrentLanguage].TryGetValue("txtCPass", out txtCPass);
		languages[CurrentLanguage].TryGetValue("btnResP", out btnResP);

		
		
		
		
		
		
	}
	
	void OnGUI()
	{
		LResPText.text = lblResP;
		LPleasText.text = lblPles;
		LPassText.text = lblPass;
		TPassText.text = txtPass;
		LCPassText.text = lblCPass;
	    TCPassText.text = txtCPass;

		BResPText.text = btnResP;
	
		
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
					if (value.Name == "lblResP")
					{
						obj.Add(value.Name, value.InnerText);
					}
					
					if (value.Name == "lblPles")
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblPass")
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "txtPass")
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblCPass")
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "txtCPass")
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnResP")
					{
						
						obj.Add(value.Name, value.InnerText);
					}

					
				}
				languages.Add(obj);//Add dictionary to the languageslist
			}
		}
	}
}
