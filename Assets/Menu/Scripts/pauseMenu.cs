using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class pauseMenu : MonoBehaviour 
{
    public Canvas PauseMenu; //Create a variable for the Pause Menu Canvas
    public Canvas QuitMenu; //Create a variable for the Quit Menu Canvas
    public Button Resume; //Create a variable for the Resume Button
    public Button Restart; //Create a variable for the Restart Button
    public Button Quit;  //Create a variable for the Quit Button
    private bool paused = false, waited = true;  //Create a paused and waited variable.

	public GameObject loadPanel; 

	private void Start () 
    {
        PauseMenu = PauseMenu.GetComponent<Canvas>(); //Getting the Pause Menu Component and placing it into the created variable
        QuitMenu = GetComponent<Canvas>(); //Getting the Quit Menu Component and placing it into the created variable
        Resume = Resume.GetComponent<Button>(); //Getting the Resume Button Component and placing it into the created variable
        Restart = Restart.GetComponent<Button>(); //Getting the Restart Button Component and placing it into the created variable
        Quit = Quit.GetComponent<Button>(); //Getting the Quit Button Component and placing it into the created variable
	   
        QuitMenu.enabled = false; //Disabling the Quit Menu Canvas on Load
        PauseMenu.enabled = false; //Disabling the Pause Menu Canvas on Load

	}

    private void waiting()
    {
        waited = true;
    }
  

    private void Update()
    {
        if(waited)
       // if(Input.GetKey(KeyCode.Escape) || Input.GetKey(KeyCode.P)) //if the escape key is pressed
        {
			Debug.Log("Pause menu is workings");
			loadPanel.SetActive(true);
           // PauseMenu.enabled = true;
            if(paused)
              paused= false; //if game is paused, unpause it
            else
                paused= true;//if game is unpaused, pause it 

            waited = false;
            Invoke("waiting", 0.3f);            
        }

        if (paused)
           Time.timeScale = 0; //Set the Game timescale to 0. Pauses the game at that time
        else
            Time.timeScale = 1; //Set the Game timescale to 1. Plays the game        
    }

    public void RestartGame()
    {
        Application.LoadLevel(10);//Reload the level
    }

    public void ResumeGame()
    {
        paused = false; //unpause the game
        PauseMenu.enabled = false; //disable pause menu
        
    }

    public void QuitButton()
    {
        PauseMenu.enabled = false; //disable pause menu
        QuitMenu.enabled = true; //enable quit menu
    }

    public void YesButton()
    {
        Application.LoadLevel(1);//Goes to the main menu
    }

    public void NoButton()
    {
        PauseMenu.enabled = true; //enable pause menu
        QuitMenu.enabled = false; //disable quit menu
    }
   
}