using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnTouch : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] public float _timer;

    private void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.TryGetComponent(out IHealthBase health))
        {

            health.TakeDamage(_damage);
            if (other.gameObject.TryGetComponent(out PlayInput player))
            {
                player.DamageOverlay();
            }
        }

    }
}
