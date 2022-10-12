using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Video;

public abstract class Bullets : MonoBehaviour
{
    [SerializeField] public float _duration = 3;
    [SerializeField] public int _damage = 1;
    [SerializeField] public float _speed = 25;

    [SerializeField] ParticleSystem _hitParticles;
    [SerializeField] AudioClip _hitSound;


    


    Rigidbody rb;

    private void Awake()
    {
        Destroy(gameObject, _duration);
        
    }

    //Issue with bullets hitting the player's collider and kill them
    private void OnCollisionEnter(Collision other)
    {
        ImpactSFX();
        ImpactVFX();

        if (other.gameObject.TryGetComponent(out IHealthBase health))
        {

            health.TakeDamage(_damage);
        }

        Destroy(gameObject);
    }

    void ImpactSFX()
    {
        AudioHelper.PlayClip2D(_hitSound, 1f);
    }

    void ImpactVFX()
    {
        Instantiate(_hitParticles, gameObject.transform.position, Quaternion.identity);
    }
}
