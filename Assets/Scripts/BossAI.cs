using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    public NavMeshAgent _agent;

    public Transform _player;
    public Transform _muzzle;

    public LayerMask _LayerGround, _LayerPlayer;

    public GameObject _projectile;
    public float _projectileSpeed;

    public Vector3 _walkpoint;
    bool walkPointSet;
    public float _walkPointRange;

    public float _timeBetweenAttacks;
    bool alreadyAttacked;

    public float _sightRange, _attackRange;
    public bool _playerInSight, _playerInAttackRange;

    private void Awake()
    {
        _playerInAttackRange = GameObject.Find("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _playerInSight = Physics.CheckSphere(transform.position, _sightRange, _LayerPlayer);
        _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _LayerPlayer);

        if (!_playerInSight && !_playerInAttackRange) Patrol();
        {

        }
        if (_playerInSight && !_playerInAttackRange) Chase();
        {

        }
        if (_playerInSight && _playerInAttackRange) Attack();
        {

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
    }

    private void Chase()
    {
        _agent.SetDestination(_player.position);
    }

    private void Attack()
    {
        _agent.SetDestination(transform.position);

        transform.LookAt(_player);

        if (!alreadyAttacked)
        {
            //attack types//

            Vector3 bulletOrigin = _muzzle.position;
            Rigidbody rb = Instantiate(_projectile, bulletOrigin, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);
            //rb.AddForce(transform.forward * _projectileSpeed, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), _timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
