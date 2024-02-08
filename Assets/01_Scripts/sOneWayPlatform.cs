using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sOneWayPlatform : MonoBehaviour
{
    public PlatformEffector2D platformEffector;
    public Collider2D platformCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject _player = GameObject.FindGameObjectWithTag("Player");

        Collider2D _playerCollider = _player.GetComponent<Collider2D>();

        float playerBottom = transform.TransformPoint(_playerCollider.bounds.min).y;
        float platformTop = transform.TransformPoint(platformCollider.bounds.max).y;

        bool _isPlayerUnderPlatform = playerBottom < platformTop;

        float effectorAngle = _isPlayerUnderPlatform ? 0 : 180;

        platformEffector.rotationalOffset = effectorAngle;
    }
}
