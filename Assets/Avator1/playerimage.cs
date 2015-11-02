using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerimage : MonoBehaviour {

	Texture2D pictex = null;
	bool showPic = false;
	public  Texture pic;
	void Start () 
	{
		StartCoroutine(getPic());
	}

	void OnGUI()
	{
		if (FB.IsLoggedIn) {
			if (showPic) {
				GUI.DrawTexture (new Rect (0, 0, 40, 40), pictex);
			}
		} 
		else 
		{
			GUI.DrawTexture(new Rect(0,0,40,40), pic);
		}
	}

	public IEnumerator getPic()
	{
		var www = new WWW ("http://graph.facebook.com/" + FB.UserId + "/picture?type=square");
		yield return www;
		
		pictex = new Texture2D (25, 25);
		www.LoadImageIntoTexture (pictex);
		
		showPic = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
