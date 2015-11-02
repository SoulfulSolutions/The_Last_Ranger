using UnityEngine; 
using UnityEngine.UI;
using System.Collections;

public class PerksManager : MonoBehaviour 
{
	public GameObject[] Perks;
	GameObject lPerk;
	GameObject pntPerk;
	GameObject powPerk;

	int PerkIndex;

	// Use this for initialization
	void Start ()
	{
	    for (int i=0; i<Perks.Length; i++) 
		{
			Perks[i].SetActive(false);
		}

		lPerk = Perks [0];
		pntPerk = Perks [1];
		powPerk = Perks [2];

		//StartCoroutine ("Run_Show_Perks");
		//lPerk.SetActive(false);
		//pntPerk.SetActive(false);
		//powPerk.SetActive(false);

		InvokeRepeating ("ShowPerks",15,15);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//StartCoroutine ("Run_Show_Perks");
	}

	//IEnumerator Run_Show_Perks()
	//{
		//yield return new WaitForSeconds (5);
		//ShowPerks ();
	//}

	public void ShowPerks()
	{
		PerkIndex = Random.Range (-1, Perks.Length);
		
		if (PerkIndex == 0) 
		{
			lPerk.SetActive(true);
			pntPerk.SetActive(false);
			powPerk.SetActive(false);
		}
		
		else
			if (PerkIndex == 1) 
		{
			lPerk.SetActive(false);
			pntPerk.SetActive(true);
			powPerk.SetActive(false);
		}
		else
			if (PerkIndex == 2) 
		{
			lPerk.SetActive(false);
			pntPerk.SetActive(false);
			powPerk.SetActive(true);
		}
	}
}
