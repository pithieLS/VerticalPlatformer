using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCheckpointBehvior : MonoBehaviour
{
    public int checkpointCost = 10;
    private bool isTooked = false;
    private SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    public Sprite checkpointGreen;
    public Sprite checkpointOrange;
    public Sprite checkpointRed;

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
        if (isTooked)
            return;

        if (collision.gameObject.tag != "Player")
            return;

        OnTooked(collision.gameObject);
    }

    private void OnTooked(GameObject _player)
    {
        sPlayerBehavior playerBehavior = _player.GetComponent<sPlayerBehavior>();
        if (playerBehavior.coinsNb < checkpointCost)
        {
            StartCoroutine(OnCantBuy());
            return;
        }

        BuyCheckpoint(playerBehavior);
    }

    IEnumerator OnCantBuy()
    {
        spriteRenderer.sprite = checkpointRed;

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.sprite = checkpointOrange;
    }

    private void BuyCheckpoint(sPlayerBehavior inPlayerBehavior)
    {
        inPlayerBehavior.lastCheckpoint = this.gameObject;
        inPlayerBehavior.coinsNb -= checkpointCost;
        spriteRenderer.sprite = checkpointGreen;

        sCoinCountUI coinCountUI = GameObject.FindObjectOfType<sCoinCountUI>();
        coinCountUI.UpdateCounter();
    }
}
