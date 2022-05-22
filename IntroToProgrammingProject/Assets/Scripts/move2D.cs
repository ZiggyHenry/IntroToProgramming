using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class move2D : MonoBehaviour
{
    public float movementSpeed;
    public float JumpForce = 0.1f;
    public Transform spawnPoint;
    public Camera cam;
    public Slider healthBar;

    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;
    private SpriteRenderer spriteRenderer;
    private float playerSize;

    private Vector2 newPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = rb.GetComponent<BoxCollider2D>();
        playerSize = playerCollider.bounds.extents.y;
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        healthBar.value = 1.0f;
    }

    void Update()
    {
        newPosition = transform.position;

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

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("You Died");
        healthBar.value -= 0.1f;
        ResetPlayer();
    }

    private void ResetPlayer()
    {
        newPosition = spawnPoint.position;
        cam.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, cam.transform.position.z);
        rb.velocity = Vector2.zero;

        transform.position = newPosition; //Only useful for the OnTriggerEnter and not the R button
    }
}