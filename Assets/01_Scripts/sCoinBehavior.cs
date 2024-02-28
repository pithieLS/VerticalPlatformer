using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sCoinBehavior : MonoBehaviour
{
    public Animator coinAnimator;

    private bool bIsCollected = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void OnCollected()
    {
        if (bIsCollected)
            return;

        Destroy(GetComponentInChildren<CircleCollider2D>());

        bIsCollected = true;
        coinAnimator.SetBool("isCoinCollected", true);

        sCoinCountUI coinCountUI = GameObject.FindObjectOfType<sCoinCountUI>();
        coinCountUI.UpdateCounter();

        StartCoroutine(CoinCollectedCorroutine());
    }

    IEnumerator CoinCollectedCorroutine()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }
}
