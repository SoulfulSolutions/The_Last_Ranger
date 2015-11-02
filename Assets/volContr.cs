using UnityEngine;
using System.Collections;

public class volContr : MonoBehaviour {
    

	// Use this for initialization
	void Start ()
    {
      AudioListener.volume = PlayerPrefs.GetFloat("levelValue", 10);
	}

}
