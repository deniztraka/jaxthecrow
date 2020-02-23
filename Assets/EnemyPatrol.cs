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
        if (gameObject.transform.position.x < LeftPoint.position.x)
        {
            worm.HorizontalMove = 1;
        }
        else if (gameObject.transform.position.x > RightPoint.position.x)
        {
            worm.HorizontalMove = -1;
        }
    }

    void OnDrawGizmosSelected()
    {

#if UNITY_EDITOR
        Gizmos.color = Color.red;

        //Draw the suspension
        Gizmos.DrawLine(
            Vector3.zero,
            Vector3.up
        );

        //draw force application point
        Gizmos.DrawWireSphere(Vector3.zero, 0.05f);

        Gizmos.color = Color.white;
#endif
    }
}
