using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IHealthBase : MonoBehaviour
{
    private float _damagetimer;
    [Header("Health Stats")]
    [SerializeField] public int _maxHealth;
    [SerializeField] public int _currentHealth;

    [Header("Damage Specifics")]
    [SerializeField] public GameObject _damagedForm;
    public float _damagedDuration = .05f;
    [SerializeField] AudioClip _damageSound;

    [Header("Death Specifics")]
    [SerializeField] ParticleSystem _deathParticles;
    [SerializeField] AudioClip _deathSound;

    PlayInput player;
    TankController tankController;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _damagedForm.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        StartCoroutine(DamagedVisual());
        DamageSound();

        Debug.Log(gameObject + " current health is:" + _currentHealth);
        if (_currentHealth <= 0)
        {
            DeathSound();
            DeathVisuals();
            Kill();
        }
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }
    


    /*
    public void DamageVisuals()
    {
        
    }
   

    void DamagedEffectStart()
    {
        _damagedForm.SetActive(true);
        Invoke("DamagedEffectStop", _damagedDuration);
    }

    
    void DamagedEffectStop()
    {
        _damagedForm.SetActive(false);
    }
    */

    void DamageSound()
    {
        AudioHelper.PlayClip2D(_damageSound, 1f);
    }

    IEnumerator DamagedVisual()
    {
        _damagedForm.SetActive(true);
        yield return new WaitForSeconds(_damagedDuration);
        _damagedForm.SetActive(false);
    }

    void DeathSound()
    {
        AudioHelper.PlayClip2D(_deathSound, 1f);
    }

    void DeathVisuals()
    {
        _damagedForm.SetActive(true);
        Instantiate(_deathParticles, transform.position, Quaternion.identity);
        
    }

    public void Kill()
    {

        gameObject.SetActive(false);
    }
}
