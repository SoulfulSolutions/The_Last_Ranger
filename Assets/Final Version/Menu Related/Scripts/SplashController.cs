using UnityEngine;
using System.Collections;

public class SplashController : MonoBehaviour 
{

	// Use this for initialization
	/*void Start ()
	{
	    LoadingScenes();
	}
	
	// Update is called once per frame
	void Update () 
    {
	}

    public void LoadingScenes()
    {
        StartCoroutine("Wait");
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        Application.LoadLevel("Login Select");
    }*/

	public float Fixtime= 2.0f;
	public float Dimtime= 2.0f;
	public Light dimLight;
	public float zoomSpeed= 0.2f;
	
	Camera camera;
	float timer;
	
	
	
	// Use this for initialization
	void Start () 
	{
		camera = GameObject.Find("Main Camera").GetComponent<Camera> ();
		timer = 0.0f;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		camera.fieldOfView -= zoomSpeed;
		if(timer > Dimtime && timer < Fixtime)
		{
			dimLight.intensity -=zoomSpeed;
		}
		else if(timer > Fixtime)
		{
			Application.LoadLevel(1);
		}
		
	}
}
