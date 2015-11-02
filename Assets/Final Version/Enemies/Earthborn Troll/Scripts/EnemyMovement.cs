//using UnityEngine;
//using System.Collections;
//
//public class EnemyMovement : MonoBehaviour
//{
//
//     Transform player;
//	// Use this for initialization
//
//    NavMeshAgent nav;
//
//    void Awake()
//    {
//        player = GameObject.FindGameObjectWithTag ("Player").transform;
//
//        nav = GetComponent<NavMeshAgent>();
//    }
//
//    void Start () {
//	
//
//	}
//	
//	// Update is called once per frame
//	void Update ()//nav mesh does not keep up with physics
//	{
//	    nav.SetDestination(player.position);
//
//	   // nav.enabled = false;
//
//	}
//}
