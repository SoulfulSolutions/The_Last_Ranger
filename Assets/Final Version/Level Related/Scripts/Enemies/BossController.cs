using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour {

    private float _distance;

    public static float boss_Health = 300f;

    public float LookAtDistance = 40.0f;
    public float ChaseRange = 30.0f;
    public float AttackRange = 1.5f;
    public float MoveSpeed = 5.0f;
    public float Damping = 6.0f;
    public float AttackRepeatTime = 2f;
    public float AttackDamage;

    private float _attackTime;

    public CharacterController Charcontroller;
    public float Gravity = 20.0f;
    private Vector3 _moveDirection = Vector3.zero;
    private Animation _anim;
    private Transform Target;

    Bullet_Hit_Boss boss;
	PlayerHealth player;
    Collider coli;
	NavMeshAgent nav;
    private AudioSource mutantSound;

    public void Start()
    {
        boss = GetComponent<Bullet_Hit_Boss>();
        _attackTime = Time.time;
        Target = GameObject.Find("Player").transform;
		player = GameObject.Find ("Player").GetComponent<PlayerHealth> ();
        _anim = GetComponent<Animation>();
        mutantSound = GetComponent<AudioSource>();
		nav = gameObject.GetComponent<NavMeshAgent> ();
    }

    public void Update()
    {
		_distance = Vector3.Distance(Target.position, transform.position);

		health ();

		if (_distance < AttackRange)
		{
			nav.enabled = false;
			Attack ();
		} 
		else 
		{
			nav.enabled = true;
			Move ();
		}        
    }

	void Move()
	{
		nav.SetDestination (Target.position);
		_anim.CrossFade("mutant_run_inPlace");
	}
	

    public void Attack()
    {
        if (Time.time > _attackTime)
        {
            if (boss_Health > 0)
            {
                if (gameObject.name == "Mutant")
                {
                    _anim.CrossFade("mutant_swiping");
					player.HandleHealth(AttackDamage);
                }
                _attackTime = Time.time + AttackRepeatTime;
            }

        }
    }

    public void health()
    {
        if (boss_Health <= 0)
        {
			nav.enabled = false;
			StartCoroutine("You_Win");
            _anim.CrossFade("mutant_dying");
            Destroy(gameObject, 1);
        }
    }

    void OnTriggerEnter(Collider g)
    {
        if (g.tag == "bullet")
        {
            if (gameObject.name == "Mutant")
                ScoreManager.score += 10;
                //mutantSound.Play();
        }
    }
	private IEnumerator You_Win()
	{
		yield return new WaitForSeconds (3);
		Application.LoadLevel ("Win");
	}
}
