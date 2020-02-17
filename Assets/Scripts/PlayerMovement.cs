using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    private Crow crow;



    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;

    void Start()
    {
        crow = GameObject.FindGameObjectWithTag("Player").GetComponent<Crow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform != RuntimePlatform.Android)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * crow.MovementSpeed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.SetJumpPower(crow.WingPower * 10);
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    public void Jump()
    {
        if (crow.Stamina >= crow.WingPower)
        {
            jump = true;
        }
    }

    public void MoveLeftPressed()
    {
        horizontalMove = -1 * crow.MovementSpeed;
    }

    public void MoveRightPressed()
    {
        horizontalMove = 1 * crow.MovementSpeed;
    }

    public void MoveButtonReleased()
    {
        horizontalMove = 0;
    }

    public float GetHorizontalMove(){
        return horizontalMove;
    }
}
