using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sWinPlatformBehavior : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public GameObject WinMenuGO;

    private bool isTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" || isTriggered)
            return;

        StartCoroutine(OnWin());
    }

    IEnumerator OnWin()
    {
        isTriggered = true;

        particleSystem.Play();

        yield return new WaitForSeconds(2.0f);

        Instantiate(WinMenuGO, GameObject.FindGameObjectWithTag("Canvas").transform);

        GameObject.FindGameObjectWithTag("Player").GetComponent<sPlayerMovements>().canMove = false;
    }
}
