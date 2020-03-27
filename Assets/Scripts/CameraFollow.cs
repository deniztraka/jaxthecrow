﻿using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float maxHeight;
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;
    // Use this for initialization
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;
            //posNoZ.y = target.transform.position.y;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 5f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);
            var targetFinalPosition = targetPos + offset;
            if (targetFinalPosition.y > maxHeight)
            {
                targetFinalPosition.y = maxHeight;
            }

            transform.position = Vector3.Lerp(transform.position, targetFinalPosition, 0.25f);

        }
    }
}