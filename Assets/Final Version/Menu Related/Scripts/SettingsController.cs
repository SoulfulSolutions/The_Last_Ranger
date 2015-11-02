using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;
using System.Collections;

public class SettingsController : MonoBehaviour {

	public Slider gameSlider; // a variable which will store slider
	public Toggle gamemute;
	public Slider menuSlider;
	public Toggle menumute;
	public Slider masterSlider;
	public Toggle mastermute;
	private AudioSource menuaudio;
	
	public static float gameVal;
	
	
	private MainMenuController main;
	
	// Use this for initialization
	void Start ()
	{
		Debug.Log ("V2 start : " + gameSlider.value);
		menuaudio = GameObject.Find("menuaudio").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//taking a slider value and making it a float. assigning it to volume  
		//	main.setVol( (menuSlider.value) / 10);
		//PlayerPrefs.SetFloat ("volValue", AudioListener.volume); //setting a global variable of current slider value
		PlayerPrefs.SetFloat ("gamevol", gameSlider.value / 10f);
		Debug.Log ("I have " + (gameSlider.value / 10f));
		PlayerPrefs.SetFloat ("mastervol", masterSlider.value / 10f);
		menuaudio.volume = menuSlider.value / 10f;
		
		
		if (menuSlider.value > 0) {
			menumute.isOn = false;
		} else
			menumute.isOn = true;
		
		if (gameSlider.value > 0)
			gamemute.isOn = false;
		else
			gamemute.isOn = true;
		
		if (masterSlider.value > 0)
			mastermute.isOn = false;
		else
			mastermute.isOn = true;
		
		
	}
	
	public float v = 0;
	public void muteMenu()
	{
		
		if (menumute.isOn)
		{
			v =menuSlider.value;
			Debug.Log("first if menuvalue is : " + menuSlider.value);
			Debug.Log("first if v is : " + v);
			menuSlider.value = 0f;
		}
		else
		{
			Debug.Log("v is : "+v);
			menuSlider.value = v;
		}
	}
	
	public float v2 = 0;
	public void muteGame()
	{
		Debug.Log ("V2 ryt now " + gameSlider.value);
		Debug.Log (gamemute.isOn);
		if (gamemute.isOn == true)
		{
			v2 = gameSlider.value;
			Debug.Log("first if game value is : " + gameSlider.value);
			//	Debug.Log("first if v is : " + v);
			gameSlider.value = 0;
			//gamemute.isOn = true;
		}
		
		else
		{
			Debug.Log("v2 is : "+v2);
			gameSlider.value = v2;
			//	gamemute.isOn = false;
		}
	}
	
	public float v3 = 0;
	public void masterGame()
	{
		Debug.Log ("V2 ryt now " + gameSlider.value);
		Debug.Log (mastermute.isOn);
		if (mastermute.isOn == true)
		{
			v3 = masterSlider.value;
			Debug.Log("first if master value is : " + masterSlider.value);
			//	Debug.Log("first if v is : " + v);
			masterSlider.value = 0;
			//mastermute.isOn = true;
		}
		
		else
		{
			Debug.Log("v2 is : "+v3);
			masterSlider.value = v3;
			//mastermute.isOn = false;
		}
	}

}
