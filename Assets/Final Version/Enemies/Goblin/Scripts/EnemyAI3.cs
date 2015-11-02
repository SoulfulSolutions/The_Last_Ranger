//using System;
//using UnityEngine;
//using System.Collections;
//
//public class EnemyAI3 : MonoBehaviour {
//
//	
//
////var lookAround01 : MouseLook;
////var lookAround02 : MouseLook;
//public CharacterController charController;
//
//public Transform respawnTransform;
//
//public static bool playerIsDead = false;
//
//public void Start () 
//{
//	//lookAround01 = gameObject.GetComponent(MouseLook);
//	//lookAround02 = GameObject.Find("MainCamera").GetComponent(MouseLook);
//	charController = gameObject.GetComponent<CharacterController>();
//}
//
//public void Update ()
//{
//	if (playerIsDead == true)
//	{
////		lookAround01.enabled = false;
////		lookAround02.enabled = false;
//		charController.enabled = false;
//	}
//}
//
///*public void OnGUI ()
//{
//	if (playerIsDead == true)
//	{
//		if (GUI.Button(Rect(Screen.width*0.5-50, 200-20, 100, 40), "Respawn"))
//		{
//			RespawnPlayer();
//		}
//		
//		if (GUI.Button(Rect(Screen.width*0.5-50, 240, 100, 40), "Menu"))
//		{
//			Debug.Log("Return to Menu");
//		}
//	}
//}*/
//
////public void RespawnPlayer ()
////{
////	transform.position = respawnTransform.position;
////	transform.rotation = respawnTransform.rotation;
////	gameObject.SendMessage("RespawnStats");
//////	lookAround01.enabled = true;
//////	lookAround02.enabled = true;
////	charController.enabled = true;
////	playerIsDead = false;
////	Debug.Log("Player has respawned");
////}
//
//  
//}
