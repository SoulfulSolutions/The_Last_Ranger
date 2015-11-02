using UnityEngine;
using System.Collections;

public class HorseAnimator : MonoBehaviour {


    Animation _horse;
    //CharacterController _gor;

    // Use this for initialization
    void Start()
    {
        _horse = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        _horse.CrossFade("grazing");
    }
}
