using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Enemy : HealthBase
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
        HealthBase health = other.gameObject.GetComponent<HealthBase>();
        PlayInput player = other.gameObject.GetComponent<PlayInput>();
        if(player != null)
        {
            Impact(_damageAmount);
            ImpactFeedback();
        }
    }
    
    protected virtual void Impact(int _damageAmount)
    {
        DecreaseHealth(_damageAmount);
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
