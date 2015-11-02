using UnityEngine;
using System.Collections;

public class GorillaAnimator : MonoBehaviour
{

    Animation _gorilla;
    //CharacterController _gor;

    // Use this for initialization
    void Start()
    {
        _gorilla = GetComponent<Animation>();
        //_gor = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _gorilla.CrossFade("Howl");
    }
}
