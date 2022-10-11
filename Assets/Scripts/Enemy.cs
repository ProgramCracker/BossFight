using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : IHealthBase
{
    [SerializeField] public int _damageAmount = 1;
    [SerializeField] ParticleSystem _impactParticles;
    [SerializeField] AudioClip _impactSound;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        IHealthBase health = other.gameObject.GetComponent<IHealthBase>();
        PlayInput player = other.gameObject.GetComponent<PlayInput>();
        if(player != null)
        {
            Impact(_damageAmount);
            ImpactFeedback();
        }
    }
    
    protected virtual void Impact(int _damageAmount)
    {
        TakeDamage(_damageAmount);
    }

    private void ImpactFeedback()
    {
        if (_impactParticles != null)
        {
            _impactParticles = Instantiate(_impactParticles, transform.position, Quaternion.identity);

        }

        if (_impactSound != null)
        {
            AudioHelper.PlayClip2D(_impactSound, 1f);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        
    }
}
