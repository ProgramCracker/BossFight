using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : MonoBehaviour
{
    protected abstract void PowerUp(Player player);


    [SerializeField] public float _powerUpDuration;

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        MeshRenderer rend = other.GetComponent<MeshRenderer>();
        Collider collider = other.gameObject.GetComponent<Collider>();

        if (player != null)
        {
            PowerUp(player);

            rend.enabled = false;
            collider.enabled = false;

            Duration();

            PowerDown();
        }
    }

    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(_powerUpDuration);
    }

    private void PowerDown()
    {
        gameObject.SetActive(false);
    }
}
