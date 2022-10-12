using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CamControl : MonoBehaviour
{
    [SerializeField] public Transform _camTarget;

    [SerializeField] public float _camSpeed = 0.25f;
    [SerializeField] public Vector3 _camOffset;

    public float _shakeDuration = 1f;
    public bool _cameraShake = false;
    public AnimationCurve _curve;

    private void FixedUpdate()
    {
        Vector3 desiredPosition = _camTarget.position + _camOffset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _camSpeed);

        transform.position = smoothedPosition;

        transform.LookAt(_camTarget);

        if (_cameraShake)
        {
            _cameraShake = false;
            StartCoroutine(Shake());
        }

    }

    public IEnumerator Shake()
    {
        float timeElapsed = 0f;
        Vector3 originalPos = transform.localPosition;

        

        while (timeElapsed < _shakeDuration)
        {
            timeElapsed += Time.deltaTime;
            float intensity = _curve.Evaluate(timeElapsed / _shakeDuration);
            transform.position = originalPos + UnityEngine.Random.insideUnitSphere * intensity;
            yield return null;
        }

        transform.position = originalPos;
    }
}
