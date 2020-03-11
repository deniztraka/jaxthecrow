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

    private void Awake()
    {
        Random.InitState(System.Environment.TickCount);
    }

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();
        InvokeRepeating("Jump", Random.Range(0, 5), 3);
        controller.OnLandEvent.AddListener(Landed);
    }

    void Update()
    {
        if (wasOnLand && !isJumped)
        {
            HorizontalMove = 0;
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

        if (Random.Range(0, 100) > 20)
        {
            controller.SetJumpPower(Random.Range(10, 20));
            MovementSpeed = Random.Range(150, 300);
            isJumped = true;
            HorizontalMove = Random.Range(0, 100) > 50 ? -1 : 1;
        }
    }

    public void Landed(bool wasOnland)
    {
        this.wasOnLand = wasOnland;
    }

}
