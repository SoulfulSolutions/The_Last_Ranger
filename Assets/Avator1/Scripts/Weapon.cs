using UnityEngine;
using System.Collections;

using Globals;
public class Weapon : MonoBehaviour {

	#region Public Fields & Properties
	public float fireDelay;
	public float lerpSpeed=2;
	 AudioSource sound;
	public Transform gun;
	public Transform muzzle;

	public GameObject bulletHole;
	public GameObject muzzleFlash;

	public Camera cam;
	 
	int counter=0;

    #endregion

    #region Private Fields & Properties
	private float _fireCounter;

	private Ray _ray;
	private PlayerController _platerController;

    #endregion

    #region Getters & Setters

    #endregion

    #region System Methods
    // Use this for initialization
    private void Start()
    {
		_platerController=GetComponent<PlayerController>();
		sound=muzzle.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    private void LateUpdate()
    {
		_ray = cam.ScreenPointToRay (new Vector3(Screen.width*0.5f,Screen.height*0.5f,0f));

		if (_platerController.aim)
		{
			gun.forward = _ray.direction;
		} else 
		{
			gun.forward=transform.forward;
		}

		if(Input.GetButtonDown(PlayerInput.Fire1))
		{


			if(Input.GetButtonDown(PlayerInput.Fire1))
			{	if(Time.timeScale==1)
					sound.Play();
			}
			else
			{
				if(sound.isPlaying)
				sound.enabled=false;;
			}

			RaycastHit hit;
			if(_platerController.aim)
			{
				if(Physics.Raycast(_ray,out hit,100f))
				{
					Instantiate(bulletHole,hit.point,Quaternion.FromToRotation(Vector3.up,hit.normal));
				}
			}
			else
			{
				if(Physics.Raycast(muzzle.position,muzzle.forward,out hit,100f))
				{
					Instantiate(bulletHole,hit.point,Quaternion.FromToRotation(Vector3.up,hit.normal)); 
				}
			}
//
		StartCoroutine(MuzzleFlash());

		}
		_fireCounter += Time.deltaTime;
    }
    #endregion

    #region Custom Methods
    private IEnumerator MuzzleFlash()
	{
		GameObject go=(GameObject)(Instantiate(muzzleFlash,muzzle.position,Quaternion.Euler(muzzle.eulerAngles.x,muzzle.eulerAngles.y-90f,muzzle.eulerAngles.z)));
		go.transform.parent = muzzle;
		yield return new WaitForSeconds (go.GetComponent<ParticleSystem>().duration + 0.05f);
		Destroy (go);
	}
    #endregion
}
