using UnityEngine;
using System.Collections;

public class backAndRestart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void restart()
    {
        Application.LoadLevel(9);
    }


    public void backtomenu()
    {
        Application.LoadLevel(0);
    }
}
