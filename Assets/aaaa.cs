using UnityEngine;
using System.Collections;

public class aaaa : MonoBehaviour 
{
	public GameTips gameTipScript;
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		Time.timeScale = 0;
		if ((PlayerPrefs.GetString ("Load") != "")||gameTipScript.checkGameTip == true) 
		{
			Time.timeScale = 0;
		} else 
		{
			gameObject.SetActive(false);
		}
	}
}
