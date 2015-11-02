using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;
using UnityEngine.UI;

public class RegisterReader2 : MonoBehaviour {

	public TextAsset dictionary;//we assign xmlfie
	
	public string LanguageName;//will show current language
	public int CurrentLanguage;//will change the language
	
	string lblReg;
	string lblUser;
	string txtUser;
	string lblPassw;
	string txtPassw;
	string lblPassR;
	string txtSel;
	string lblAns;
	string txtEnt;
	string btnCreate;
	string btnBack;
	string txtq1;
	string txtq2;
	string txtq3;
	string txtq4;
	string txtq5;
	
	
	
	
	
	
	public Text LRegText;
	
	public Text LUserText;
	
	public Text TUserText;
	
	public Text LPasswText;
	
	public Text TPasswText;
	
	public Text LPassRText;
	
	public Text TSelText;
	
	public Text LAnsText;
	
	public Text TEntText;
	
	public Text BCreatText;
	
	public Text BBackText;
	
	public Text TQ1Text;
	
	public Text TQ2Text;
	
	public Text TQ3Text;
	
	public Text TQ4Text;
	
	public Text TQ5Text;


	
	
	
	
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
		LRegText = LRegText.GetComponent<Text>();
		
		LUserText = LUserText.GetComponent<Text>();
		
		TUserText = TUserText.GetComponent<Text>();
		
		LPasswText = LPasswText.GetComponent<Text>();
		
		TPasswText = TPasswText.GetComponent<Text>();
		
		LPassRText = LPassRText.GetComponent<Text> ();
		
		TSelText = TSelText.GetComponent<Text>();
		
		LAnsText = LAnsText.GetComponent<Text>();
		
		TEntText = TEntText.GetComponent<Text> ();
		
		BCreatText = BCreatText.GetComponent<Text>();
		
		BBackText = BBackText.GetComponent<Text> ();
		
		TQ1Text = TQ1Text.GetComponent<Text> ();
		
		TQ2Text = TQ2Text.GetComponent<Text> ();
		
		TQ3Text = TQ3Text.GetComponent<Text> ();
		
		TQ4Text = TQ4Text.GetComponent<Text> ();
		
		TQ5Text = TQ5Text.GetComponent<Text> ();
	}
	private void Update()//updates every frame
	{

		Debug.Log ("Customer : "+languages.Count);
		languages[CurrentLanguage].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers
		
		languages[CurrentLanguage].TryGetValue("lblReg", out lblReg);

		languages[CurrentLanguage].TryGetValue("lblUser", out lblUser);

		languages[CurrentLanguage].TryGetValue("txtUser", out txtUser);

		languages[CurrentLanguage].TryGetValue("lblPassw", out lblPassw);

		languages[CurrentLanguage].TryGetValue("txtPassw", out txtPassw);

		languages[CurrentLanguage].TryGetValue("lblPassR", out lblPassR);
		
		languages[CurrentLanguage].TryGetValue("txtSel", out txtSel);

		languages[CurrentLanguage].TryGetValue("lblAns", out lblAns);
		
		languages[CurrentLanguage].TryGetValue("txtEnt", out txtEnt);

		languages[CurrentLanguage].TryGetValue("btnCreate", out btnCreate);

		languages[CurrentLanguage].TryGetValue("btnBack", out btnBack);

		languages[CurrentLanguage].TryGetValue("txtq1", out txtq1);

		languages[CurrentLanguage].TryGetValue("txtq2", out txtq2);

		languages[CurrentLanguage].TryGetValue("txtq3", out txtq3);

		languages[CurrentLanguage].TryGetValue("txtq4", out txtq4);

		languages[CurrentLanguage].TryGetValue("txtq5", out txtq5);
		
		
		
		
		
	}
	
	void OnGUI()
	{
		LRegText.text = lblReg;
		LUserText.text = lblUser;
		TUserText.text = txtUser;
		LPasswText.text = lblPassw;
		TPasswText.text = txtPassw;
		LPassRText.text = lblPassR;

		LAnsText.text = lblAns;
		TEntText.text = txtEnt;
		BCreatText.text = btnCreate;
		BBackText.text = btnBack;
		TQ1Text.text = txtq1;
		TQ2Text.text = txtq2;
		TQ3Text.text = txtq3;
		TQ4Text.text = txtq4;
		TQ5Text.text = txtq5;

	
	}
	
	
	
	void Reader()
	{
		XmlDocument xmldoc = new XmlDocument ();// we make an xmldocument
		{
			xmldoc.LoadXml (dictionary.text);//attach dicctionary text to xml document
			
			XmlNodeList languagesList = xmldoc.GetElementsByTagName ("language");
			//make list that will check every node that is called language
			
			foreach (XmlNode languagevalueNode in languagesList) {//check every xmlnode language value in languages list
				XmlNodeList languageContent = languagevalueNode.ChildNodes;//every language will have its own content eg button
				obj = new Dictionary<string, string> ();
				
				foreach (XmlNode value in languageContent) {//check every single value in value list for current language
					if (value.Name == "Name") {
						obj.Add (value.Name, value.InnerText);//check if the name is the same as the one in the xml file and add it
						
					}
					if (value.Name == "lblReg") {
						obj.Add (value.Name, value.InnerText);
					}
					
					if (value.Name == "lblUser") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtUser") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "lblPassw") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtPassw") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "lblPassR") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtSel") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "lblAns") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtEnt") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "btnCreate") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "btnBack") {
						
						obj.Add (value.Name, value.InnerText);
					}

					if (value.Name == "txtq1") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtq2") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtq3") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtq4") {
						
						obj.Add (value.Name, value.InnerText);
					}
					if (value.Name == "txtq5") {
						
						obj.Add (value.Name, value.InnerText);
					}
					
					
					
					
					
				}
				languages.Add (obj);//Add dictionary to the languageslist
			}
		}
	}
}

