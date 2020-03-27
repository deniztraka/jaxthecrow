using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBush : MonoBehaviour, IHurtful
{
    [SerializeField]
    private int damageDone;
    public int DamageDone
    {
        get { return damageDone; }
        set { damageDone = value; }
    }

    public void TakeDamage(IDamageable damagableObj)
    {
        damagableObj.GetDamage(damageDone);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var damagableObj = other.gameObject.GetComponent<IDamageable>();
        if (damagableObj != null)
        {
            TakeDamage(damagableObj);
        }
    }
}
