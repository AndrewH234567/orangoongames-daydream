using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Random.Range(1, 100) > 50)
            rb.AddForceX(20);
        else
            rb.AddForceX(-20);
    }
}
