using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    public float CurrentHealth;
    Player Player;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        Player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        CurrentHealth = Player._currentHealth;
        healthBar.fillAmount = CurrentHealth / Player._maxHealth;
    }
}
