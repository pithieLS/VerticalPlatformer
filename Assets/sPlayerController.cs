using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayerController : MonoBehaviour
{
    // Components
    private Rigidbody2D rb;
    private BoxCollider2D playerCollider;

    // Jump related
    public float jumpForce = 4.0f;
    private int jumpCount = 0;
    private bool isGrounded = true;

    // Movement related
    public float playerSpeed = 5.0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {

    }

    void Update()
    {

        // Handle touch input
        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                HandleJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collisionTag = collision.gameObject.tag;

        if (collisionTag == "Ground" || collisionTag == "Wall")
            ResetJump();

        if(collisionTag == "Wall")
            HandleWallCollision();
    }

    private void HandleWallCollision()
    {
        // TODO
    }

    private void HandleJump()
    {
        if (jumpCount >= 2)
            return;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        jumpCount++;
    }

    private void ResetJump()
    {
        isGrounded = true;
        jumpCount = 0;
    }
}
