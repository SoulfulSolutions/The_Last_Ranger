using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelSound : MonoBehaviour {
    public Canvas SettingsHolder;
    public Slider volValue; // a variable which will store slider
    //public Rect SliderLocation;
  
    // Use this for initialization
    void Start()
    {
        SettingsHolder = SettingsHolder.GetComponent<Canvas>();
        float s = PlayerPrefs.GetFloat("levelValue", 10); //when setting script run for the first time the volume value will be max (10) and after will get a value from prefs called volValue 
        volValue.value = 10 * s; // taking a float value and multiply by 10
    }

    // Update is called once per frame
    void Update()
    {
        AudioListener.volume = (volValue.value / 10); //taking a slider value and making it a float. assigning it to volume  

        PlayerPrefs.SetFloat("levelValue", AudioListener.volume); //setting a global variable of current slider value

      

    }
}
