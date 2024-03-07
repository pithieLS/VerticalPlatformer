using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sSlideFX : MonoBehaviour
{
    public float velocityMax = 3.0f;
    public float velocityTreshold = 1.0f;
    public Vector2 minSize = new Vector2(0.0f, 0.0f);
    public Vector2 maxSize = new Vector2(0.1f, 0.3f);

    private ParticleSystem slideParticleSystem;
    private Rigidbody2D playerRb;

    private void Awake()
    {
        slideParticleSystem = GetComponent<ParticleSystem>();
        playerRb = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.MainModule mainModule = slideParticleSystem.main;

        if (Mathf.Abs(playerRb.velocity.y) < velocityTreshold)
        {
            mainModule.startSize = new ParticleSystem.MinMaxCurve(0.0f, 0.0f);
            return;
        }

        float m_alphaVelocity = Mathf.Abs(playerRb.velocity.y) / velocityMax;
        Vector2 m_newSize = Vector2.Lerp(minSize, maxSize, m_alphaVelocity);

        mainModule.startSize = new ParticleSystem.MinMaxCurve(m_newSize.x, m_newSize.y);
    }
}
