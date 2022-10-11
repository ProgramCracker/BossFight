using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIncrease : CollectibleBase
{
    [SerializeField] float _speedAmount = .05f;

    protected override void Collect(PlayInput player)
    {

        TankController controller = player.GetComponent<TankController>();
        if (controller != null)
        {
            controller.MaxSpeed += _speedAmount;
            Debug.Log("Current Speed: " + controller.MaxSpeed);
        }
    }

    protected override void Movement(Rigidbody rb)
    {
        Quaternion turnOffset = Quaternion.Euler
            (MovementSpeed, MovementSpeed, MovementSpeed);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
}
