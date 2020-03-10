using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableGrasshoper : Collectable
{
    // Start is called before the first frame update
    private CharacterController2D controller;
    public float MovementSpeed = 20;
    public float HorizontalMove = 0;

    private bool isJumped = false;
    private bool wasJumped = false;
    private bool wasOnLand = false;

    private void Awake() {
        Random.InitState(System.Environment.TickCount);
    }

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
        InvokeRepeating("Jump", Random.Range(0, 5), 1);        
        controller.OnLandEvent.AddListener(Landed);
    }

    void Update()
    {
        if (wasOnLand)
        {
            HorizontalMove = 0;
        }
        else
        {
            HorizontalMove = 1;
        }
    }

    void FixedUpdate()
    {
        // Move our character        
        controller.Move(HorizontalMove * MovementSpeed * Time.fixedDeltaTime, false, isJumped);
        isJumped = false;
    }

    void Jump()
    {        
        
        if (Random.Range(0, 100) > 90)
        {
            isJumped = true;
            HorizontalMove = 1;
        }
    }

    public void Landed(bool wasOnland)
    {
        this.wasOnLand = wasOnland;
    }

}
