using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class Player : HealthBase
{
    //[SerializeField] public int _maxHealth = 3;
    //public int _currentHealth;

    TankController _tankController;
    Inventory _inventory;



    private void Awake()
    {
        _tankController = GetComponent<TankController>();
        _inventory = GetComponent<Inventory>();
    }

    /*
    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's Health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Player's Health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }

    }

*/
    public void Points(int amount)
    {
        _inventory.GetPoints(amount);
    }


}
