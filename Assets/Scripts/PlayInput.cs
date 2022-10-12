using System.Collections;
using System.Collections.Generic;
using UnityEditor.Overlays;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TankController))]
public class PlayInput : IHealthBase
{
    //[SerializeField] public int _maxHealth = 3;
    //public int _currentHealth;
    private float _overlayFadeTimer;

    TankController _tankController;
    Inventory _inventory;

    public Image _overlay;
    public float _overlayDuration;
    public float _overlayFadeSpeed;

    

    private void Awake()
    {
        _damagedForm.SetActive(false);
        _currentHealth = _maxHealth;
        _tankController = GetComponent<TankController>();
        _inventory = GetComponent<Inventory>();

        _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, 0);
    }

    private void Update()
    {
        if (_overlay.color.a > 0)
        {
            _overlayFadeTimer += Time.deltaTime;
            if(_overlayFadeTimer > _overlayDuration)
            {
                float tempAlpha = _overlay.color.a;
                tempAlpha -= Time.deltaTime * _overlayFadeSpeed;
                _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, tempAlpha);
            }
        }
    }

    public void DamageOverlay()
    {
        _overlayDuration = 0;
        _overlay.color = new Color(_overlay.color.r, _overlay.color.g, _overlay.color.b, .5f);
        GameObject.Find("Main Camera").GetComponent<CamControl>()._cameraShake = true;
    }

    public void Points(int amount)
    {
        _inventory.GetPoints(amount);
    }


}
