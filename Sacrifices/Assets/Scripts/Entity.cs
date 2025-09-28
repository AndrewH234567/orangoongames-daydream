using System;
using UnityEngine;

public class Entity : MonoBehaviour
{

    [Header("Movement Attributes")]
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] public float jumpForce = 8f;
    [SerializeField] public bool isJumping = false;

    protected float currentFacingDirection = 1f;

    [Header("DEBUG")]

    public Vector2 movement;

    protected Rigidbody2D rb;
    protected float health = 20;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float GetHp()
    {
        return health;
    }

    public void AddHp(float amt)
    {
        health += amt;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void AddSpeed(float amt)
    {
        moveSpeed += amt;
    }

    public Rigidbody2D getRigidBody()
    {
        return rb;
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
