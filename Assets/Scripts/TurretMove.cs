using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TurretMove : MonoBehaviour
{

    [SerializeField] private LayerMask groundMask;
    [SerializeField] public float _turnSpeed = .5f;


    [SerializeField] Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Aim(_turnSpeed);
    }

    private void Aim(float Speed)
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // direction math
            var direction = (position - transform.position);

            //direction.y = 0; // no height

            float turnSpeed = Speed * Time.deltaTime;

            transform.forward = direction;
            //transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, position.y, 0), turnSpeed);

            //transform.localRotation = Quaternion.Euler(aimSpeed, direction.y,aimSpeed);

        }
    }

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

}

