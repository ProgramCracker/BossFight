using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthBar : MonoBehaviour
{
    private Image healthBar;
    public float CurrentHealth;
    PlayInput Player;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        Player = FindObjectOfType<PlayInput>();
    }

    private void Update()
    {
        CurrentHealth = Player._currentHealth;
        healthBar.fillAmount = CurrentHealth / Player._maxHealth;
    }
}
