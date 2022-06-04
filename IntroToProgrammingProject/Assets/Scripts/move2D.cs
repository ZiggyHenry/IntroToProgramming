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
    private CircleCollider2D playerCollider;
    private SpriteRenderer sp;

    private float playerSize;
    private bool isGrounded;
    private Vector2 movementDir;

    private bool flipX;
    private bool isMoveRight = false;
    private bool isMoveLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        playerSize = playerCollider.bounds.extents.y;
        sp = GetComponent<SpriteRenderer>();
        healthBar.value = 1.0f;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector3 moveForward = transform.right * movementDir.x * movementSpeed * Time.fixedDeltaTime;
        rb.AddForce(moveForward, ForceMode2D.Impulse);
    }

    void Update()
    {
        isGrounded = checkGround();
        movementDir = Vector2.zero;

        if (Input.GetKey(KeyCode.A) || isMoveLeft)
        {
            movementDir.x = -1.0f;
        }

        if (Input.GetKey(KeyCode.D) || isMoveRight)
        {
            movementDir.x = 1.0f;
        }

        flipX = false;
        if (rb.velocity.x < 0)
        {
            flipX = true;
        }
        sp.flipX = flipX;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.zero;

            rb.AddForce(new Vector2(0.0f, JumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPlayer();
        }
    }

    bool checkGround()
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
        cam.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, cam.transform.position.z);
        rb.velocity = Vector2.zero;

        transform.position = spawnPoint.position;
    }

    public void MoveLeft()
    {
        isMoveLeft = true;
        isMoveRight = false;
    }

    public void MoveRight()
    {
        isMoveRight = true;
        isMoveLeft = false;
    }
}