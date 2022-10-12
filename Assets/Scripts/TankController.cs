using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float _turnSpeed = 2f;
    [SerializeField] Camera mainCamera;
    //[SerializeField] private LayerMask groundMask;
    //[SerializeField] public float _turretRotSpeed = 5f;
    [SerializeField] float _maxSpeed = .25f;

    [Header("Weapons")]
    Rigidbody _rb = null;
    public Transform _turretBase;

    //public UnityEvent<Vector3> OnMoveTurret = new UnityEvent<Vector3>();

    [SerializeField] Transform _bulletSpawnPoint;
    public GameObject bullet_1;

    [SerializeField] AudioClip _shootSound;
    [SerializeField] ParticleSystem _shootParticles;


    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value;
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Shoot();
    }

    private void FixedUpdate()
    {
        MoveTank();
        TurnTank();
        
        //GetTurretMovement();

    }

    public void MoveTank()
    {
        // calculate the move amount
        float moveAmountThisFrame = Input.GetAxis("Vertical") * _maxSpeed;
        // create a vector from amount and direction
        Vector3 moveOffset = transform.forward * moveAmountThisFrame;
        // apply vector to the rigidbody
        _rb.MovePosition(_rb.position + moveOffset);
        // technically adjusting vector is more accurate! (but more complex)
    }

    public void TurnTank()
    {
        // calculate the turn amount
        float turnAmountThisFrame = Input.GetAxis("Horizontal") * _turnSpeed;
        // create a Quaternion from amount and direction (x,y,z)
        Quaternion turnOffset = Quaternion.Euler(0, turnAmountThisFrame, 0);
        // apply quaternion to the rigidbody
        _rb.MoveRotation(_rb.rotation * turnOffset);
    }

    // projectiles
    public void Shoot()
    {
        float _bulletSpeed = bullet_1.GetComponent<BallBullet>()._speed;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShootSFX();
            ShootVFX();
            var bullet = Instantiate(bullet_1, _bulletSpawnPoint.position, _bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = _bulletSpawnPoint.forward * _bulletSpeed;
        }
    }


    void ShootSFX()
    {
        AudioHelper.PlayClip2D(_shootSound, 1f);
    }

    void ShootVFX()
    {
        Instantiate(_shootParticles, _bulletSpawnPoint.position, _bulletSpawnPoint.transform.rotation);
    }

}
