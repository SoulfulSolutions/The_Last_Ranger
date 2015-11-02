using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public MenuTransitionController CurrentMenu;
    public MenuManager Menu;

    public Text Credit1;
    public Text Credit2;
    public Text Credit3;
    public Text Credit4;
    public Text Credit5;
    public Text Credit6;
    public Text Credit7;
    public Text Credit8;
    public Text Credit9;

    public Image Credit10;

    //public Button Back;

	// Use this for initialization
	void Start ()
	{
	    Credit1 = GameObject.Find("1").GetComponent<Text>();
        Credit2 = GameObject.Find("2").GetComponent<Text>();
        Credit3 = GameObject.Find("3").GetComponent<Text>();
        Credit4 = GameObject.Find("4").GetComponent<Text>();
        Credit5 = GameObject.Find("5").GetComponent<Text>();
        Credit6 = GameObject.Find("6").GetComponent<Text>();
        Credit7 = GameObject.Find("7").GetComponent<Text>();
        Credit8 = GameObject.Find("8").GetComponent<Text>();
        Credit9 = GameObject.Find("9").GetComponent<Text>();
        Credit10 = GameObject.Find("Soulful_Logo").GetComponent<Image>();

	    DisplayCredits();

	    //Back = GameObject.Find("btnBack").GetComponent<Button>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

     public void DisplayCredits()
    {
        StartCoroutine("LoadContent");

        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;
        //Back.enabled = false;
    }

    private IEnumerator LoadContent()
    {
        yield return new WaitForSeconds(3);
        Credit1.enabled = true;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;

        //diplay 1st part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = true;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;
        //Back.enabled = false;

        //diplay 2nd part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = true;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;

        //diplay 3rd part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = true;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;

        //diplay 4th part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = true;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;
        //Back.enabled = false;

        //diplay 5th part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = true;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;
        //Back.enabled = false;

        //diplay 6th part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = true;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;
       // Back.enabled = false;

        //diplay 7th part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = true;
        Credit9.enabled = false;
        Credit10.enabled = false;
        //Back.enabled = false;

        //diplay 8th part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = true;
        Credit10.enabled = false;
        //Back.enabled = false;

        //diplay 9th part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = true;
        //Back.enabled = false;
        
        //diplay 10th part
        yield return new WaitForSeconds(3);
        Credit1.enabled = false;
        Credit2.enabled = false;
        Credit3.enabled = false;
        Credit4.enabled = false;
        Credit5.enabled = false;
        Credit6.enabled = false;
        Credit7.enabled = false;
        Credit8.enabled = false;
        Credit9.enabled = false;
        Credit10.enabled = false;
        
		StopCoroutine ("LoadContent");
        Menu.ShowMenu(CurrentMenu);
        //finnish
    }
}
