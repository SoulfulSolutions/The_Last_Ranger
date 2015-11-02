using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Internal;


public class StoryLineController : MonoBehaviour
{

    public Canvas StoryLineHolder;
    public Button Skip;

    public string levelToLoad;

    public GameObject background;
    public GameObject text;
    public GameObject progressBar;
    private int loadProgress = 0;


	void Awake()
	{

	}

	// Use this for initialization
	void Start ()
	{
        background.SetActive(false);
        text.SetActive(false);
        progressBar.SetActive(false);

	    StoryLineHolder = StoryLineHolder.GetComponent<Canvas>();
        Skip = Skip.GetComponent<Button>();
        
	}


    public void GoToNextScene()
    {
		DestroyImmediate(GameObject.Find("menuaudio"));
        StartCoroutine(DisplayLoadingScreen(levelToLoad));

    }


    IEnumerator DisplayLoadingScreen(string level)
    {

        background.SetActive(true);
        text.SetActive(true);
        progressBar.SetActive(true);
        StoryLineHolder.enabled = false;

        DestroyImmediate(GameObject.Find("soundObject"));

        progressBar.transform.localScale = new Vector3(loadProgress, progressBar.transform.position.y, progressBar.transform.position.z); // getting the position of the progress bar moving

        text.GetComponent<GUIText>().text = "Loading Progress " + loadProgress + "%"; //Text for the loading progress

        AsyncOperation async = Application.LoadLevelAsync(level);

        while (!async.isDone) //while the level is not yet loaded 
        {
            loadProgress = (int)(async.progress * 100); //Loading bar maximum percentage is 100.
            progressBar.transform.position = new Vector3(async.progress, progressBar.transform.position.y, progressBar.transform.position.z); //Moving the progress bar


            text.GetComponent<GUIText>().text = "Loading Progress " + loadProgress + "%";
            yield return null;

            Debug.Log("Loading Screen is visible");
        }

    } 

	
	

    //public void Pressed_Skip()
    //{
    //    Application.LoadLevel(5);
    //    DestroyImmediate(GameObject.Find("soundObject"));
    //}
}
