using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableWorm : Collectable
{
    // Start is called before the first frame update
    private CharacterController2D controller;
    public float MovementSpeed = 20;
    public float HorizontalMove = 0;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController2D>();        
    }
    void FixedUpdate()
    {
        // Move our character        
        controller.Move(HorizontalMove * MovementSpeed * Time.fixedDeltaTime, false, false);
    }

}
