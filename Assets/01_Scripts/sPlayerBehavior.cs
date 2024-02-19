using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayerBehavior : MonoBehaviour
{
    [HideInInspector] public GameObject lastCheckpoint;
    [HideInInspector] public int coinsNb;

    private Vector2 startLocation;
    // Start is called before the first frame update
    void Start()
    {
        startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null)
            return;

        if(collision.gameObject.tag == "Spike")
            OnHitSpikes();

        if (collision.gameObject.tag == "Coin")
            coinsNb++;
    }

    void OnHitSpikes()
    {
        // TODO: play anim, then respawn
        Respawn();
    }

    void Respawn()
    {
        // TODO
        if(lastCheckpoint == null)
            transform.position = startLocation;

        transform.position = lastCheckpoint.transform.position;
    }
}
