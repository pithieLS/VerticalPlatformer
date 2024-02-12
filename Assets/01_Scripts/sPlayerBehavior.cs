using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayerBehavior : MonoBehaviour
{
    [HideInInspector] public GameObject lastCheckpoint;
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
        if (collision == null)
            return;

        if(collision.gameObject.tag == "Spike")
            OnHitSpikes();
    }

    void OnHitSpikes()
    {
        // TODO
        Respawn();
    }

    void Respawn()
    {
        // TODO
        transform.position = lastCheckpoint.transform.position;
    }
}
