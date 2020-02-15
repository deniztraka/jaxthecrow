using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableWorm : Collectable
{
    // Start is called before the first frame update


    public override void Use()
    {
        Debug.Log("you got a worm.");
    }

}
