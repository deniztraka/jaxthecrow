using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerAnimal : MonoBehaviour
{
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log(other.gameObject.name + " : " + gameObject.name + " : " + Time.time);
            GameObject.Destroy(other.gameObject);
            
        }

    }
}
