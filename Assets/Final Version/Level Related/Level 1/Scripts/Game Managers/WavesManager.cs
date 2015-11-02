using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    public GameObject[] WaveEnemies;
    public Canvas WaveCanvas;
   // public Image WaveInfo;
   // public Button Continue;

    private int _countEnemies = 0;

	// Use this for initialization
	void Start ()
	{
	    WaveCanvas = WaveCanvas . GetComponent<Canvas>();
       // WaveInfo = GetComponent<Image>();
	   // Continue   = GetComponent<Button>();

	    _countEnemies = WaveEnemies.Length;
        Debug.Log(_countEnemies);

	    WaveCanvas.enabled = false;
	    //WaveInfo.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        _countEnemies = WaveEnemies.Length;
    //    Debug.Log("There are " + _countEnemies +" enemies left in the wave");
		//if(WaveEnemies!=null)
   //     if (WaveEnemies[0].tag == "")

	    if (_countEnemies == 0)
	    {
           
	        Debug.Log("Wave is complete.... load next wave");
            WaveCanvas.enabled = true;
            Time.timeScale = 0;
           // WaveInfo.enabled = true;
	    }
	    else
	    {
	        Time.timeScale = 1;
	    }


    }

    public void Next_Wave()
    {
		if(GameObject.Find("WaveNumber").tag=="Wave1")
        Application.LoadLevel(10);
		else if(GameObject.Find("WaveNumber").tag=="Wave2")
			Application.LoadLevel(11);
    }
    
}
