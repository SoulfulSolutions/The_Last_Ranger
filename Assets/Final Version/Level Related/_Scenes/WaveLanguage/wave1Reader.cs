using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using UnityEngine.UI;
using System.IO;


public class wave1Reader : MonoBehaviour {

	public TextAsset dictionary;//we assign xmlfie
	
	public string LanguageName;//will show current language
	public int CurrentLanguage;//will change the language
	
	string lblWaveObj;//1
	string lblWaveOText;//2
	string btnNext;//3
	string lblWaveObj2;//4
	string lblWaveOText2;//5
	string btnClose;//6

	string lblPause;//9
	string btnResume;//10
	string btnRestart;//11
	string btnSaveG;//12
	string btnLoadG;//13
	string btnQuitG;//14


	string lblQuitText;//15
	string btnYes;//16
	string btnNo;//17

	string lblStatHead;
	string lblWaveScore;
	string lblSavedAnimals;
	string lblEnemiesKilled;
	string lblAnimalsKilled;
	string btnProceed;
			
	public Text LWaveObjText;//1
	public Text LWaveOTextText;//2
	public Text BNextText;//3
	public Text LWaveObj2Text;//4
	public Text LWaveOTextText2;//5
	public Text BCloseText;//6

	public Text lblPauseText;//9
	public Text BResumeText;//10
	public Text BRestartText;//11
	public Text BSaveGText;//12
	public Text BLoadGText;//13
	public Text BQuitGText;//14

	public Text LQuitTextText;//15
	public Text BYesText;//16
	public Text BNoText;//17

	public Text LStatHeadText;
	public Text LWaveScoreText;
	public Text LNoSavedAnimalsText;
	public Text LNoEnemKilledText;
	public Text LNoAnimalsKilledText;
	public Text BProceedText;
	
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

		LWaveObjText = LWaveObjText.GetComponent<Text>();//1
		LWaveOTextText = LWaveOTextText.GetComponent<Text>();//2
		BNextText = BNextText.GetComponent<Text>();//3
		LWaveObj2Text = LWaveObj2Text.GetComponent<Text>();//4
		LWaveOTextText2 = LWaveOTextText2.GetComponent<Text> ();//5

		BCloseText = BCloseText.GetComponent<Text> ();//6
			
		lblPauseText = lblPauseText.GetComponent<Text> ();//9

		BResumeText = BResumeText.GetComponent < Text> ();//10

		BRestartText = BRestartText.GetComponent<Text> ();//11
		BSaveGText = BSaveGText.GetComponent<Text> ();//12
		BLoadGText = BLoadGText.GetComponent<Text> ();//13
		BQuitGText = BQuitGText.GetComponent<Text> ();//14

		
		LQuitTextText = LQuitTextText.GetComponent<Text> ();//15
		BYesText = BYesText.GetComponent<Text> ();//16
		BNoText = BNoText.GetComponent<Text> ();//17

		LStatHeadText = LStatHeadText.GetComponent<Text> ();
		LWaveScoreText = LWaveScoreText.GetComponent<Text> ();
		LNoSavedAnimalsText = LNoSavedAnimalsText.GetComponent<Text> ();
		LNoEnemKilledText = LNoEnemKilledText.GetComponent<Text> ();
		LNoAnimalsKilledText = LNoAnimalsKilledText.GetComponent<Text> ();
		BProceedText = BProceedText.GetComponent<Text> ();
	}
	private void Update()//updates every frame
	{
		//	Debug.Log ("Customer : "+languages.Count);
		languages[0].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers
		
		languages[CurrentLanguage].TryGetValue("lblWaveObj", out lblWaveObj);//1
		languages[CurrentLanguage].TryGetValue("lblWaveOText", out lblWaveOText);//2
		languages[CurrentLanguage].TryGetValue("btnNext", out btnNext);//3
		languages[CurrentLanguage].TryGetValue("lblWaveObj2", out lblWaveObj2);//4
		
		languages[CurrentLanguage].TryGetValue("lblWaveOText2", out lblWaveOText2);//5
		languages[CurrentLanguage].TryGetValue("btnClose", out btnClose);//6
      
		languages[CurrentLanguage].TryGetValue("lblPause", out lblPause);//9
		languages[CurrentLanguage].TryGetValue("btnResume", out btnResume);//10
		languages[CurrentLanguage].TryGetValue("btnRestart", out btnRestart);//11
		languages[CurrentLanguage].TryGetValue("btnSaveG", out btnSaveG);//12
		languages[CurrentLanguage].TryGetValue("btnLoadG", out btnLoadG);//13
		languages[CurrentLanguage].TryGetValue("btnQuitG", out btnQuitG);//14

		languages[CurrentLanguage].TryGetValue("lblQuitText", out lblQuitText);//15
		languages[CurrentLanguage].TryGetValue("btnYes", out btnYes);//16
		languages[CurrentLanguage].TryGetValue("btnNo", out btnNo);//17

		languages[CurrentLanguage].TryGetValue("lblStatHead", out lblStatHead);//17
		languages[CurrentLanguage].TryGetValue("lblWaveScore", out lblWaveScore);//17
		languages[CurrentLanguage].TryGetValue("lblSavedAnimals", out lblSavedAnimals);//17
		languages[CurrentLanguage].TryGetValue("lblEnemiesKilled", out lblEnemiesKilled);//17
		languages[CurrentLanguage].TryGetValue("lblAnimalsKilled", out lblAnimalsKilled);//17
		languages[CurrentLanguage].TryGetValue("btnProceed", out btnProceed);//17

	}
	
	void OnGUI()
	{
		LWaveObjText.text = lblWaveObj;//1
		LWaveOTextText.text = lblWaveOText;//2
		BNextText.text = btnNext;//3
		LWaveObj2Text.text = lblWaveObj2;//4
		LWaveOTextText2.text =lblWaveOText2;//5
		BCloseText.text = btnClose;//6

		lblPauseText.text = lblPause;//9
		BResumeText.text = btnResume;//10
		BRestartText.text = btnRestart;//11
		BSaveGText.text = btnSaveG;//12
		BLoadGText.text = btnLoadG;//13
		BQuitGText.text = btnQuitG;//14

		LQuitTextText.text = lblQuitText;//15
		BYesText.text = btnYes;//16
		BNoText.text = btnNo;//17

		LStatHeadText.text = lblStatHead;
		LWaveScoreText.text = lblWaveScore;
		LNoSavedAnimalsText.text = lblSavedAnimals;
		LNoEnemKilledText.text = lblEnemiesKilled;
		LNoAnimalsKilledText.text = lblAnimalsKilled;
		BProceedText.text = btnProceed;
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
					if (value.Name == "lblWaveObj")//1
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblWaveOText")//2
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnNext" )//3
					{
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblWaveObj2" )//4
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblWaveOText2")//5
					{
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnClose")//6
					{
						
						obj.Add(value.Name, value.InnerText);
					}

					if (value.Name == "lblPause")//9
					{
						
						obj.Add(value.Name, value.InnerText);
					}

					if (value.Name == "btnResume")//10
					{
						
						obj.Add(value.Name, value.InnerText);
					}

					if (value.Name == "btnRestart")//11
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnSaveG")//12
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnLoadG")//13
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnQuitG")//14
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblQuitText")//15
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnYes")//16
					{
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnNo")//17
					{
						obj.Add(value.Name, value.InnerText);
					}



					if (value.Name == "lblStatHead")
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblWaveScore")
					{
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblSavedAnimals")
					{	
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblEnemiesKilled")
					{
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "lblAnimalsKilled")
					{
						
						obj.Add(value.Name, value.InnerText);
					}
					if (value.Name == "btnProceed")
					{		
						obj.Add(value.Name, value.InnerText);
					}
				}
				languages.Add(obj);//Add dictionary to the languageslist
			}
		}
	}
}
