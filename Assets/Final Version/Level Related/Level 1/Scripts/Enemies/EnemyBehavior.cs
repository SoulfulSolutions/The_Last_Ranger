using UnityEngine;
using System.Collections;

public class EnemyBehavior : MonoBehaviour 
{
    private float _distance;
    private float _attackTime;
    private Transform _target;
    private Vector3 _moveDirection = Vector3.zero;
    private PlayerHealth _playerHealth;
    private AudioSource _enemySource;
    private Animation _playAnimation;

    public float LookAtDistance = 25.0f;
    public float ChaseRange = 15.0f;
    public float AttackRange = 1.5f;
    public float MoveSpeed = 5.0f;
    public float Damping = 6.0f;
    public float AttackRepeatTime = 2f;
    public float AttackDamage;
    public float Gravity = 20.0f;
    public float HitPoints;

    public CharacterController Charcontroller;

    public AnimationClip IdleAnimationClip;
    public AnimationClip MoveAnimationClip;
    public AnimationClip AttackAnimationClip;
    public AnimationClip DeathAnimationClip;

    Bullet_hit _bullet;
    Collider _enemyCollider;

    public void Start()
    {
        _enemySource = GetComponent<AudioSource>();
        _enemySource.Stop();

        _bullet = GetComponent<Bullet_hit>();

        _attackTime = Time.time;

        _playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
        _target = GameObject.Find("Player").transform;

        _playAnimation = GetComponent<Animation>();
    }

    public void Update()
    {
        EnemyHealth();

            _distance = Vector3.Distance(_target.position, transform.position);

            if (_distance < LookAtDistance)
            {
                LookAt();
            }
            if (_distance < AttackRange)
            {
                Attack();
            }
            else
                if (_distance < ChaseRange)
                {
                    Chase();
                }
    }

    public void LookAt()
    {
        var rotation = Quaternion.LookRotation(_target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);

        _playAnimation.CrossFade(IdleAnimationClip.name);

    }

    public void Chase()
    {
        if (HitPoints >= 0)
        {
            // if ((gameObject.name == "GOBLIN(Clone)") || (gameObject.name == "Troll(Clone)") || (gameObject.name == "cyclop_soldier(Clone)"))
            {
                _playAnimation.CrossFade(MoveAnimationClip.name);

                _moveDirection = transform.forward;
                _moveDirection *= MoveSpeed;

                _moveDirection.y -= Gravity * Time.deltaTime;
                Charcontroller.Move(_moveDirection * Time.deltaTime);
            }
        }

    }

    public void Attack()
    {
        if (Time.time > _attackTime)
        {
            _playerHealth.HandleHealth(AttackDamage);

            if (HitPoints > 0)
            {
                //if ((gameObject.name == "GOBLIN(Clone)") || (gameObject.name == "Troll(Clone)") || (gameObject.name == "cyclop_soldier(Clone)"))
                _playAnimation.CrossFade(AttackAnimationClip.name);
                _attackTime = Time.time + AttackRepeatTime;
            }


        }
    }

    public void ApplyDammage()
    {
        ChaseRange += 30;
        MoveSpeed += 2;
        LookAtDistance += 40;
    }

    public void EnemyHealth()
    {
        if (HitPoints <= 0)
        {
            _playAnimation.CrossFade(DeathAnimationClip.name);
            Destroy(gameObject, 4);
        }
    }

    void OnTriggerEnter(Collider g)
    {
        if (g.tag == "bullet")
        {
            _enemySource.Play();
        }
    }
	
}

