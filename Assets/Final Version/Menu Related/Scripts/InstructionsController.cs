using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InstructionsController : MonoBehaviour
{

    public Text ControlsText;

    public Text PerksText1;
    public Text PerksText2;
    public Text PerksText3;
    public Text PerksText4;

    public Text ScoringText;

    public Button ControlsButton;
    public Button PerksButton;
    public Button ScoringButton;

	// Use this for initialization
	void Start ()
	{
	    ControlsText = GameObject.Find("txtControls").GetComponent<Text>();

        PerksText1 = GameObject.Find("txtPerks1").GetComponent<Text>();
        PerksText2 = GameObject.Find("txtPerks2").GetComponent<Text>();
        PerksText3 = GameObject.Find("txtPerks3").GetComponent<Text>();
        PerksText4 = GameObject.Find("txtPerks4").GetComponent<Text>();

        ScoringText = GameObject.Find("txtScoring").GetComponent<Text>();

	    ControlsButton = GetComponent<Button>();
        PerksButton = GetComponent<Button>();
        ScoringButton = GetComponent<Button>();

	    ControlsText.enabled = true;
	    PerksText1.enabled = false;
        PerksText2.enabled = false;
        PerksText3.enabled = false;
	    PerksText4.enabled = false;
	    ScoringText.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void Pressed_Controls()
    {
        ControlsText.enabled = true;
        PerksText1.enabled = false;
        PerksText2.enabled = false;
        PerksText3.enabled = false;
        PerksText4.enabled = false;
        ScoringText.enabled = false;
        StopCoroutine("LoadPerks");
    }

    public void Pressed_Perks()
    {
        ControlsText.enabled = false;
        ScoringText.enabled = false;
        StartCoroutine("LoadPerks");
    }

    public void Pressed_Scoring()
    {
        ControlsText.enabled = false;
        PerksText1.enabled = false;
        PerksText2.enabled = false;
        PerksText3.enabled = false;
        PerksText4.enabled = false;
        ScoringText.enabled = true;
        StopCoroutine("LoadPerks");
    }

        private IEnumerator LoadPerks()
        {
            //1st part
            yield return  new WaitForSeconds(0);
            PerksText1.enabled = true;
            PerksText2.enabled = false;
            PerksText3.enabled = false;
            PerksText4.enabled = false;

            //2nd part
            yield return new WaitForSeconds(3);
            PerksText1.enabled = false;
            PerksText2.enabled = true;
            PerksText3.enabled = false;
            PerksText4.enabled = false;

            //3rd part
            yield return new WaitForSeconds(3);
            PerksText1.enabled = false;
            PerksText2.enabled = false;
            PerksText3.enabled = true;
            PerksText4.enabled = false;
        
            //4th part
            yield return new WaitForSeconds(3);
            PerksText1.enabled = false;
            PerksText2.enabled = false;
            PerksText3.enabled = false;
            PerksText4.enabled = true;

            //5th part
            yield return new WaitForSeconds(3);
            PerksText1.enabled = false;
            PerksText2.enabled = false;
            PerksText3.enabled = false;
            PerksText4.enabled = false;
        }
}
