using UnityEngine;
using System.Collections;

public class perkSpin : MonoBehaviour {

	// Update is called once per frame
	void Update () {

        transform.Rotate(0, 90 * Time.deltaTime, 0);//Rotating around the z axiz
        
	
	}
}
