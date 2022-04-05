using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed * Time.deltaTime);

        rb.MovePosition(newPosition);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
