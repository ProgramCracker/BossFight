using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TurretMove : MonoBehaviour
{


    [SerializeField] private LayerMask groundMask;


    [SerializeField] Camera mainCamera;


    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        Aim();
    }

    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // direction math
            var direction = position - transform.position;

            direction.y = 0; // no height

            transform.forward = direction;
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

