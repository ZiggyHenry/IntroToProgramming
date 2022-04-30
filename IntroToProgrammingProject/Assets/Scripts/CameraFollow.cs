using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float rotationSpeed;
    public float movementSpeed;
    public Vector3 offset;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPosition = target.position;

        newPosition = newPosition + (- target.forward * offset.z);
        newPosition.y += offset.y;

        transform.position = Vector3.MoveTowards(transform.position, newPosition,
           movementSpeed * Time.deltaTime);

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, target.forward,
            rotationSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
