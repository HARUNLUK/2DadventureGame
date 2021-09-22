using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxControl : MonoBehaviour
{
    private float length, startpos,y,z;
    public GameObject cam;
    [Range(0, 1f)] public float parallaxEffect;

    void Start()
    {
        startpos = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startpos + dist, transform.position.y, z);
        if (temp > startpos + length) { startpos += length; }
        else if (temp < startpos - length) { startpos -= length; }
    }
}
