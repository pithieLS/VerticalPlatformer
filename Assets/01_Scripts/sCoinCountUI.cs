using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class sCoinCountUI : MonoBehaviour
{
    public TextMeshProUGUI coinCountTMP;
    // Start is called before the first frame update
    private void Start()
    {
        // We wait for everything to be initialized in the case that the player isn't loaded when UpdateCounter()
        StartCoroutine(LateStartCorroutine());
    }

    IEnumerator LateStartCorroutine()
    {
        yield return new WaitForSeconds(0.2f);

        UpdateCounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateCounter()
    {
        sPlayerBehavior playerBehavior = GameObject.FindObjectOfType<sPlayerBehavior>();

        coinCountTMP.SetText(playerBehavior.coinsNb.ToString("000"));
    }
}