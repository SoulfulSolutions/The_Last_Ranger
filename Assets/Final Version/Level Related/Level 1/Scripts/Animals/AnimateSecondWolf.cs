using UnityEngine;
using System.Collections;

public class AnimateSecondWolf : MonoBehaviour {

    // Use this for initialization

    Animation _wolf;
    //CharacterController _gor;

    // Use this for initialization
    void Start()
    {
        _wolf = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        _wolf.CrossFade("eating");
    }
}
