using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int _points = 0;

    public int _LAmmo = 3;

    public void GetPoints(int amount)
    {
        _points += amount;
        Debug.Log("Points: " + _points);
    }

    public void GetAmmo(int amount)
    {

    }

}
