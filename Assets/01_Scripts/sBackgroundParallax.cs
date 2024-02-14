using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class sBackgroundParallax : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffectAmplitude;
    private float startPos;


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = cam.transform.position.y * parallaxEffectAmplitude;

        transform.position = new Vector3(transform.position.x, startPos + dist, transform.position.z);
    }
}
