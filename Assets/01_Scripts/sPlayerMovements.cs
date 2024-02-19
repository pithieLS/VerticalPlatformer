using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using UnityEngine;

public class sPlayerController : MonoBehaviour
{
    // Components
    private BoxCollider2D playerCollider;
    private Rigidbody2D rb;
    private Animator playerAnimator;

    // Jump related
    public float jumpForce = 4.0f;
    private int jumpCount = 0;

    // Movement related
    public float playerSpeed = 5.0f;
    public float GravityScale = 1.0f;
    [SerializeField] public LayerMask groundLayer;
    [SerializeField] public LayerMask wallLayer;
    private float currentSpeed = 0.0f;
    private int wallLayerNumber;
    private int groundLayerNumber;
    private int playerDirection = 1;
    private bool isWallSliding = false;
    private bool isGrounded = false;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        rb.gravityScale = GravityScale;

        // Reverse bit shifting from the layermask value
        wallLayerNumber = (int)(Mathf.Log(wallLayer.value) / Mathf.Log(2));
        groundLayerNumber = (int)(Mathf.Log(groundLayer.value) / Mathf.Log(2));
    }

    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
            if (Input.GetTouch(0).phase == TouchPhase.Began)
                HandleJump();

        if (isWallSliding)
            if (!CheckIfOnWall())
                SetWallSliding(false);

        //CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        //Debug.Log(isWallSliding + "//" + isGrounded);
        //Debug.Log(currentSpeed);
        MovePlayer();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for wall collision
        LayerMask collisionLayer = collision.gameObject.layer;
        if(collisionLayer == wallLayerNumber)
            HandleWallCollision(collision);
        if (collisionLayer == groundLayerNumber && CheckIfGrounded(collision))
            HandleGroundCollision();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        LayerMask collisionLayer = collision.gameObject.layer;
        if (collisionLayer == wallLayerNumber)
        {
            if(!CheckIfGrounded(collision))
                SetGrounded(false);
        }
        if (collisionLayer == groundLayerNumber)
            SetGrounded(false);
    }

    private void MovePlayer()
    {
        rb.velocity = new Vector2(currentSpeed * playerDirection, rb.velocity.y);
    }

    private void HandleWallCollision(Collision2D collision)
    {
        // Check if collision isn't from the sides
        foreach (ContactPoint2D contact in collision.contacts)
        {
            Vector2 normal = contact.normal;
            if (Vector2.Dot(normal, Vector2.up) > 0.9f)
            {
                Debug.Log("Collision is from below");
                HandleGroundCollision();
                return;
            }
            else if (Vector2.Dot(normal, Vector2.down) > 0.9f)
            {
                Debug.Log("Collision is from above");
                return;
            }
        }

        playerDirection *= -1;

        // Rotate sprite in relation to it's direction
        float newScaleX = Math.Abs(transform.localScale.x) * playerDirection;
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);

        if(!isGrounded)
            SetWallSliding(true);

        ResetJump();
    }

    private void HandleGroundCollision()
    {
        SetGrounded(true);
        SetWallSliding(false);

        rb.gravityScale = GravityScale;
        ResetJump();
    }

    private void HandleJump()
    {
        if (jumpCount >= 2)
            return;

        jumpCount++;

        rb.gravityScale = GravityScale;
        currentSpeed = playerSpeed;

        playerAnimator.SetInteger("JumpCount", jumpCount);
        SetGrounded(false);
        SetWallSliding(false);

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

    }

    private void ResetJump()
    {
        jumpCount = 0;
    }

    private void SetGrounded(bool _IsGrounded)
    {
        isGrounded = _IsGrounded;
        playerAnimator.SetBool("isGrounded", _IsGrounded);

        if(isGrounded)
        {
            rb.gravityScale = GravityScale;
            currentSpeed = playerSpeed;
        }
    }

    private void SetWallSliding(bool _IsWallSliding)
    {
        isWallSliding = _IsWallSliding;
        playerAnimator.SetBool("isWallSliding", _IsWallSliding);

        if(isWallSliding)
        {
            rb.gravityScale = GravityScale / 5;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            currentSpeed = 0.0f;
        }
    }

    private bool CheckIfGrounded(Collision2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            BoxCollider2D _platformCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            float playerBottom = playerCollider.bounds.min.y;
            float platformTop = _platformCollider.bounds.max.y;

            bool _isPlayerUnderPlatform = playerBottom < platformTop;

            return !_isPlayerUnderPlatform;
        }

        Vector2 origin = transform.position - Vector3.up * playerCollider.size.y;

        // Perform the BoxCast
        RaycastHit2D boxcastHit = Physics2D.BoxCast(origin, playerCollider.size, 0f, Vector2.down, 0, groundLayer);

        return boxcastHit.collider != null;
    }

    private bool CheckIfOnWall()
    {
        RaycastHit2D boxcastHitLeft = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.left, 0.1f, wallLayer);
        RaycastHit2D boxcastHitRight = Physics2D.BoxCast(playerCollider.bounds.center, playerCollider.bounds.size, 0, Vector2.right, 0.1f, wallLayer);

        bool isWallDetected = boxcastHitLeft.collider != null || boxcastHitRight.collider != null;
        return isWallDetected;
    }
}
