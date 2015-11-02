using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{

    private Shader healthIndicator; 

    private EnemyAI EnemyAI;

    //private enemyController enemyController;

	void Start () 
    {
   
        EnemyAI =gameObject.GetComponentInParent<EnemyAI>();  // referencing the enemy script
      
	}
	
	
	void Update () 
    {
        Health();
	}


    public void Health()
    {
        //goblin health
		if (gameObject.name == "GoblinCube")
		{
			if (EnemyAI.goblinHealth > 79f) {
				gameObject.GetComponent<Renderer> ().material.color = Color.green; //if health is between 80-100 cube colour will be green.
           
			} else if (EnemyAI.goblinHealth > 39f) {
				gameObject.GetComponent<Renderer> ().material.color = Color.yellow;// if health is between 40-79 cube will change colour to yellow
           
			} else {
				gameObject.GetComponent<Renderer> ().material.color = Color.red; //if health is between 0-39 the cube will change colour to red
           
			}

			if (EnemyAI.goblinHealth == 0) { //if the health is 0 the enemy and the bar disappear
				Destroy (gameObject, 2);
			}
		}

        //cyclop health
		if (gameObject.name == "CyclopCube")
		{
			if (EnemyAI .cyclopHealth > 69) { //if the health is between 70-150 the cube bar becomes green
				gameObject.GetComponent<Renderer> ().material.color = Color.green;
          
			} else if (EnemyAI.cyclopHealth > 39) { //if the health is between 40-69 the cube bar changes to yellow
				gameObject.GetComponent<Renderer> ().material.color = Color.yellow;
      
			} else { ////if the health is between 0-39 the cube bar changes to red
				gameObject.GetComponent<Renderer> ().material.color = Color.red;
          
			}

			if (EnemyAI.cyclopHealth == 0) { //if the health is 0 the enemy and the bar disappear
				Destroy (gameObject, 1);
			}
		}


        //troll health
		if(gameObject.name=="TrollCube")
		{
        if (EnemyAI.trollHealth > 99) //if health is 100-200 the health cube is green
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
          
        }

        else if (EnemyAI.trollHealth > 59) //if health is 60-99 cube changes colour to yellow
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        
        }

        else
        {
            gameObject.GetComponent<Renderer>().material.color = Color.red;  //if health is between 0-59 the health cube will change colour to red
         
        }

        if (EnemyAI.trollHealth <= 0) //if the health is 0 the enemy and the bar disappear
        {
            Destroy(gameObject, 2);
        }
		
		}
	}
}
