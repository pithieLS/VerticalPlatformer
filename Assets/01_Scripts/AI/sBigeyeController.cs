using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class sBigeyeController : MonoBehaviour
{
    public GameObject projectileGO;
    public float speed = 1.0f;
    public float attackCooldown = 1.0f;
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public LayerMask playerLayer;

    // Components
    private Animator animator;
    private Rigidbody2D rb;

    private bool isAttacking = false;
    private int bigeyeDirection = 1;

    private int wallLayerNumber;
    private int groundLayerNumber;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        wallLayerNumber = (int)(Mathf.Log(wallLayer.value) / Mathf.Log(2));
        groundLayerNumber = (int)(Mathf.Log(groundLayer.value) / Mathf.Log(2));
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TryAttackPlayer());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == wallLayerNumber)
            RotateCharacter();
    }

    private bool IsPlayerAttackable()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");

        float dist = Vector3.Distance(playerGO.transform.position, transform.position);
        if (dist < 6)
            return true;

        return false;
    }

    private void RotateCharacter()
    {
        bigeyeDirection *= -1;

        float newScaleX = Math.Abs(transform.localScale.x) * bigeyeDirection;
        transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
    }

    private IEnumerator TryAttackPlayer()
    {
        bool m_canAttack = IsPlayerAttackable();
        if (m_canAttack)
        {
            animator.SetTrigger("onAttack");
            Shoot();
        }

        yield return new WaitForSeconds(attackCooldown);

        StartCoroutine(TryAttackPlayer());
    }

    private void Shoot()
    {
        GameObject spawnedProjectile = Instantiate(projectileGO);
        spawnedProjectile.transform.position = transform.position;

        spawnedProjectile.GetComponent<sProjectileBehavior>().LaunchProjectile(Vector3.down);
    }

    private void FixedUpdate()
    {
        if (!isAttacking)
        {
            rb.velocity = new Vector2(speed * bigeyeDirection, rb.velocity.y);
        }
    }
}
