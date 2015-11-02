using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using UnityEngine.UI;

public class menufbReader : MonoBehaviour {

    public TextAsset dictionary;//we assign xmlfie

    public string LanguageName;//will show current language
    public int CurrentLanguage;//will change the language

    string lblMain;//check how its written on the other eg on the main pc
    string btnNewG;
    string btnLoad;
    string btnInstr;
    string btnSett;
    string btnCred;
    string btnLog;

    string lblInstr;
    string btnCon;
    string btnPerk;
    string btnScore;
    string btnBackIns;

    string lblSett;
    string lblMenS;
    string lblMute;
    string lblLevs;
    string lblMute2;
    string btnBackSet;
    string lblCred;
    string btnCredBack;
    string lblmaster;
    string mute3;


    public Text LMainText;
    public Text BNewGText;
    public Text BLoadText;
    public Text BInstrText;
    public Text BSettText;
    public Text BCredText;
    public Text BLogText;

    public Text LInstrText;
    public Text BConText;
    public Text BPerkText;
    public Text BScoreText;
    public Text BBackInsText;

    public Text LSettText;
    public Text LMenSText;
    public Text LMuteText;
    public Text LLevsText;
    public Text LMute2Text;
    public Text BBackSetText;
    public Text LCredText;
    public Text BCredBackText;
    public Text txtMaster;
    public Text chk3;






    List<Dictionary<string, string>> languages = new List<Dictionary<string, string>>();
    // above we create a list called languges



    Dictionary<string, string> obj;//we call this dictionary object

    void Awake()//will check the reader method before the game starts
    {
        Reader();
        CurrentLanguage = PlayerPrefs.GetInt("lang");
    }

    void Start()
    {

        LMainText = LMainText.GetComponent<Text>();

        BNewGText = BNewGText.GetComponent<Text>();

        BLoadText = BLoadText.GetComponent<Text>();

        BInstrText = BInstrText.GetComponent<Text>();

        BSettText = BSettText.GetComponent<Text>();

        BCredText = BCredText.GetComponent<Text>();

        BLogText = BLogText.GetComponent<Text>();

        LInstrText = LInstrText.GetComponent<Text>();

        BConText = BConText.GetComponent<Text>();

        BPerkText = BPerkText.GetComponent<Text>();

        BScoreText = BScoreText.GetComponent<Text>();

        BBackInsText = BBackInsText.GetComponent<Text>();

        LSettText = LSettText.GetComponent<Text>();

        LMenSText = LMenSText.GetComponent<Text>();

        LMuteText = LMuteText.GetComponent<Text>();

        LLevsText = LLevsText.GetComponent<Text>();

        LMute2Text = LMute2Text.GetComponent<Text>();

        BBackSetText = BBackSetText.GetComponent<Text>();

        LCredText = LCredText.GetComponent<Text>();

        txtMaster = txtMaster.GetComponent<Text>();

        BCredBackText = BCredBackText.GetComponent<Text>();

        chk3 = chk3.GetComponent<Text>();
    }
    private void Update()//updates every frame
    {
        Debug.Log(languages.Count);
        //	      languages[0].TryGetValue("Name", out LanguageName);//2 languages are stored here through current language in integers

        languages[CurrentLanguage].TryGetValue("lblMain", out lblMain);

        languages[CurrentLanguage].TryGetValue("btnNewG", out btnNewG);

        languages[CurrentLanguage].TryGetValue("btnLoad", out btnLoad);

        languages[CurrentLanguage].TryGetValue("btnInstr", out btnInstr);

        languages[CurrentLanguage].TryGetValue("btnSett", out btnSett);

        languages[CurrentLanguage].TryGetValue("btnCred", out btnCred);

        languages[CurrentLanguage].TryGetValue("btnLog", out btnLog);

        languages[CurrentLanguage].TryGetValue("lblInstr", out lblInstr);

        languages[CurrentLanguage].TryGetValue("btnCon", out btnCon);

        languages[CurrentLanguage].TryGetValue("btnPerk", out btnPerk);

        languages[CurrentLanguage].TryGetValue("btnScore", out btnScore);

        languages[CurrentLanguage].TryGetValue("btnBackIns", out btnBackIns);

        languages[CurrentLanguage].TryGetValue("lblSett", out lblSett);

        languages[CurrentLanguage].TryGetValue("lblMens", out lblMenS);

        languages[CurrentLanguage].TryGetValue("lblMute", out lblMute);

        languages[CurrentLanguage].TryGetValue("lblLevs", out lblLevs);

        languages[CurrentLanguage].TryGetValue("lblMute2", out lblMute2);

        languages[CurrentLanguage].TryGetValue("btnBackSet", out btnBackSet);

        languages[CurrentLanguage].TryGetValue("lblCred", out lblCred);

        languages[CurrentLanguage].TryGetValue("btnCredBack", out btnCredBack);

        languages[CurrentLanguage].TryGetValue("lblMaster", out lblmaster);
        languages[CurrentLanguage].TryGetValue("lblMute3", out mute3);
    }

    void OnGUI()
    {
        LMainText.text = lblMain;

        BNewGText.text = btnNewG;

        BLoadText.text = btnLoad;

        BInstrText.text = btnInstr;

        BSettText.text = btnSett;

        BCredText.text = btnCred;

        BLogText.text = btnLog;

        LInstrText.text = lblInstr;

        BConText.text = btnScore;

        BPerkText.text = btnPerk;

        BScoreText.text = btnScore;

        BBackInsText.text = btnBackIns;

        LSettText.text = lblSett;

        LMenSText.text = lblMenS;

        LMuteText.text = lblMute;

        LLevsText.text = lblLevs;

        LMute2Text.text = lblMute2;

        BBackSetText.text = btnBackSet;

        LCredText.text = lblCred;

        BCredBackText.text = btnCredBack;

        txtMaster.text = lblmaster;

        chk3.text = mute3;



    }

    void Reader()
    {
        Debug.Log("reader...");
        XmlDocument xmldoc = new XmlDocument();// we make an xmldocument
        {
            Debug.Log("Second..");
            Debug.Log(dictionary.name);
            xmldoc.LoadXml(dictionary.text);//attach dicctionary text to xml document

            XmlNodeList languagesList = xmldoc.GetElementsByTagName("language2");
            //make list that will check every node that is called language

            foreach (XmlNode languagevalueNode in languagesList)//check every xmlnode language value in languages list
            {
                Debug.Log("foreach...");
                XmlNodeList languageContent = languagevalueNode.ChildNodes;//every language will have its own content eg button
                obj = new Dictionary<string, string>();
                obj.Clear();
                foreach (XmlNode value in languageContent)//check every single value in value list for current language
                {
                    if (value.Name == "Name")
                    {
                        Debug.Log("we here");

                        obj.Add(value.Name, value.InnerText);//check if the name is the same as the one in the xml file and add it

                    }
                    if (value.Name == "lblMain")
                    {
                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "btnNewG")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnLoad")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnInstr")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnSett")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnCred")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnLog")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    //                    if (value.Name == "btnInstr")
                    //                    {
                    //
                    //                        obj.Add(value.Name, value.InnerText);
                    //                    }
                    if (value.Name == "lblInstr")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "btnCon")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnPerk")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnScore")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }




                    if (value.Name == "btnBackIns")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblSett")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblMaster")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblMute3")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblMens")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "lblMute")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "lblLevs")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "lblMute2")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnBackSet")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }

                    if (value.Name == "lblCred")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                    if (value.Name == "btnCredBack")
                    {

                        obj.Add(value.Name, value.InnerText);
                    }
                }
                languages.Add(obj);//Add dictionary to the languageslist
            }
        }
    }
}
