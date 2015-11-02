using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveGameState : MonoBehaviour
{
	private string url = "http://rangerdata.azurewebsites.net/";

    public GameObject pnlPause;
    public GameObject pnlSave;
    public Text Gamename;
    public ScoreManager score;
    public GameObject saveCanvas;
	public Text responceText;
	//public LoadGame loadgameScript;
    // Use this for initialization
    void Start()
    {

        //saveCanvas = GameObject.Find ("SaveCamePanel");

    }

    // Update is called once per frame
    void Update()
    {


		if ((Input.GetKeyDown(KeyCode.KeypadEnter)) && (saveCanvas.activeInHierarchy == true)&&(GameObject.Find("txtnickname").activeInHierarchy == true))
		{
			Debug.Log("it is working on save state as well yeeeey.... Save controller..");
            SaveGameSecond();

            if (GameObject.Find("txtnickname").activeInHierarchy == true)
                GameObject.Find("txtnickname").SetActive(false);
        }

    }

	public void SavePressed()
	{

	}

    public void ButtonToSave()
    {
        pnlPause.SetActive(false);
        pnlSave.SetActive(true);
    }
    public void ButtonBackSave()
    {
        pnlPause.SetActive(true);
        pnlSave.SetActive(false);
    }

    public void SaveGameFirst()
    {


        //when the button is clicked        
        //setup url to the ASP.NET webpage that is going to be called
        string customUrl = url + "SaveGame/Create";

        //setup a form
        WWWForm form = new WWWForm();

        //creates a random user

        form.AddField("FBuserID", FB.UserId);
		form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString());
        //Call the server
        WWW www = new WWW(customUrl, form);
        StartCoroutine(WaitForRequest(www));
    }
    public void SaveGameSecond()
    {
	

		//when the button is clicked        
		//setup url to the ASP.NET webpage that is going to be called
		string customUrl = url + "SaveGame/Edit";

		//setup a form
		WWWForm form = new WWWForm ();

			//creates a random user
			form.AddField ("GameName", Gamename.text);
			form.AddField ("GameScore", ScoreManager.score);

			form.AddField ("GameWaveNo", Application.loadedLevelName);
			form.AddField ("FBuserID", FB.UserId);
			form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString ());
			SaveEnemyPos.save = true;
			Debug.Log ("Save changed to true.......");

			//Call the server
			WWW www = new WWW (customUrl, form);

			StartCoroutine (WaitForRequest (www));
			//	loadgameScript.LoadGameStates ();
		if (GameObject.Find ("txtnickname").activeInHierarchy == true) 
			GameObject.Find ("txtnickname").SetActive (false);
		
		if(GameObject.Find("btnSave").activeInHierarchy==true)
			GameObject.Find("btnSave").SetActive(false);
	}

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
			responceText.text=www.text;
            Debug.Log(www.text);
        }
        else
        {

            Debug.Log("WWW Error: " + www.error);
        }
    }


}
