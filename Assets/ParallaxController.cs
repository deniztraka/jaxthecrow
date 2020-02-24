using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private FreeParallax parallax;
    public PlayerMovement movement;
    // Start is called before the first frame update
    void Start()
    {        
        parallax = gameObject.GetComponent<FreeParallax>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (parallax != null)
        {
            parallax.Speed = movement.GetHorizontalMove() * -0.01f;
            //Debug.Log(movement.GetHorizontalMove());
        }
    }
}
