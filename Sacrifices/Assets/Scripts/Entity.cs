using System;
using UnityEngine;

public class Entity : MonoBehaviour
{

    protected Rigidbody2D rb;
    protected float health = 20;
    protected float speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
