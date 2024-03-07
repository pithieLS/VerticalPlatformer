using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class sSliderBehavior : MonoBehaviour
{
    public bool isGoingRight;

    private bool isPlayerOnSlider = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sPlayerMovements playerMovement = collision.gameObject.GetComponent<sPlayerMovements>();
        if (playerMovement == null)
            return;

        if (isPlayerOnSlider)
            return;

        isPlayerOnSlider = true;

        Rigidbody2D playerRb = playerMovement.GetComponent<Rigidbody2D>();

        playerMovement.playerDirection = isGoingRight ? 1 : -1;

        // Scale sprite in relation to it's direction
        float newScaleX = Math.Abs(playerMovement.transform.localScale.x) * playerMovement.playerDirection;
        playerMovement.transform.localScale = new Vector3(newScaleX, playerMovement.transform.localScale.y, playerMovement.transform.localScale.z);

        playerMovement.isWallSliding = true;

        playerMovement.GetComponent<Animator>().SetBool("isWallSliding", true);
        
        playerMovement.GetComponent<sPlayerBehavior>().EnableSlideFX(true);

        playerRb.velocity = new Vector2(0, 0);

        playerMovement.currentSpeed = playerMovement.playerSpeed / 2;
        playerMovement.ResetJump();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        isPlayerOnSlider = false;
        sPlayerMovements playerMovement = collision.gameObject.GetComponent<sPlayerMovements>();
        playerMovement.GetComponent<Animator>().SetBool("isWallSliding", false);
    }
}
