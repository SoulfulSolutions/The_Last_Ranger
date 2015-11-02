using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class waveContro : MonoBehaviour {


	public GameObject cyclopsholder;
	

	
	//	private static int _goblinCount = 3;
	//	public static int _cyclopsCount = 4;
	//	public static int _trollCount = 5;
	

	GameObject [] _cyclopsCount;
	public Canvas loadlevel3;

 //   public Image bosshealth;
  //  public Text bossText;

	public void Awake()
	{
		
		//		GameObject.Find ("DestroyCyclop").SetActive (false);
		//		GameObject.Find("DestroyTroll").SetActive(false);
	}
	
	void Start()
	{
		loadlevel3 = loadlevel3 .GetComponent <Canvas >();
        loadlevel3.enabled = false;

//        bosshealth.enabled = false;
     //   bossText.enabled = false;


		//		goblinHolder = GameObject.Find ("Goblins_Wave_1");
		//		cyclopsholder = GameObject.Find ("Cyclops_Wave_1");
		//		trollHolder = GameObject.Find ("Trolls_Wave_1");
		
		//		
		
		//
		//        _goblinHolder.SetActive(true);
		//        _cyclopsHolder.SetActive(false);
		//        _trollHolder.SetActive(false);
		//        _bossHolder.SetActive(false);
		//
		//        Debug.Log("Number of Goblins is : "+EnemyAI._goblinCount);
		//        Debug.Log("Number of Cyclops is : "+EnemyAI._cyclopsCount);
		//        Debug.Log("Number of Trolls is : "+EnemyAI._trollCount);
	}
	
	
	
	void Update()
	{
		_cyclopsCount = GameObject.FindGameObjectsWithTag ("CyclopEnemy");

		
		ManageWaves();
	}
	
	void ManageWaves()
	{
		//Debug.Log ("Count : " + _cyclopsCount.Length);

			if (_cyclopsCount.Length == 0) 
            {
				
				Destroy (GameObject.Find ("DestroyCyclop"));
				
                   loadlevel3.enabled = true;
            
		      }

        if (_cyclopsCount.Length == 0)
            Time.timeScale = 0; //Set the Game timescale to 0. Pauses the game at that time
        else
            Time.timeScale = 1; //Set the Game timescale to 1. Plays the game   
	}


    public void Continue()
    {
        Application.LoadLevel(11);
    }

    public void quit()
    {
        Application.LoadLevel(0);
    }  
		
		
		//       Debug.Log("There are " + EnemyAI._goblinCount + " goblins left");
		//       if (EnemyAI._goblinCount == 0)
		//       {
		//           _goblinHolder.SetActive(false);
		//           _cyclopsHolder.SetActive(true);
		//           _trollHolder.SetActive(false);
		//           _bossHolder.SetActive(false);
		//       }
		//
		//       Debug.Log("There are " + EnemyAI._trollCount + " trolls left");
		//
		//       if (EnemyAI._trollCount == 0)
		//       {
		//           _goblinHolder.SetActive(false);
		//           _cyclopsHolder.SetActive(false);
		//           _trollHolder.SetActive(false);
		//           _bossHolder.SetActive(true);
		//       }
		//
		//       Debug.Log("There are " + EnemyAI._cyclopsCount + " cyclops left");
		//
		//       if (EnemyAI._cyclopsCount == 0)
		//       {
		//           _goblinHolder.SetActive(false);
		//           _cyclopsHolder.SetActive(false);
		//           _trollHolder.SetActive(true);
		//           _bossHolder.SetActive(false);
		//       }
	}


