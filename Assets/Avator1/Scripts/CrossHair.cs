using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour {
	public Texture2D crosshair;

	void Start () {
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	void OnGUI()
	{
	
		//GUI.DrawTexture (new Rect(Screen.width/2,Screen.height/2,crosshair.width+1,crosshair.height+1),crosshair);
		if(Input.GetButton("Fire2"))
			GUI.DrawTexture (new Rect(Screen.width/2,Screen.height/2,crosshair.width+3,crosshair.height+3),crosshair);
	}

}
