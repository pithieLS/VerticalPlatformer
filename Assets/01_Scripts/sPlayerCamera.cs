using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPlayerCamera : MonoBehaviour
{
    public float smoothSpeed = 0.05f;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float targetLocationY = player.transform.position.y;

        float smoothedLocationY = Mathf.Lerp(transform.position.y, targetLocationY, smoothSpeed);

        transform.position = new Vector3(0, smoothedLocationY, transform.position.z);
    }
}
