using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sStartFX : MonoBehaviour
{
    public float animLenght = 1.0f;
    public Vector3 deviation;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position + deviation;
        Destroy(gameObject, animLenght);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
