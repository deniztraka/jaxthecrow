using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crow : MonoBehaviour
{
    
    public float MaxStamina = 15;
    private bool isAscending;

    [SerializeField]
    private float stamina = 15;
    public float Stamina
    {
        get { return stamina; }
        set
        {
            if(value != stamina){
                OnStaminaChanged.Invoke();
            }
            stamina = value;
        }
    }
    public float WingPower = 20;
    public float MovementSpeed = 40f;
    public int CollectableCount;

    public UnityEvent OnStaminaChanged;

    [SerializeField]
    private bool isOnLand = false;

    void Start()
    {

    }

    void FixedUpdate()
    {
        if (Stamina <= MaxStamina && !isAscending)
        {
            Stamina += 0.1f;
            Stamina = Stamina > MaxStamina ? MaxStamina : Stamina;
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
