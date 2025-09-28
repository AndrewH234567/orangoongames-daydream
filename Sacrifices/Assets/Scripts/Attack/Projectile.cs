using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Behavior")]
    public float lifeTime;
    
    private Rigidbody2D rb;
    
    public float damage = 1;
    public GameObject parent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed, float projectileLifeTime, GameObject parent)
    {
        lifeTime = projectileLifeTime;
        this.parent = parent;

        // Apply initial velocity instantly
        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * speed;
        }

        Invoke(nameof(DestroyProjectile), lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ignore collision with the owner/self-hit
        if (other.gameObject == parent || other.name == name)
            return;

        // Get the Entity (or subclass) component, if it exists
        Entity entity = other.GetComponent<Entity>();
        if (entity != null)
        {
            Debug.Log($"Projectile hit an Entity: {entity.name}");

            // Apply damage here
            // entity.TakeDamage(damage);
            entity.addHp(-damage);
            float newHP = entity.getHp();
            Debug.Log(newHP);
        }

        DestroyProjectile();
    }


    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
