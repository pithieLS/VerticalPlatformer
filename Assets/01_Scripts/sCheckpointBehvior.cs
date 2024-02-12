using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCheckpointBehvior : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        OnTooked(collision.gameObject);
    }

    private void OnTooked(GameObject _player)
    {
        sPlayerBehavior playerBehavior = _player.GetComponent<sPlayerBehavior>();

        playerBehavior.lastCheckpoint = this.gameObject;

        spriteRenderer.color = Color.green;
    }
}
