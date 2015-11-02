using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class wave1stat : MonoBehaviour {
	
	private int noAnimalsDead = 0;
	private int noAnimalsSaved = 0;
	private int noEnermiesKilled = 0;
	GameObject boss;
	//	private float time = 0;
	
	public Text txtAniDead;
	public Text txtEneKilled;
	public Text txtscore;
	public Text txtanimSaved;

	public Text txtAnimals;
	public Text txtEnemies;


	// EnemyAI enemy;
	
	GameObject [] enemiesCount;
	GameObject[]  animalsCount;
	public GameObject statHolder;
	public PauseManager pauseManagerScript;
	private PlayerHealth health;
	int NoOfAnimals =0;
	int NoOfEnemies = 0;
	
	public Image warningimage;
	public Color flashcolour = new Color(1f,0f,0f, 0.2f);
	public GameObject bar;


	
	void Start()
	{
		health = GameObject.Find("Player").GetComponent<PlayerHealth>();
		if (Application.loadedLevelName == "Wave 1") {
			ScoreManager.score = 0;
			NoOfEnemies = GameObject.FindGameObjectsWithTag ("Goblin").Length;
		}
		else if (Application.loadedLevelName == "Wave 2")
			NoOfEnemies = GameObject.FindGameObjectsWithTag ("CyclopEnemy").Length;
		else if (Application.loadedLevelName == "Wave 3") {
			NoOfEnemies = GameObject.FindGameObjectsWithTag ("TrollEnemy").Length;
			
			boss = GameObject.Find("Boss Holder");
			boss.SetActive(false);
			bar.SetActive(false);
		}
		animalsCount = GameObject.FindGameObjectsWithTag ("Animal");
		
		NoOfAnimals = animalsCount.Length;

	}


	void Update()
	{
		
		wavecompl ();
		
		if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.E)) 
		{
			for (int i =0; i < enemiesCount.Length; i++)
			{
				Destroy(enemiesCount[i]);
			}
		}
		if (Application.loadedLevelName == "Wave 1") 
		{
			enemiesCount = GameObject.FindGameObjectsWithTag ("Goblin");
			animalsCount = GameObject.FindGameObjectsWithTag ("Animal");
			txtEneKilled.text = (NoOfEnemies - enemiesCount.Length).ToString();
			txtAniDead.text = (getNoAniDead() ).ToString();
			txtanimSaved.text = getNoAniSaved().ToString();
			txtscore.text = ScoreManager.score.ToString();
			txtAnimals.text ="Animals Killed : " +getNoAniDead().ToString();
			txtEnemies.text = "Enemies Killed : "+ (NoOfEnemies- enemiesCount.Length).ToString();
		}
		
		if (Application.loadedLevelName == "Wave 2") {
			
			enemiesCount = GameObject.FindGameObjectsWithTag ("CyclopEnemy");
			animalsCount = GameObject.FindGameObjectsWithTag ("Animal");
			txtEneKilled.text = (NoOfEnemies - enemiesCount.Length).ToString();
			txtAniDead.text = (getNoAniDead() ).ToString();
			txtscore.text =  ScoreManager.score.ToString();
			txtanimSaved.text = getNoAniSaved().ToString();
			txtAnimals.text ="Animals Killed : " +getNoAniDead().ToString();
			txtEnemies.text = "Enemies Killed : "+ (NoOfEnemies- enemiesCount.Length).ToString();
		}
		
		if (Application.loadedLevelName == "Wave 3") 
		{
			//boss.SetActive(false);
			enemiesCount = GameObject.FindGameObjectsWithTag ("TrollEnemy");
			animalsCount = GameObject.FindGameObjectsWithTag ("Animal");
			txtEneKilled.text = (NoOfEnemies - enemiesCount.Length).ToString();
			txtAniDead.text = (getNoAniDead()).ToString();
			txtscore.text = ScoreManager.score.ToString();
			txtanimSaved.text = getNoAniSaved().ToString();

			txtAnimals.text ="Animals Killed : " +getNoAniDead().ToString();
			txtEnemies.text = "Enemies Killed : "+ (NoOfEnemies- enemiesCount.Length).ToString();
			
			if (enemiesCount.Length <= 10)
			{
				boss.SetActive(true);
				bar.SetActive(true);
			}

			if (getNoAniDead () >= 8) {
				Application.LoadLevel ("Game Over");
			}
		}
		
		flash ();
	
	}
	
	public void flash()
	{
		if (getNoAniDead () > 8) 
		{
			warningimage.color = flashcolour;
			warningimage.color = Color.Lerp (warningimage.color, Color.clear, 5f * Time.deltaTime);
			
		}
	}
	
	
	public void setNoAnimalsS(int newNo)
	{
		noAnimalsSaved += newNo;
	}
	public void setNoAnimalsD(int newNo)
	{
		noAnimalsDead += newNo;
	}
	public void setEnermiesKilled(int newNo)
	{
		noEnermiesKilled += newNo;
	}
	
	public int getNoAniDead()
	{
		return noAnimalsDead;
	}
	
	public int getNoAniSaved()
	{
		return noAnimalsSaved;
	}
	public int getNoEnermKiiled()
	{
		return noEnermiesKilled;
	}
	int deadanimals1 = 0;
	public void wavecompl()
	{

		if(Application.loadedLevelName=="Wave 1")
			enemiesCount = GameObject.FindGameObjectsWithTag ("Goblin");
		else if(Application.loadedLevelName=="Wave 2")
			enemiesCount = GameObject.FindGameObjectsWithTag ("CyclopEnemy");
		else if(Application.loadedLevelName=="Wave 3")
			enemiesCount = GameObject.FindGameObjectsWithTag ("TrollEnemy");
		
//		Debug.Log("enemies count "+enemiesCount.Length);
		
		if (enemiesCount.Length ==0 || BossController.boss_Health <= 0) 
		{
			pauseManagerScript.setPause(true);
			pauseManagerScript.PauseMenu.gameObject.SetActive(false);
			Debug.Log("if enemies count "+enemiesCount.Length);
			Cursor.visible = true;
			statHolder.SetActive(true);

			Time.timeScale = 0;
		}
	}
	
	public void nextwave()
	{
		
		if (Application.loadedLevelName == "Wave 1") 
		{
			float temp = health.CurrentHealth;
			Application.LoadLevel("Wave 2");
			PlayerPrefs.SetFloat("health", temp);
		}
		
		if (Application.loadedLevelName == "Wave 2") 
		{
			Time.timeScale = 1;
			float temp = health.CurrentHealth;
			Application.LoadLevel("Wave 3");
			PlayerPrefs.SetFloat("health2", temp);
			Debug.Log("health : "+temp );
		}
		
		if (Application.loadedLevelName == "Wave 3") 
		{

			Application.LoadLevel("Win");
		}
	}
}