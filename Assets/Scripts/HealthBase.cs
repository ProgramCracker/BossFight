using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{ 
    [SerializeField] int _maxHealth;
    [SerializeField] int _currentHealth;

    Player player;
    TankController tankController;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void DecreaseHealth(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
