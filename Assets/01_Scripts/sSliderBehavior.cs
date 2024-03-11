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

        playerMovement.HandleWallCollision();

        isPlayerOnSlider = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        isPlayerOnSlider = false;
    }
}
