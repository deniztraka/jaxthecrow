using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField]
    private float maxStamina = 100;
    private bool isAscending;
    public float Stamina = 100;
    public float WingPower = 20;
    public float MovementSpeed = 40f;
    public int CollectableCount;

    [SerializeField]
    private bool isOnLand = false;    

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Stamina <= maxStamina && !isAscending)
        {
            Stamina += 0.1f;
            Stamina = Stamina > maxStamina ? maxStamina : Stamina;
        }        
    }

    public void Landed()
    {
        
    }

    internal void Collect(Collectable collectable)
    {
        collectable.Use();
        CollectableCount++;
    }

    public void Flap()
    {        
        Stamina -= WingPower;
    }

    public void AscendingStatusChanged(bool isAscending)
    {
        this.isAscending = isAscending;
    }
}
