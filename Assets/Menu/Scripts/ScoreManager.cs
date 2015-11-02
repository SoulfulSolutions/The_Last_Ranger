using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static int score=0;
    public Text text;


    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "Score: " + score;
    }
	
}
