using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move2D : MonoBehaviour
{
    public float movementSpeed;
    public float JumpForce = 0.1f;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private float playerSize;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = rb.GetComponent<BoxCollider2D>();
        playerSize = playerCollider.bounds.extents.y;
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 newPosition = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            newPosition.x -= movementSpeed;
            spriteRenderer.flipX = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            newPosition.x += movementSpeed;
            spriteRenderer.flipX = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            rb.velocity = Vector2.zero;

            rb.AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);
        }

        transform.position = newPosition;
    }

    bool IsOnGround()
    {
        Vector2 RaycastOrigin = transform.position;
        RaycastOrigin.y -= (playerSize - 0.01f);
        int layerMask = LayerMask.GetMask("Ground");

        return Physics2D.Raycast(RaycastOrigin, new Vector2(0.0f, -1.0f), 0.05f, layerMask);
    }
}
