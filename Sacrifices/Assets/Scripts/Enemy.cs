using System;
using UnityEngine;


public class Enemy : Entity
{

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Random.Range(1, 100) > 50)
            rb.AddForceX(speed);
        else
            rb.AddForceX(-speed);
    }
}
