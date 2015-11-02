using UnityEngine;
using System.Collections;

public class TrollManager : MonoBehaviour 
{
    public PlayerHealth playerHealth;       // Reference to the player's heatlh.
    public GameObject Enemy;                // The enemy prefab to be spawned.
    public Transform[] SpawnPoints;  // An array of the spawn points this enemy can spawn from.

    public CyclopsManager _cyclops;
   
    public int TrollCounter = 0;
    public int DeadCyclops = 0;
    public bool flag = true;

    void Start()
    {
        //_cyclops = _cyclops.GetComponent<CyclopsManager>();
        DeadCyclops = _cyclops.CyclopsCounter;
    }

    void Update()
    {
        if (DeadCyclops == 0)
        {
            if (flag == true)
            {
                DetermineToSpawn();
                flag = false;
            }

        }
    }

    void SpawnTroll()
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
            Invoke("SpawnTroll", 5);
            TrollCounter++;

            if (TrollCounter == 5)
            {
                Debug.Log("Troll limit for wave is reached");
                break;
            }
            Debug.Log("Spawn more Trolls");
        }
    }
}
