using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slower : Enemy
{
    [SerializeField] public float _speedDecrease;

    //private float _effectDuration = 3;

    protected override void PlayerImpact(Player player)
    {
        TankController controller = player.GetComponent<TankController>();
        if (controller != null)
        {
            controller.MaxSpeed -= _speedDecrease;
            Debug.Log("Current Speed: " + controller.MaxSpeed);
        }
    }
}
