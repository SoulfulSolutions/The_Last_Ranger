using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour 
{

    private float _cachedY; //check
    private float _minXValue; //minimum value of the health
    private float _maxXValue; //maximum value of the health
    public float CurrentHealth; // health that the avatar currently have.
    private float _startingHealth = 100; //health that the avatar will have before getting in contact with the damage/enemy.

    public Image Health;// health bar that will decrease when damage have occured
    public RectTransform HealthTransform; //moves the health bar in order to reduce the  health.
   

    void Start()
    {
        _cachedY = HealthTransform.position.y;
        _maxXValue = HealthTransform.position.x;
        _minXValue = HealthTransform.position.x - HealthTransform.rect.width;
        CurrentHealth = _startingHealth; //before any damage occur current health should be the same as the starting health.

        Health = Health.GetComponent<Image>();
       
        HealthTransform = HealthTransform.GetComponent<RectTransform>();
    }

   
    public void HandleHealth(float damageAmount) //function to handle hea
    {
        // Reduce the current health by the damage amount.
        if (CurrentHealth > 0)
        {
            CurrentHealth -= damageAmount;
        }

        Health.fillAmount = CurrentHealth; //the health bar and current health move simultaneously
        float currentXValue = Values(CurrentHealth, 0, _startingHealth, _minXValue, _maxXValue);
        HealthTransform.position = new Vector3(currentXValue, _cachedY);

    }

    private float Values(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }


	
}
