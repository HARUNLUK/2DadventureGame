using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour
{
    float walkTime = 5;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > walkTime)
        {
            walk();
            walkTime += 5;
        }
    }

    private void walk()
    {
        
    }
}
