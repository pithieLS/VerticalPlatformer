using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sProjectileBehavior : MonoBehaviour
{
    private Rigidbody2D rb;
    public int speed;
    [HideInInspector] public Vector3 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            return;

        Destroy(this);
    }

    public void LaunchProjectile(Vector3 inDirection)
    {
        direction = inDirection;
    }
}
