using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TankController : MonoBehaviour
{
    [SerializeField] float _turnSpeed = 2f;
    [SerializeField] Camera mainCamera;
    //[SerializeField] private LayerMask groundMask;
    //[SerializeField] public float _turretRotSpeed = 5f;
    [SerializeField] float _maxSpeed = .25f;

    Rigidbody _rb = null;
    public Transform _turretBase;

    //public UnityEvent<Vector3> OnMoveTurret = new UnityEvent<Vector3>();

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

    /*
   
    // GETTING mouse position on screen to be position in world
    private void GetTurretMovement()
    {
        OnMoveTurret?.Invoke(GetMousePos());
    }
    private Vector3 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.y = mainCamera.nearClipPlane;
        Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        return mouseWorldPosition;
    }


    public void TurnTurret(Vector3 mousePos)
    {
        var turretdirection = (Vector3)mousePos - _turretBase.position;

        var desiredAngle = Mathf.Atan2(turretdirection.y, turretdirection.x) * Mathf.Rad2Deg;

        var rotationPeriod = _turretRotSpeed * Time.deltaTime;

        _turretBase.rotation = Quaternion.RotateTowards(_turretBase.rotation, Quaternion.Euler(0,desiredAngle -90,0), rotationPeriod);
    }

    /*
    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }
    */
}
