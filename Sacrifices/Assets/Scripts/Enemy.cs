using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rb;
    private float health = 20;
    private float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UnityEngine.Random.Range(1, 100) > 50)
            rb.AddForceX(speed);
        else
            rb.AddForceX(-speed);
    }

    public float getHp()
    {
        return health;
    }

    public void addHp(float amt)
    {
        health += amt;
    }

    public float getSpeed()
    {
        return speed;
    }

    public void addSpeed(float amt)
    {
        speed += amt;
    }
}
