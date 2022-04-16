using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour
{
    public GameObject prefab;
    public float firingRate;
    private float currentTime = 0.0f;

    void Start()
    {
        
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= firingRate)
        {
            Vector3 spawnPosition = transform.position + (transform.forward * 1.2f);
            GameObject obj = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity);

            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 500.0f);
            currentTime = 0.0f;
        }
    }
}