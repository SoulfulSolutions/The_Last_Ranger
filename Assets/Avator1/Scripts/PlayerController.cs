using UnityEngine;
using System.Collections;

using Globals;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HeadLookController))]
[RequireComponent(typeof(CharacterMotor))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Weapon))]
public class PlayerController : MonoBehaviour
{

    #region Public Fields & Properties
	public float IdleTimer;

	public float runSpeed=4.6f;
	public float runStrafeSpeed=3.07f;
	public float walkSpeed=1.22f;
	public float walkStrafeSpeed=1.22f;
	public float maxRotationSpeed=540f;



	//public variables that are hedden in the inspectore

	[HideInInspector]
	public float targetYRotaion;
	[HideInInspector]
	public bool walk;
	[HideInInspector]
	public bool inAir;
	[HideInInspector]
	public bool grounded;
	[HideInInspector]
	public bool aim;

	[HideInInspector]
	public Vector3 MoveDir;


    #endregion

    #region Private Fields & Properties
	private Animation anim;
	private Transform _playerTransform;

	private CharacterController _controller;

	private CharacterMotor _motor;

    #endregion

    #region Getters & Setters

    #endregion

    #region System Methods
    // Use this for initialization
    private void Start()
    {
		anim=GetComponent<Animation>();
		IdleTimer = 0f;

		_playerTransform = transform;

		walk = true;
		aim = false;

		_controller = GetComponent<CharacterController> ();
		_motor = GetComponent<CharacterMotor> ();

		_controller.center = new Vector3 (0f,1f,0f);
    }

    // Update is called once per frame
    private void Update()
    {
		GetUserInput ();

		if (!_motor.CanControl) 
		{//this is coming from the charecter motor script
			_motor.CanControl=true;
		}

		MoveDir = new Vector3 (Input.GetAxis (PlayerInput.Horizontal), 0f, Input.GetAxis (PlayerInput.Vertical));

		if (MoveDir.sqrMagnitude > 1f)
			MoveDir = MoveDir.normalized;

		_motor.InputMoveDirection = _playerTransform.TransformDirection (MoveDir);
		_motor.InputJump = Input.GetButtonDown (PlayerInput.Jump);

	    _motor.movement.MaxForwardSpeed = (walk) ? walkSpeed : runSpeed;
		_motor.movement.MaxBackwardsSpeed = _motor.movement.MaxForwardSpeed;
		_motor.movement.MaxSidewaysSpeed = (walk) ? walkStrafeSpeed : runStrafeSpeed;
	

		if (MoveDir != Vector3.zero)
			IdleTimer = 0f;

		inAir = !_motor.grounded;
		grounded = !inAir;

		float currentAngle = _playerTransform.localRotation.eulerAngles.y;
		float delta = Mathf.Repeat ((targetYRotaion - currentAngle), 360f);

		if (delta > 180f)
			delta -= 360f;

		float newRot = Mathf.MoveTowards (currentAngle, currentAngle + delta, Time.deltaTime * maxRotationSpeed);
		Vector3 newLocalRot = new Vector3 (_playerTransform.localRotation.eulerAngles.x,newRot,_playerTransform.localRotation.eulerAngles.z);
		_playerTransform.localRotation=Quaternion.Euler(newLocalRot);

	


    }
    #endregion

    #region Custom Methods
    private void GetUserInput()
	{
		aim = Input.GetButton (PlayerInput.Fire2);

		IdleTimer += Time.deltaTime;

		walk = (!Input.GetButton (PlayerInput.Run) || MoveDir == Vector3.zero || Input.GetAxis (PlayerInput.Vertical) < 0f);

	}
    #endregion
}
