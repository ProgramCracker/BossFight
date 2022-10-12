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
    [Header ("Rapid Fire")]
    [SerializeField] AudioClip _shootingsound;
    [SerializeField] ParticleSystem _shootingParticles;
    [SerializeField] AudioClip _proximityWarning;
    [SerializeField] GameObject _proxmityAlert;
    public float _rapidFireWarningTime;
    bool playedWarning;
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
        _proxmityAlert.SetActive(false);
        playedWarning = false;
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
            WarningSFX();
                Attack();

        }

        if (_playerInSight && _playerInAttackRange && _playerInCloseRange)
        {

            _proxmityAlert.SetActive(true);
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
            ShootSFX();
            ShootVFX();
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
        
        StartCoroutine(wait(_rapidFireWarningTime));

        if (!alreadyRapidAttacked)
        {
            //attack types//
            ShootSFX();
            ShootVFX();
            Vector3 bulletOrigin = _muzzle.position;

            Rigidbody rb = Instantiate(_projectile, bulletOrigin, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
            //rb.AddForce(transform.up * -8f, ForceMode.Impulse);


            alreadyRapidAttacked = true;
            Invoke(nameof(ResetRapidAttack), _timeBetweenRapid);
        }


    }

    void ShootSFX()
    {
        AudioHelper.PlayClip2D(_shootingsound, 1f);
        _proxmityAlert.SetActive(false);
    }

    void WarningSFX()
    {
        if (playedWarning == false)
        {

            StartCoroutine(soundwait(_rapidFireWarningTime));
        }
    }

    IEnumerator wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }

    IEnumerator soundwait(float seconds)
    {
        playedWarning = true;
        AudioHelper.PlayClip2D(_proximityWarning, 10);
        yield return new WaitForSeconds(seconds);
        playedWarning = false;
    }

    void ShootVFX()
    {
        Instantiate(_shootingParticles, _muzzle.position, _muzzle.rotation);
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
        
    }
    private void ResetRapidAttack()
    {
        alreadyRapidAttacked = false;
        
    }
}
