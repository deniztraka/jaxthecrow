using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crow : MonoBehaviour
{

    public float MaxStamina = 15;
    [SerializeField]
    private int level = 1;
    public int Level
    {
        get { return level; }
        set
        {            
            level = value;
            OnLevelChanged.Invoke();
        }
    }

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
            stamina = value;
            OnStaminaChanged.Invoke();
        }
    }
    public float WingPower = 20;
    public float MovementSpeed = 40f;

    [SerializeField]
    private int collectableCount;
    public int CollectableCount
    {
        get { return collectableCount; }
        set
        {            
            collectableCount = value;
            OnCollectableCountChanged.Invoke();
        }
    }


    public UnityEvent OnStaminaChanged;
    public UnityEvent OnLevelChanged;
    public UnityEvent OnCollectableCountChanged;

    [SerializeField]
    private bool isOnLand = false;

    void Start()
    {
        characterController2D = gameObject.GetComponent<CharacterController2D>();
        characterController2D.OnLandEvent.AddListener(Landed);
        landedMovementSpeed = MovementSpeed;
    }

    internal void LevelUp()
    {
        Level++;
        CollectableCount = 0;
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
            Stamina = Stamina >= MaxStamina ? MaxStamina : Stamina;
        }
    }

    public void Landed(bool onLand)
    {
        isOnLand = onLand;
    }

    internal void Collect(Collectable collectable)
    {        
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
