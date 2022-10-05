using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControl : MonoBehaviour
{
    [SerializeField] public Transform _camTarget;

    [SerializeField] public float _camSpeed = 0.25f;
    [SerializeField] public Vector3 _camOffset;


    private void FixedUpdate()
    {
        Vector3 desiredPosition = _camTarget.position + _camOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _camSpeed);

        transform.position = smoothedPosition;

        transform.LookAt(_camTarget);

    }
}
