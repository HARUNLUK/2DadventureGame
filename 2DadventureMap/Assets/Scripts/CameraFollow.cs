using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;

    void Start()
    {
        if (transform != null)
        {
            transform.position = target.position + offset;
        }
    }

    private void Update()
    {
        if (transform != null)
        {
            transform.position = target.position + offset;
        }
    }
}
