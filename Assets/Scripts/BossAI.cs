using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    [Header("Identifiers")]
    public NavMeshAgent _agent;
    public Transform _player;
    public Transform _muzzle;

    public LayerMask _LayerGround, _LayerPlayer;

    [Header("Weapons")]
    public GameObject _projectile;
    public float _projectileSpeed;
    public float _timeBetweenAttacks;
    bool alreadyAttacked;
    //[SerializeField] GameObject _explosion;
    [SerializeField] AudioClip _rapidFireShots;
    [SerializeField] ParticleSystem _rapidFireShoot;
    bool alreadyRapidAttacked;
    public float _timeBetweenRapid;

    [Header("Movement")]
    public Vector3 _walkpoint;
    bool walkPointSet;
    public float _walkPointRange;

    public float _sightRange, _attackRange, _closeRange;
    public bool _playerInSight, _playerInAttackRange, _playerInCloseRange;

    private void Awake()
    {
        _playerInAttackRange = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
        //_explosion.SetActive(false);
    }

    private void Update()
    {
        _playerInSight = Physics.CheckSphere(transform.position, _sightRange, _LayerPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _LayerPlayer);
        _playerInCloseRange = Physics.CheckSphere(transform.position, _closeRange, _LayerPlayer);

        if (!_playerInSight && !_playerInAttackRange && !_playerInCloseRange) 
        {
            Patrol();
        }
        if (_playerInSight && !_playerInAttackRange && !_playerInCloseRange) 
        {
            Chase();
        }
        if (_playerInSight && _playerInAttackRange && !_playerInCloseRange) 
        {
            Attack();
        }

        if (_playerInSight && _playerInAttackRange && _playerInCloseRange)
        {
            RapidFire();
        }
    }

    private void Patrol()
    {
        if(!walkPointSet)
        {
            LookForWalkPoint();
        }

        if (walkPointSet)
        {
            _agent.SetDestination(_walkpoint);
        }

        Vector3 distanceToWalkPoint = transform.position - _walkpoint;

        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void LookForWalkPoint()
    {
        float ranZ = Random.Range(-_walkPointRange, _walkPointRange);
        float ranx = Random.Range(-_walkPointRange, _walkPointRange);

        _walkpoint = new Vector3(transform.position.x + ranx, transform.position.y, transform.position.z + ranZ);

        if (Physics.Raycast(_walkpoint, -transform.up, 2f,_LayerGround))
        {
            walkPointSet = true;
        }
        transform.LookAt(_walkpoint);
    }

    private void Chase()
    {
        transform.LookAt(_player);
        _agent.SetDestination(_player.position);
    }

    private void Attack()
    {
        transform.LookAt(_player);
        _agent.SetDestination(transform.position);

        

        if (!alreadyAttacked)
        {
            //attack types//
            
            Vector3 bulletOrigin = _muzzle.position;
            Rigidbody rb = Instantiate(_projectile, bulletOrigin, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
            rb.AddForce(transform.up * -2f, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void RapidFire()
    {
        transform.LookAt(_player);
        _agent.SetDestination(transform.position);

        if (!alreadyRapidAttacked)
        {
            //attack types//

            Vector3 bulletOrigin = _muzzle.position;
            Rigidbody rb = Instantiate(_projectile, bulletOrigin, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
            //rb.AddForce(transform.up * -8f, ForceMode.Impulse);


            alreadyRapidAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenRapid);
        }

        //ExplosionTimer();

    }

    /*
    IEnumerator ExplosionTimer()
    {
        _explosion.SetActive(true);
        Rigidbody rb = Instantiate(_explosion, gameObject.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        yield return new WaitForSeconds(_explosion.GetComponent<HurtOnTouch>()._timer);
        _explosion.SetActive(false);
    }
    */

    private void ResetAttack()
    {
        alreadyAttacked = false;
        alreadyRapidAttacked = false;
    }
}
