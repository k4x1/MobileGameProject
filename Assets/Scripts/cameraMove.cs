using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    Transform PlayerTransform;
    Vector3 distanceFromPlayer;
    private void Start()
    {
        PlayerTransform = GameObject.FindWithTag("Player").transform;
        distanceFromPlayer = transform.position;
    }
    private void Update()
    {
        transform.position = PlayerTransform.position + distanceFromPlayer;
    }
}
