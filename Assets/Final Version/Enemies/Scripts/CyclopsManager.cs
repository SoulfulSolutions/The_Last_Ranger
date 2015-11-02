using UnityEngine;
using System.Collections;

public class CyclopsManager : MonoBehaviour
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject Enemy;                // The enemy prefab to be spawned.
    public Transform[] SpawnPoints;// An array of the spawn points this enemy can spawn from.


    public GoblinManager _goblin;
    
    public int CyclopsCounter = 0;
    public int DeadGoblins = 0;
    public bool flag = true;

    void Start()
    {
      // _goblin = _goblin.GetComponent<GoblinManager>();
        DeadGoblins = _goblin.GoblinCounter;
    }

    void Update()
    {
        if (DeadGoblins == 0)
        {
            if (flag == true)
            {
                DetermineToSpawn();
                flag = false;    
            }
            
        }
    }

    void SpawnCyclops()
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
        for (int i = 1; i <= 3; i++)
        {
            Invoke("SpawnCyclops", 5);
            CyclopsCounter++;

            if (CyclopsCounter == 5)
            {
                Debug.Log("Cyclops limit for wave is reached");
                break;
            }
            Debug.Log("Spawn more cyclops");
        }
    }
}
