using UnityEngine;
using System.Collections;

public class AnimateThirdWolf : MonoBehaviour {

    // Use this for initialization

    Animation _wolf;
    //CharacterController _gor;

    // Use this for initialization
    void Start()
    {
        _wolf = GetComponent<Animation>();
        //_gor = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _wolf.CrossFade("sniff");
    }
}
