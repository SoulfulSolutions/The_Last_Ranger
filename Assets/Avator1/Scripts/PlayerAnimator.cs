using UnityEngine;
using System.Collections;

using Globals;
public class PlayerAnimator : MonoBehaviour
{

    #region Public Fields & Properties
    
    #endregion

    #region Private Fields & Properties
	private float _airVelocity;

	private Animator _animator;
	private CharacterController _playerController;
    #endregion

    #region Getters & Setters

    #endregion

    #region System Methods
    // Use this for initialization
    private void Start()
    {
		_animator = GetComponent<Animator> ();
		_playerController = GetComponent<CharacterController> ();
    }

    // Update is called once per frame
    private void Update()
    {
		_animator.SetBool (AnimatorCondition.Grounded,_playerController.isGrounded);
		_animator.SetFloat (AnimatorCondition.Speed,Input.GetAxis(PlayerInput.Vertical));
		_animator.SetFloat (AnimatorCondition.Direction,Input.GetAxis(PlayerInput.Horizontal));

		if(Input.GetKey(KeyCode.UpArrow))
		
	
		if (_playerController.isGrounded) 
		{
			_airVelocity=0f;
		} else 
		{ 
			_airVelocity-=Time.time;
		}

		_animator.SetFloat (AnimatorCondition.AirVerlocity,_airVelocity);
    }
    #endregion

    #region Custom Methods

    #endregion
}
