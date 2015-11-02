using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SavePlayerPos : MonoBehaviour
{

    //Text username;
    //public SaveGame savegame;
    private PlayerHealth playerHealth;
	private string url = "http://rangerdata.azurewebsites.net/";

    public  float loadedHealth;
    public GameObject saveCanvas;
    // Use this for initialization

	public PauseManager pausermanagerScript;

    public void Awake()
    {
	
    }


    public void LoadPlayer()
    {


    //    string url = "http://localhost:55624/";

        //when the button is clicked        
        //setup url to the ASP.NET webpage that is going to be called
        string customUrl = url + "PlayerState/Details";

        //setup a form
        WWWForm form = new WWWForm();

        //creates a random user

        form.AddField("FBuserID", FB.UserId);
		form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString());
		form.AddField ("GameId", PlayerPrefs.GetString ("Load").ToString());
        //Call the server
        WWW www = new WWW(customUrl, form);
        StartCoroutine(LoadPlayerWaitForRequest(www));
    }



    IEnumerator LoadPlayerWaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {      //write data returned from ASP.NET

            Debug.Log(www.text);
            string[] playerStates = www.text.Split(',');

            transform.position = new Vector3(float.Parse(playerStates[1]), float.Parse(playerStates[2]), float.Parse(playerStates[3]));
            transform.Rotate(float.Parse(playerStates[4]), float.Parse(playerStates[5]), float.Parse(playerStates[6]));

			PlayerPrefs.DeleteKey("Load");
            loadedHealth = float.Parse(playerStates[0]);
            Debug.Log("Sya loaded health " + loadedHealth);
            playerHealth.currHealthContr = true;
       
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    void Start()
    {

        //saveCanvas = GameObject.Find ("SaveCamePanel");
        playerHealth = gameObject.GetComponent<PlayerHealth>();

		if (PlayerPrefs.GetString ("Load") != "") 
		{
			Time.timeScale = 1; 
			LoadPlayer();
		
		}
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.KeypadEnter)) && (saveCanvas.activeInHierarchy == true))
        {
            //call the method to save the player's states
            SavePosi();
        }

//        if (Input.GetKeyDown(KeyCode.L))
//        {
//            //call the method to save the player's states
//
//            LoadPlayer();
//
//        }

    }


    public void SavePosi()
    {
        {
            //setup url to the ASP.NET webpage that is going to be called
            string customUrl = url + "PlayerState/Create";

            //setup a form
            WWWForm form = new WWWForm();

            //Setup the paramaters

            //Save the players position
            string x = transform.position.x.ToString("0.00");
            string y = transform.position.y.ToString("0.00");
            string z = transform.position.z.ToString("0.00");

            string rx = transform.rotation.x.ToString("0.00");
            string ry = transform.rotation.y.ToString("0.00");
            string rz = transform.rotation.z.ToString("0.00");

            form.AddField("PlayerName", transform.name);
            form.AddField("PlayerHealth", playerHealth.CurrentHealth.ToString()); 
            form.AddField("PlayerPosition", x + "," + y + "," + z);
            form.AddField("PlayerRotation", rx + "," + ry + "," + rz);
            form.AddField("FBuserId", FB.UserId);
			form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString());
            //Call the server
            WWW www = new WWW(customUrl, form);
            StartCoroutine(WaitForRequest(www));
        }
    }


    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {      //write data returned from ASP.NET
            Debug.Log(www.text);
			Debug.Log("Player was saved");
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
