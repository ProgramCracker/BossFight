using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : CollectibleBase
{
    [SerializeField] float Value = 1;

    protected override void Collect(Player player)
    {

    }
}
