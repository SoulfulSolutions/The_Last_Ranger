using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{

    public MenuTransitionController CurrentMenu;


	void Awake()
	{

	}
    public void Start()
    {
        ShowMenu(CurrentMenu);
		Debug.Log (PlayerPrefs.GetString("un")+" is logged in");
		if(GameObject.Find ("txtusername")!=null)
		GameObject.Find ("txtusername").GetComponent<Text> ().text ="Hello , " +PlayerPrefs.GetString ("un");
		Time.timeScale = 1;
	//	PlayerPrefs.DeleteKey ("un"); //logout
    }

	//public GameObject loadGamePanel;
	//public GameObject menuObject;

    public void ShowMenu(MenuTransitionController menu)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.IsOpen = false;
        }

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
		Time.timeScale = 1;
    }

    public void ShowCustomLogin()
    {
        Application.LoadLevel(4);
    }

    public void ShowFacebookMenu()
    {
        Application.LoadLevel(2);
    }

    public void ShowCustomMenu()
    {
        Application.LoadLevel(3);
    }

    public void ShowCreateAccount()
    {
        Application.LoadLevel(5);
    }

    public void CreateAccount()
    {
        Application.LoadLevel(2);
    }
    public void Play()
    {
        Application.LoadLevel("Wave 1");
    }

	public void Logout()
    {
			PlayerPrefs.DeleteKey("Load");
			PlayerPrefs.DeleteKey("un");
			PlayerPrefs.DeleteAll ();
			Application.LoadLevel("Login Select");
	
    }

    public void LoadWave1()
    {
        Application.LoadLevel(6);
    }

    public void ShowStoryLine()
    {
        Application.LoadLevel(6);
    }


}
