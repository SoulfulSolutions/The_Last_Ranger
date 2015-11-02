using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
//sbu
public class PlayerHealth : MonoBehaviour
{
    private float _cachedY; //check
    private float _minXValue; //minimum value of the health
    private float _maxXValue; //maximum value of the health
    public  float CurrentHealth; // health that the avatar currently have.
    private float _startingHealth = 100; //health that the avatar will have before getting in contact with the damage/enemy.

    public RectTransform HealthTransform; //moves the health bar in order to reduce the  health.
    public Image Health;// health bar that will decrease when damage have occured
    public Text HealthText; //Text(in number format) of the health

    public bool currHealthContr = false;
	 Animation _anim;
		void Start()
    {

        _cachedY = HealthTransform.position.y;
        _maxXValue = HealthTransform.position.x;
        _minXValue = HealthTransform.position.x - HealthTransform.rect.width;
        CurrentHealth = _startingHealth; //before any damage occur current health should be the same as the starting health.
        
        Health = Health.GetComponent<Image>();
        HealthText = HealthText.GetComponent<Text>();
        HealthTransform = HealthTransform.GetComponent<RectTransform>();

		if (Application.loadedLevelName == "Wave 2")
		{
			CurrentHealth = PlayerPrefs.GetFloat ("health");
			Debug.Log ( "this side "+PlayerPrefs.GetFloat ("health"));
		}

		else if (Application.loadedLevelName == "Wave 3")
		{
			CurrentHealth = PlayerPrefs.GetFloat ("health2");
			PlayerPrefs.DeleteKey("health2");
			Debug.Log("ths side "+CurrentHealth);
		}

		Health.fillAmount = CurrentHealth; //the health bar and current health move simultaneously
		HealthText.text = "Health: " + Convert.ToInt16(CurrentHealth); //control the health text.
		float currentXValue = Values(CurrentHealth, 0, _startingHealth, _minXValue, _maxXValue);
		HealthTransform.position = new Vector3(currentXValue, _cachedY);
		Health.color = new Color32((byte)Values(CurrentHealth, _startingHealth / 2, _startingHealth, 255, 0), 255, 0, 255);
		if(Application.loadedLevelName == "Wave 3")
		{
			_anim = GameObject.Find ("Mutant").GetComponent<Animation> ();  
		}
		
    }

    //updating the method update with methods that add functionality 
    void Update()
    {
		if (Time.timeScale == 1)
		{
		    Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
		} else
			Cursor.lockState = CursorLockMode.None;

		if (CurrentHealth <= 0)
        {
			StartCoroutine("You_Died");
        }

		if (currHealthContr == true)
		{
			Health.fillAmount = CurrentHealth; //the health bar and current health move simultaneously
            HealthText.text = "Health: " + Convert.ToInt16(CurrentHealth); //control the health text.
            float currentXValue = Values(CurrentHealth, 0, _startingHealth, _minXValue, _maxXValue);
            HealthTransform.position = new Vector3(currentXValue, _cachedY);
            Health.color = new Color32((byte)Values(CurrentHealth, _startingHealth / 2, _startingHealth, 255, 0), 255, 0, 255);
            currHealthContr = false;
        } 

    }
    
	private IEnumerator You_Died()
	{
		yield return new WaitForSeconds (1);
		HealthText.enabled = false;
		Application.LoadLevel ("Game Over");
	}

	public void ActivateLifePerk() //function to handle hea
	{
		// Reduce the current health by the damage amount.
		CurrentHealth = 100f; 
		
		Health.fillAmount = CurrentHealth; //the health bar and current health move simultaneously
		HealthText.text = "Health: " + Convert.ToInt16(CurrentHealth); //control the health text.
		float currentXValue = Values(CurrentHealth, 0, _startingHealth, _minXValue, _maxXValue);
		HealthTransform.position = new Vector3(currentXValue, _cachedY);
		
		//changing the health bar colour as the health decreases.
		if (CurrentHealth > _startingHealth / 2) // 50% -100% health
		{
			Health.color = new Color32((byte)Values(CurrentHealth, _startingHealth / 2, _startingHealth, 255, 0), 255, 0, 255);
		}
		
		else//less that 50% health
		{
			Health.color = new Color32(255, (byte)Values(CurrentHealth, 0, _startingHealth / 2, 0, 255), 0, 255);
		}
		
	}
   public void RestartGame()
    {
        Application.LoadLevel(5);
    }
    public void QuitGame()
    {
        Application.LoadLevel(0);
    }

    public void HandleHealth(float damageAmount) //function to handle hea
    {
        // Reduce the current health by the damage amount.
        if(CurrentHealth > 0)
        {
            CurrentHealth -= damageAmount;

			if (CurrentHealth <= 0)
			{
				CurrentHealth = 0;
			}
        }   

        Health.fillAmount = CurrentHealth; //the health bar and current health move simultaneously
        HealthText.text = "Health: " + Convert.ToInt16(CurrentHealth); //control the health text.
        float currentXValue = Values(CurrentHealth, 0, _startingHealth, _minXValue, _maxXValue);
        HealthTransform.position = new Vector3(currentXValue, _cachedY);

        //changing the health bar colour as the health decreases.
        if (CurrentHealth > _startingHealth / 2) // 50% -100% health
        {
            Health.color = new Color32((byte)Values(CurrentHealth, _startingHealth / 2, _startingHealth, 255, 0), 255, 0, 255);
        }

        else//less that 50% health
        {
            Health.color = new Color32(255, (byte)Values(CurrentHealth, 0, _startingHealth / 2, 0, 255), 0, 255);
        }

    }

    public float Values(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

}
