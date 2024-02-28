using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCheckpointBehvior : MonoBehaviour
{
    public int checkpointCost = 10;
    private bool isTooked = false;
    private SpriteRenderer spriteRenderer;

    private Color colorOrange = new Color(767.0f, 90.0f, 0.0f, 0.0f);
    private Color colorRed = new Color(1367.0f, 0.0f, 0.0f, 0.0f);
    private Color colorGreen = new Color(0.0f, 1825.0f, 0.0f, 0.0f);

    [Header("Sprites")]
    public Sprite checkpointGreen;
    public Sprite checkpointOrange;
    public Sprite checkpointRed;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        spriteRenderer.material.SetColor("_GlowColor", colorOrange);

        Debug.Log(spriteRenderer.material.GetColor("_GlowColor"));
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
        spriteRenderer.material.SetColor("_GlowColor", colorRed);

        spriteRenderer.sprite = checkpointRed;

        yield return new WaitForSeconds(0.5f);

        spriteRenderer.material.SetColor("_GlowColor", colorOrange);
        spriteRenderer.sprite = checkpointOrange;
    }

    private void BuyCheckpoint(sPlayerBehavior inPlayerBehavior)
    {
        if (isTooked)
            return;

        isTooked = true;

        inPlayerBehavior.lastCheckpoint = this.gameObject;
        inPlayerBehavior.coinsNb -= checkpointCost;

        spriteRenderer.material.SetColor("_GlowColor", colorGreen);
        spriteRenderer.sprite = checkpointGreen;

        sCoinCountUI coinCountUI = GameObject.FindObjectOfType<sCoinCountUI>();
        coinCountUI.UpdateCounter();
    }
}
