using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCapsule : CollectibleBase
{


    [SerializeField] int _healthValue = 1;


    protected override void Collect(PlayInput player)
    {
        player.IncreaseHealth(_healthValue);
    }
}
