using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCheckpointBehvior : MonoBehaviour
{
    public int checkpointCost = 10;
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
        if (playerBehavior.coinsNb < checkpointCost)
            return;

        playerBehavior.lastCheckpoint = this.gameObject;
        playerBehavior.coinsNb -= checkpointCost;
        spriteRenderer.color = Color.green;
    }
}
