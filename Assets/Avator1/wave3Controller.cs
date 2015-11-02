using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class wave3Controller : MonoBehaviour
{
	public GameObject trollHolder;

	GameObject [] _trollCount;
	public GameObject boss;

	public void Awake()
	{
	}

    void Start()
    {
		boss.SetActive(false);
    }



    void Update()
    {
		_trollCount = GameObject.FindGameObjectsWithTag ("TrollEnemy");
        ManageWaves();
    }

    void ManageWaves()
    {
		if (_trollCount.Length == 0) 
		{
			if(boss!=null)
			boss.SetActive(true);

//			Destroy(GameObject.Find("DestroyTroll"));
//			Debug.Log("You win screen must appear here pam");
//            Application.LoadLevel(12);
		} 
    }

   
}
