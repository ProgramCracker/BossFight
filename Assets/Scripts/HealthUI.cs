using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    private Image _healthBar;
    public float _currentHealth;
    public float _maxHealth;
    [SerializeField] IHealthBase _targetHealth;
    

    private void Awake()
    {
        _healthBar = GetComponent<Image>();
        _maxHealth = _targetHealth._maxHealth;
        _currentHealth = (_maxHealth) * 1f;
    }

    private void FixedUpdate()
    {
        _currentHealth = _targetHealth._currentHealth;
        _healthBar.fillAmount = _currentHealth / _maxHealth;
    }
}
