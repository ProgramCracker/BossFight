using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(PlayInput player);
    protected abstract void PowerDown(PlayInput player);

    [SerializeField] public float _powerUpDuration;
    [SerializeField] float _movementSpeed = 1;
    protected float MovementSpeed => _movementSpeed;

    [SerializeField] ParticleSystem _collectParticles;
    [SerializeField] AudioClip _collectSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Movement(_rb);
    }

    protected virtual void Movement(Rigidbody rb)
    {

        Quaternion turnOffset = Quaternion.Euler(0, _movementSpeed, 0);
        rb.MoveRotation(_rb.rotation * turnOffset);
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        PlayInput player = other.gameObject.GetComponent<PlayInput>();

        if (player != null)
        {
            PowerUp(player);

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            yield return new WaitForSeconds(_powerUpDuration);

            PowerDown(player);
        }
    }

}
