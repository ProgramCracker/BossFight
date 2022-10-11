using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDuration : MonoBehaviour
{
    [SerializeField] float _time;
    void Start()
    {
        Destroy(gameObject, _time);
    }

}
