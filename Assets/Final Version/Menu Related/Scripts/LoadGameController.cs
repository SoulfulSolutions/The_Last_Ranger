using UnityEngine;
using System.Collections;

public class LoadGameController : MonoBehaviour {

	public MenuTransitionController CurrentMenu;
	public MenuManager Menu;

	public GameObject loadGamePanel;
	public GameObject menuObject;

	public void Awake()
	{
		loadGamePanel.SetActive (false);
	}
	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CancelShowLoadGame()
	{
		menuObject.SetActive (true);
		Menu.ShowMenu(CurrentMenu);
		loadGamePanel.SetActive (false);
	}

	public void ShowLoadGame()
	{
		loadGamePanel.SetActive (true);
		Debug.Log("dsfdsfdsffdsdd");
		menuObject.SetActive (false);
		
		
	}
}
