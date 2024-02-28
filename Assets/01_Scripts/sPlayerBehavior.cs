using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayerBehavior : MonoBehaviour
{
    // FX related
    [Header("FX Related")]
    public GameObject landingFX;
    public GameObject trailFX;
    public GameObject playerSlideFX;
    public Shader allWhiteShader;

    [HideInInspector] public GameObject lastCheckpoint;
    [HideInInspector] public int coinsNb;

    private SpriteRenderer spriteRenderer;
    private Vector2 startPosition;
    private sPlayerMovements playerMovement;
    Animator playerAnimator;

    private void Awake()
    {
        playerMovement = GetComponent<sPlayerMovements>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerAnimator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Spike")
            OnHitSpikes();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coinsNb++;
            sCoinBehavior _coinBehavior = collision.gameObject.GetComponentInParent<sCoinBehavior>();
            _coinBehavior.OnCollected();
        }
    }

    void OnHitSpikes()
    {
        // TODO: play anim, then respawn
        StartCoroutine(RespawnCoroutine());
    }

    IEnumerator RespawnCoroutine()
    {
        GameObject trailGO = SpawnTrailFX(gameObject);

        float timer = 0;
        float alpha = 0;
        float lerpDuration = 0.4f;

        Vector3 lastPosition = transform.position;
        Vector3 respawnPosition = lastCheckpoint ? lastCheckpoint.transform.position : startPosition;
        Vector3 lerpedPosition;

        Shader m_defaultShader = spriteRenderer.material.shader;
        spriteRenderer.material.shader = allWhiteShader; // Make player all white

        while (alpha < 1)
        {
            // Pour que ce soit smooth on utilise le sinus
            alpha = timer / lerpDuration;
            alpha = Mathf.Lerp(-Mathf.PI / 2, Mathf.PI / 2, alpha);
            alpha = Mathf.Sin(alpha);
            alpha = (alpha / 2) + 0.5f; // On scale la valeur [0,1] pcq le sinus est [-1,1], j'ai pas trouvé de meilleur moyen et flemme de chercher

            lerpedPosition = Vector3.Lerp(lastPosition, respawnPosition, alpha);

            transform.position = lerpedPosition;

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = respawnPosition;
        
        playerMovement.HandleGroundCollision();

        spriteRenderer.material.shader = m_defaultShader; // Reset player color (w/ shader)

        // Detach the trail and wait for the trail to finish then destroy it
        trailGO.transform.parent = null;
        yield return new WaitForSeconds(1f);
        Destroy(trailGO);
    }


    //// FXs
    public void SpawnLandingFX()
    {
        Instantiate(landingFX, transform.position, Quaternion.identity);
    }

    public GameObject SpawnTrailFX(GameObject attachTarget)
    {
        GameObject trailGO = Instantiate(trailFX, transform.position, Quaternion.identity);
        trailGO.transform.parent = attachTarget.transform;

        return trailGO;
    }

    public void EnableSlideFX(bool isEnabled)
    {
        ParticleSystem slideParticleSystem = playerSlideFX.GetComponent<ParticleSystem>();

        if(isEnabled)
            slideParticleSystem.Play();
        else
            slideParticleSystem.Stop();
    }
}
