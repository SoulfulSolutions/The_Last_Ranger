using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WavesController : MonoBehaviour
{
	public  GameObject goblinHolder;
	GameObject [] _goblinCount;

    public Canvas loadlevel2;
    public Image bosshealth;
    public Text bossText;

    public void Awake()
    {
        
    }

    void Start()
    {
        loadlevel2 = loadlevel2.GetComponent<Canvas>();
        

        bosshealth.enabled = false;
        bossText.enabled = false;
        loadlevel2.enabled = false;
       
    }



    void Update()
    {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
		_goblinCount = GameObject.FindGameObjectsWithTag ("Goblin");

        ManageWaves();
    }

    void ManageWaves()
    {
		if (_goblinCount.Length == 0) {
		
			Destroy (GameObject.Find ("DestroyGoblin"));
            loadlevel2.enabled = true;  

            
		}

        if (_goblinCount.Length == 0)
            Time.timeScale = 0; //Set the Game timescale to 0. Pauses the game at that time
        else
            Time.timeScale = 1; //Set the Game timescale to 1. Plays the game   
	}


    public void Continue()
    {
        Application.LoadLevel(10);
    }

    public void quit()
    {
        Application.LoadLevel(0);
    }  

}
