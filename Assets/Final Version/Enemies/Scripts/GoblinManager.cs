using UnityEngine;
using System.Collections;

public class GoblinManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject Enemy;                // The enemy prefab to be spawned.
    public Transform[] SpawnPoints;
    public float spawnTime = 5f;
    // An array of the spawn points this enemy can spawn from.
    public int GoblinCounter = 0;
  

    void Start()
    {

        // Call the Spawn function after a delay of the spawnTime and then continue to call after the same amount of time.
        // InvokeRepeating("SpawnGoblin", spawnTime, 3);
        DetermineToSpawn();
    }

    void Update()
    {
       
        //Invoke("SpawnGoblin",3);
        //GoblinCounter += 3;
        //Debug.Log(GoblinCounter);

        //if (GoblinCounter == 12)
        //{
        //    Debug.Log("inside the if statement");
        //}
    }

    void SpawnGoblin()
    {
        // If the player has no health left...
        if (playerHealth.CurrentHealth <= 0f)
        {
            // ... exit the function.
            return;
        }

        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, SpawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(Enemy, SpawnPoints[spawnPointIndex].position, SpawnPoints[spawnPointIndex].rotation);
    }

    void DetermineToSpawn()
    {
        for (int i = 1; i <= 5; i++)
        {
            Invoke("SpawnGoblin",5);
            GoblinCounter ++;

            if (GoblinCounter == 5)
            {
                Debug.Log("Goblin limit for wave is reached");
                break;
            }
            Debug.Log("Spawn more goblins");
        }
    }
}
