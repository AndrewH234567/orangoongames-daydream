using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Behavior")]
    public float lifeTime;
    
    private Rigidbody2D rb;
    
    public float damage;
    public GameObject parent;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction, float speed, float damageAmount, float projectileLifeTime, GameObject parent)
    {
        damage = damageAmount;
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
        {
            return;
        }

        // --- PLACEHOLDER DAMAGE LOGIC ---
        // In a real game, you would check for a "Health" component here.
        Debug.Log($"Projectile hit: {other.name}. Applying {damage} damage.");
        
        DestroyProjectile();
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
