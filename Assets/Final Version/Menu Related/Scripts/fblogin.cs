using UnityEngine;
using System.Collections;

public class fblogin : MonoBehaviour {

	void Awake()
	{
		FB.Init (SetInit, onHideUnity);

	}
	private void SetInit()
	{
		Debug.Log ("fb initial done");
		enabled = true;
		if (FB.IsLoggedIn) 
		{

		} 
		
	}

	private void onHideUnity(bool isGameShown)
	{
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	public void FBlogin()
	{
		if (FB.IsLoggedIn)
			Application.LoadLevel ("Menu for facebook");
		else 
		{
			FB.LogInWithReadPermissions ("email", AuthCallback);
		}
	}
	void AuthCallback(FBResult result)
	{
		if (FB.IsLoggedIn) 
		{
			Application.LoadLevel("Menu for facebook");
			//dealsWithMenu(true);
			
		} 
		
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
