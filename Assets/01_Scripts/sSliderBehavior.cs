using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class sSliderBehavior : MonoBehaviour
{
    public bool isGoingRight;

    private bool isPlayerOnSlider = false;
    // Start is called before the first frame update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sPlayerMovements playerMovement = collision.gameObject.GetComponent<sPlayerMovements>();
        if (playerMovement == null)
            return;

        if (isPlayerOnSlider)
            return;

        playerMovement.HandleWallCollision();

        isPlayerOnSlider = true;

        playerMovement.playerDirection = isGoingRight ? 1 : -1;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        isPlayerOnSlider = false;
    }
}
