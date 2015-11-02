using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Internal;

public class StorylineController : MonoBehaviour
{
    public Text Paragraph1;
    public Text Paragraph2;
    public Button BtnSkip;
	public GameObject bSkip;

	public Canvas StoryLineHolder;

    public GameObject Background;
    public GameObject Text;
    public GameObject ProgressBar;
    public string LevelToLoad;

    private int _loadProgress = 0;

    void Awake()
	{
		DontDestroyOnLoad (GameObject.Find ("menuaudio"));
	}
    private void Start()
    {
 		BtnSkip =BtnSkip.GetComponent<Button>();
		StoryLineHolder = StoryLineHolder.GetComponent<Canvas>();
        Background.SetActive(false);
        Text.SetActive(false);
        ProgressBar.SetActive(false);
        LoadingStoryline();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void LoadingStoryline()
    {
        StartCoroutine("LoadPar");

        Paragraph1.enabled = true;
        Paragraph2.enabled = false;
        //btnSkip.enabled = false;
    }

    private IEnumerator LoadPar()
    {
        yield return new WaitForSeconds(15);
        Paragraph1.enabled = false;
        Paragraph2.enabled = true;
        //btnSkip.enabled = true;
    }

    public void Pressed_Skip()
    {
		bSkip.SetActive (false);
		ScoreManager.score = 0;
        StartCoroutine(DisplayLoadingScreen(LevelToLoad));
    }

    private IEnumerator DisplayLoadingScreen(string level)
    {
        Background.SetActive(true);
        Text.SetActive(true);
        ProgressBar.SetActive(true);

		Paragraph1.enabled = false;
		Paragraph2.enabled = false;

        DestroyImmediate(GameObject.Find("soundObject"));

        ProgressBar.transform.localScale = new Vector3(_loadProgress, ProgressBar.transform.position.y,
        ProgressBar.transform.position.z); // getting the position of the progress bar moving

        Text.GetComponent<GUIText>().text = "Loading Progress " + _loadProgress + "%"; //Text for the loading progress

        AsyncOperation async = Application.LoadLevelAsync(level);

        while (!async.isDone) //while the level is not yet loaded 
        {
            _loadProgress = (int) (async.progress*100); //Loading bar maximum percentage is 100.
            ProgressBar.transform.position = new Vector3(async.progress, ProgressBar.transform.position.y,
            ProgressBar.transform.position.z); //Moving the progress bar

            Text.GetComponent<GUIText>().text = "Loading Progress " + _loadProgress + "%";
            yield return null;
        }



    }
}