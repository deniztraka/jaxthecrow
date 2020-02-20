using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crow : MonoBehaviour
{

    public float MaxStamina = 15;
    private bool isAscending;

    private CharacterController2D characterController2D;

    [SerializeField]
    private float stamina = 15;
    private float landedMovementSpeed = 0;

    public float Stamina
    {
        get { return stamina; }
        set
        {
            OnStaminaChanged.Invoke();
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
        characterController2D = gameObject.GetComponent<CharacterController2D>();
        characterController2D.OnLandEvent.AddListener(Landed);
        landedMovementSpeed = MovementSpeed;
    }

    void Update()
    {
        if (isOnLand)
        {
            MovementSpeed = landedMovementSpeed;
        }
        else
        {
            MovementSpeed = landedMovementSpeed * 1.5f;
        }
    }

    void FixedUpdate()
    {
        //if (Stamina <= MaxStamina && !isAscending)
        if (Stamina <= MaxStamina && isOnLand)
        {
            Stamina += 0.1f;
            Stamina = Stamina > MaxStamina ? MaxStamina : Stamina;
        }
    }

    public void Landed(bool onLand)
    {
        isOnLand = onLand;
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
        //this.isAscending = isAscending;
    }
}
