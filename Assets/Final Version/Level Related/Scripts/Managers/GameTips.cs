using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameTips : MonoBehaviour
{
	public GameObject checkload;
	public GameObject TipsHolder;

	public GameObject Display1;
	public GameObject Display2;
	
	public GameObject NextTip;
	public GameObject CloseTip;
	public GameObject Map;
	public bool checkGameTip;
	// Use this for initialization

	void Awake()
	{
		Cursor.visible = true;
		checkGameTip = true;
	}

	void Start ()
	{
		Display1.SetActive (true);
		Display2.SetActive (false);

		CloseTip.SetActive (false);

		NextTip.SetActive (true);
		Map.SetActive (false);
	}

	private void Update()
	{
		checkload.SetActive(true);
		Cursor.visible = true;
	}

	public void Pressed_NextTip()
	{
		Display1.SetActive (false);
		Display2.SetActive (true);

		CloseTip.SetActive (true);

		NextTip.SetActive (false);
	}

	public void Pressed_CloseTip()
	{
		TipsHolder.SetActive(false);
		checkGameTip = false;
		Map.SetActive (true);
	}

}
