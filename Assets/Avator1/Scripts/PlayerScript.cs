using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PlayerScript : MonoBehaviour
{
    public float speed;  //for the movement of the avatar
    public RectTransform healthTransform; //moves the health bar in order to reduce the  health.
    private float cachedY; //check
    private float minXValue; //minimum value of the health
    private float maxXValue; //maximum value of the health
    public Text healthText; //Text(in number format) of the health
    public float startingHealth = 100; //health that the avatar will have before getting in contact with the damage/enemy.
    public float currentHealth; // health that the avatar currently have.
    public Image health;// health bar that will decrease when damage have occured
    //private int damage;
    //bool damaged;

    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            currentHealth = value;
            //HandleHealth();
        }
    }

    

    void Start()
    {


        cachedY = healthTransform.position.y;
        maxXValue = healthTransform.position.x;
        minXValue = healthTransform.position.x - healthTransform.rect.width;
        currentHealth = startingHealth; //before any damage occur current health should be the same as the starting health.
        health = GetComponent<Image>();
    }


    //updating the method update with methods that add functionality 
    void Update()
    {
        //HandleMovement();  //function for avatar movement
        //HandleHealth(); // function to handle health
    }


    
    public void HandleHealth(float damageAmount) //function to handle hea
    {
        // Reduce the current health by the damage amount.
        if(currentHealth > 0)
        {
            currentHealth -= damageAmount;

        }
        Debug.Log("Attack Method");
        health.fillAmount = currentHealth; //the health bar and current health move simultaneously

        healthText.text = "Health: " + Convert.ToInt16(currentHealth); //control the health text.
        float currentXValue = Values(currentHealth, 0, startingHealth, minXValue, maxXValue);
        healthTransform.position = new Vector3(currentXValue, cachedY);


        //changing the health bar colour as the health decreases.
        if (currentHealth > startingHealth / 2) // 50% -100% health
        {
            health.color = new Color32((byte)Values(currentHealth, startingHealth / 2, startingHealth, 255, 0), 255, 0, 255);
        }

        else//less that 50% health
        {
            health.color = new Color32(255, (byte)Values(currentHealth, 0, startingHealth / 2, 0, 255), 0, 255);
        }

        //// Set the damaged flag so the screen will flash.
        //damaged = true;

       

        // Set the health bar's value to the current health.
       // health = currentHealth;
    }

    //public void HandleMovement() //Funtion for avatar's movement
    //{
    //    float transaction = speed * Time.deltaTime; 
    //    transform.Translate(new Vector3(Input.GetAxis("Horizontal") * transaction, 0, Input.GetAxis("Vertical") * transaction));

    //}

   // Function for the damage
    void OnTriggerStay(Collider other)
    {

        if (currentHealth > 0)
            if (other.name == "GOBLIN") //if damage have took place.
            {
                if (currentHealth <= startingHealth) // if the current health is less than the starting health.
                {
                    currentHealth = currentHealth - 0.5f;
                }
            }
    }

    private float Values(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

    public void applyDamage()
    {
    }
}
