using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class sSpitflameController : MonoBehaviour
{
    public GameObject projectileGO;
    public float speed = 1.0f;
    public float attackCooldown = 1.0f;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    // Components
    private Rigidbody2D rb;
    private Animator animator;

    private bool isAttacking = false;
    private int spitflameDirection = 1;

    private int wallLayerNumber;
    private int groundLayerNumber;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        wallLayerNumber = (int)(Mathf.Log(wallLayer.value) / Mathf.Log(2));
        groundLayerNumber = (int)(Mathf.Log(groundLayer.value) / Mathf.Log(2));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TryAttackPlayer());
    }

    // Update is called once per frame
    void Update()
    {
        if(IsVoidInFront())
            RotateCharacter();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == wallLayerNumber)
            RotateCharacter();
    }

    private bool IsVoidInFront()
    {
        Vector2 origin = transform.position + (Vector3.right * spitflameDirection * 0.01f) + Vector3.down * 0.5f;
        Vector2 size = new Vector2(0.1f, 0.1f);
        float distance = 0.1f;
        bool hasFoundGround = false;

        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, size, 0.0f, Vector2.zero, distance);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.gameObject.layer == groundLayerNumber)
            {
                hasFoundGround = true;
                break;
            }
        }

        return !hasFoundGround;
    }

    private bool IsPlayerAttackable()
    {
        Vector2 center = new Vector2(0.0f, transform.position.y);
        Vector2 size = new Vector2(4, 1);
        Collider2D hit = Physics2D.OverlapBox(center, size, 0, playerLayer);

        if(hit == null)
            return false;

        if (hit.transform.gameObject.tag == "Player")
            return true;

        return false;
    }

    private void RotateCharacter()
    {
        spitflameDirection *= -1;

        float newScaleX = Math.Abs(transform.localScale.x) * spitflameDirection;
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator TryAttackPlayer()
    {
        bool m_canAttack = IsPlayerAttackable();
        if (m_canAttack)
        {
            animator.SetTrigger("onAttack");
            yield return new WaitForSeconds(0.15f); // 0.18f == wait for animation to be at the frame where it shoot
            Shoot();
        }

        yield return new WaitForSeconds(attackCooldown);

        StartCoroutine(TryAttackPlayer());
    }

    private void Shoot()
    {
        GameObject spawnedProjectile = Instantiate(projectileGO);
        spawnedProjectile.transform.position = transform.position;

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        float shootDirection = playerGO.transform.position.x > transform.position.x ? 1 : -1;

        spawnedProjectile.GetComponent<sProjectileBehavior>().LaunchProjectile(new Vector3(shootDirection, 0.0f, 0.0f));
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(speed * spitflameDirection, rb.velocity.y);
        }
    }
}
