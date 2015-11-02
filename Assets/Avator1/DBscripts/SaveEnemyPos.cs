using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveEnemyPos : MonoBehaviour
{

    Text username;

    private EnemyAI Enemyhealth;
    public static bool save = false;
    public GameObject saveCanvas;

	private string url = "http://rangerdata.azurewebsites.net/";
    // Use this for initialization



	public void Awake()
	{
	
	}

    void Start()
    {
        Enemyhealth = gameObject.GetComponent<EnemyAI>();
        // username = savegame.txtUsername;

		Debug.Log (PlayerPrefs.GetString ("un"));

		Debug.Log( PlayerPrefs.GetString ("Load")+" on enemies");

		if (PlayerPrefs.GetString ("Load") != "") 
		{

			LoadEnemy();
		}
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.KeypadEnter)) && (saveCanvas.activeInHierarchy == true))
        {
            //calling the method to save the enemy states
            SavePosi();
        }
//
//        if (Input.GetKeyDown(KeyCode.L))
//        {
//            //call the method to save the player's states
//            Debug.Log("load enemy method is running");
//            LoadEnemy();
//
//        }

    }

    public void SavePosi()
    {
        {

            //setup url to the ASP.NET webpage that is going to be called
            string customUrl = url + "EnemiesViews/Create";

            //setup a form
            WWWForm form = new WWWForm();

            //Setup the paramaters

            //Save the enemies position
            string x = transform.position.x.ToString("0.00");
            string y = transform.position.y.ToString("0.00");
            string z = transform.position.z.ToString("0.00");

            string rx = transform.rotation.x.ToString("0.00");
            string ry = transform.rotation.y.ToString("0.00");
            string rz = transform.rotation.z.ToString("0.00");


            form.AddField("EnemyPosition", x + "," + y + "," + z);
            form.AddField("EnemyRotation", rx + "," + ry + "," + rz);
            
            form.AddField("EnemyName", transform.name);

			form.AddField("FBuserId", FB.UserId);
			form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString());

            if (transform.name.Contains("cyclop_soldier"))
            {


                form.AddField("EnemyHealth", Enemyhealth.cyclopHealth + "");
                Debug.Log(transform.name + " Saved!!!!");


                WWW www = new WWW(customUrl, form);
                StartCoroutine(WaitForRequest(www));
            }


            if (transform.name.Contains("Troll"))
            {


                form.AddField("EnemyHealth", Enemyhealth.trollHealth + "");
                Debug.Log(transform.name + " Saved!!!!");

                WWW www = new WWW(customUrl, form);
                StartCoroutine(WaitForRequest(www));
            }

            if (transform.name.Contains("GOBLIN"))
            {



                form.AddField("EnemyHealth", Enemyhealth.goblinHealth + "");
                Debug.Log(transform.name + " Saved!!!!");


                WWW www = new WWW(customUrl, form);
                StartCoroutine(WaitForRequest(www));

            }
            //Call the server

        }
    }



    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {      //write data returned from ASP.NET
            Debug.Log(www.text);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }



    public void LoadEnemy()
    {


		string url = "http://rangerdata.azurewebsites.net/";

        //when the button is clicked        
        //setup url to the ASP.NET webpage that is going to be called
        string customUrl = url + "EnemiesViews/Details";

        //setup a form
        WWWForm form = new WWWForm();

        //creates a random user
		form.AddField ("CustomUser", PlayerPrefs.GetString ("un").ToString());
        form.AddField("FBuserID", FB.UserId);
        form.AddField("EnemyName", transform.name);

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
			if(playerStates.Length!=0)
			{
            transform.position = new Vector3(float.Parse(playerStates[1]), float.Parse(playerStates[2]), float.Parse(playerStates[3]));
            transform.Rotate(float.Parse(playerStates[4]), float.Parse(playerStates[5]), float.Parse(playerStates[6]));



            if (transform.name.Contains("cyclop_soldier"))
            {

                Enemyhealth.cyclopHealth = float.Parse(playerStates[0]);

            }


            if (transform.name.Contains("Troll"))
            {

                Enemyhealth.trollHealth = float.Parse(playerStates[0]);

            }

            if (transform.name.Contains("GOBLIN"))
            {

                Enemyhealth.goblinHealth = float.Parse(playerStates[0]);


            }
			}
			else
			{
				Debug.Log("destroy "+gameObject.name+" because it was dead");

			}


        }
        else
        {
			Destroy(gameObject);
            Debug.Log("WWW Error: " + www.error);
        }
    }



}
