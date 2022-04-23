using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletSpawn : MonoBehaviour
{
    public GameObject prefab;
    public float firingRate;
    private float currentTime = 0.0f;
    public float bulletForce = 30.0f;

    void Start()
    {
        
    }

    void Update()
    {
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