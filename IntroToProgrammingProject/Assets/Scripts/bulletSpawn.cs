using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour
{
    public GameObject prefab;
    public float firingRate;
    public float bulletForce = 30.0f;

    private float currentTime = 0.0f;

    public Transform target;
    public float rotationSpeed;

    void Start()
    {

    }

    void Update()
    {
        Vector3 targetDir = target.position - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDir, 
            rotationSpeed * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        currentTime += Time.deltaTime;
        if (currentTime >= firingRate)
        {
            GameObject obj = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
            
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * bulletForce, ForceMode.Impulse);
            currentTime = 0.0f;
        }
    }
}