using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    Rigidbody rb;

    private Vector3 movementDir;
    private Vector3 newPosition;
    private Vector2 lastMousePosition;
    public float rotationSpeed;
    private bool isGrounded;
    private float playerSize;
    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        CapsuleCollider playerCollider = GetComponent<CapsuleCollider>();
        playerSize = playerCollider.bounds.extents.y;
        lastMousePosition = Input.mousePosition;
    }

    private void FixedUpdate()
    {
        Vector3 moveRight = transform.right * movementDir.x;
        Vector3 moveForward = transform.forward * movementDir.z;

        Vector3 newPosition = transform.position + ((moveRight + moveForward) * speed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    bool checkGround()
    {
        Vector3 raycastOrigin = transform.position;
        raycastOrigin.y -= (playerSize - 0.1f);
        int layerMask = LayerMask.GetMask("Ground");
        return Physics.Raycast(raycastOrigin, new Vector3(0.0f, -1.0f, 0.0f), 0.2f, layerMask);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 rotationAmount = mousePosition - lastMousePosition;
        isGrounded = checkGround();

        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            Vector3 force = new Vector3(0.0f, jumpForce, 0.0f);
            rb.AddForce(force, ForceMode.Impulse);
        }

        Vector3 capsuleRotate = new Vector3(0.0f, rotationAmount.x * rotationSpeed * Time.deltaTime, 0.0f);
        transform.Rotate(capsuleRotate);

        movementDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movementDir.z = 1.0f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            movementDir.x = -1.0f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementDir.z = -1.0f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementDir.x = 1.0f;
        }

        lastMousePosition = mousePosition;
    }
}