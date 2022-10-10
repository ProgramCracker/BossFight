using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullets : MonoBehaviour
{
    [SerializeField] float _duration = 3;
    [SerializeField] int _damage = 1;

    [SerializeField] ParticleSystem _hitParticles;
    [SerializeField] AudioClip _hitSound;

    

    Rigidbody rb;

    private void Awake()
    {
        Destroy(gameObject, _duration);
    }

    private void OnCollisionEnter(Collision collision)
    {
     

        if (collision.gameObject.TryGetComponent(out HealthBase health))
        {
            health.DecreaseHealth(_damage);
        }
        Destroy(gameObject);
    }
}
