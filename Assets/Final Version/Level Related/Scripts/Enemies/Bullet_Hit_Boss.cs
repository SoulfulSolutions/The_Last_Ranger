using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bullet_Hit_Boss : MonoBehaviour {
    
    private GameObject bullet;
    GameObject gameobject;

    public static int damage = 10;

    private float _cachedY; //check
    private float _minXValue; //minimum value of the health
    private float _maxXValue; //maximum value of the health
    //public  float  CurrentHealth; // health that the avatar currently have.
    private float startingHealth = 300f;

    //health that the avatar will have before getting in contact with the damage/enemy.

    public Image Health;// health bar that will decrease when damage have occured
    public RectTransform HealthTransform; //moves the health bar in order to reduce the  health.
   
    // Update is called once per frame
    void Start()
    {
       // startingHealth = GameObject.FindGameObjectWithTag("Boss").GetComponent<BossController>().boss_Health;

        bullet = GameObject.Find("BulletHole(Clone)");
        _cachedY = HealthTransform.position.y;
        _maxXValue = HealthTransform.position.x;
        _minXValue = HealthTransform.position.x - HealthTransform.rect.width;

       // CurrentHealth = BossController.boss_Health; //before any damage occur current health should be the same as the starting health.

        Health = Health.GetComponent<Image>();

        HealthTransform = HealthTransform.GetComponent<RectTransform>();
    }
    
    void Update()
    {
        Destroy(GameObject.Find("BulletHole(Clone)"), 0.2f);
    }
         
    public void OnTriggerEnter(Collider thecollider)
    {
        if (thecollider.tag == "bullet")
        {
            
            if (gameObject.name == "bulletDetectBoss")
            {
                Debug.Log("you have shot the box " + gameObject.name);

                BossController.boss_Health -= damage;
                Debug.Log(" " + BossController.boss_Health);

                Health.fillAmount = BossController.boss_Health; //the health bar and current health move simultaneously
                float currentXValue = Values(BossController.boss_Health, 0, startingHealth, _minXValue, _maxXValue);
                HealthTransform.position = new Vector3(currentXValue, _cachedY);
            }

        }
    }


    private float Values(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }

}
