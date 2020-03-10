using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform RightPoint;
    public Transform LeftPoint;

    private CollectableWorm worm;

    // Start is called before the first frame update
    void Start()
    {
        worm = gameObject.GetComponent<CollectableWorm>();
        worm.HorizontalMove = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (LeftPoint != null && gameObject.transform.position.x < LeftPoint.position.x)
        {
            worm.HorizontalMove = 1;
        }
        else if (RightPoint != null && gameObject.transform.position.x > RightPoint.position.x)
        {
            worm.HorizontalMove = -1;
        }
    }
}
