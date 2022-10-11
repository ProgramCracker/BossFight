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
    /*
    private void OnTriggerEnter(Collider other)
    {
        HealthBase impacted = other.gameObject.GetComponent<HealthBase>();

        if (other.gameObject.GetComponent<HealthBase>())
        {
            impacted.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
    */

  
    //Issue with bullets hitting the player's collider and kill them
    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.TryGetComponent(out IHealthBase health))
        {

            health.TakeDamage(_damage);
        }
        /*
        GameObject player = GameObject.Find("Player");
        Collider playerCollider = player.GetComponent<Collider>();

        if (other != playerCollider) {
            Debug.Log("it's the player");
           
        }
        */

        Destroy(gameObject);
    }
        
}
