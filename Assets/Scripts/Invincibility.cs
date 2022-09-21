using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    public Material _InvincMat;
    public Material _BaseMat;

    protected override void PowerUp(Player player)
    {
        Enemy _enemy = gameObject.GetComponent<Enemy>();
        player.gameObject.GetComponentInChildren<MeshRenderer>().material = _InvincMat;
        _enemy._damageAmount = 0;
        

    }

    protected override void PowerDown(Player player)
    {
        Enemy _enemy = gameObject.GetComponent<Enemy>();
        player.gameObject.GetComponentInChildren<MeshRenderer>().material = _BaseMat;
        _enemy._damageAmount = 1;
        
    }

}
