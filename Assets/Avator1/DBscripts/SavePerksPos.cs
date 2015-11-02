using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SavePerksPos : MonoBehaviour {

    Text username;
    public SaveGame savegame;
	private string url = "http://rangerdata.azurewebsites.net/";
    // Use this for initialization
    void Start()
    {
        // username = savegame.txtUsername;
    }

    // Update is called once per frame
    void Update()
    {



    }


    public void SavePosi()
    {
        {
            //when the button is clicked        
            //setup url to the ASP.NET webpage that is going to be called
            string customUrl = url + "SameGame/Create";

            //setup a form
            WWWForm form = new WWWForm();

            //Setup the paramaters

            //Save the perks position
            string x = transform.position.x.ToString("0.00");
            string y = transform.position.y.ToString("0.00");
            string z = transform.position.z.ToString("0.00");

            string rx = transform.rotation.x.ToString("0.00");
            string ry = transform.rotation.y.ToString("0.00");
            string rz = transform.rotation.z.ToString("0.00");

            form.AddField("PerksName", transform.name);
            form.AddField("PerkPosition", x + "," + y + "," + z);
            form.AddField("PerkRotation", rx + "," + ry + "," + rz);
            form.AddField("Username", username + "");



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
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
