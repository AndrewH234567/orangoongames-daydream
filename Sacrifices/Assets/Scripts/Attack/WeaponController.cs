using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Setup")]
    [SerializeField] private Projectile projectile;
    [SerializeField] private Entity parent;

    [SerializeField] private Transform firePoint;
    
    [Header("Behavior Settings")]
    [Tooltip("Check for Ranged (default) | Uncheck for Melee (shotgun-style)")]
    public int weaponId = 0; 

    [Header("Stats")]
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;

    [Header("Melee/Shotgun Only")]
    [Tooltip("Projectiles to spawn for the 'hitbox'")]
    public int projectilesPerShot = 5;
    [Tooltip("Total spread angle in degrees (e.g., 40 for a wide arc)")]
    public float spreadAngle = 40f; 
    public float meleeLifeTime = 0.15f;

    private float nextFireTime = 0f;

    private Vector3 originalFirePointLocalPosition;

    private bool isFlipped = false;


    // --- Public method called by your InputHandler when Attack is pressed ---
    public void TryAttack(Vector2 aimDirection)
    {
        if (Time.time < nextFireTime)
        {
            return;
        }

        // Calculate the next time we can fire
        nextFireTime = Time.time + fireRate;

        // Ensure we have a fire point and a projectile prefab
        if (firePoint == null || projectile == null)
        {
            Debug.LogError("WeaponController is missing Fire Point or Projectile Prefab reference!");
            return;
        }

        if (isFlipped) aimDirection = new Vector2(-aimDirection.x, aimDirection.y);
        if (weaponId == 0)
        {
            FireRanged(aimDirection);
        }
        else if (weaponId == 1)
        {
            FireMelee(aimDirection);
        }
    }

    void Awake()
    {
        originalFirePointLocalPosition = firePoint.localPosition;
    }

    void Update()
    {
        FlipSprite();
    }

    private void FlipSprite()
    {
        float horizontalVelocity = parent.getRigidBody().linearVelocity.x;

        if (horizontalVelocity > 0.01f) // Moving Right
        {
            firePoint.GetComponent<SpriteRenderer>().flipX = false; //Make this get from an actual weapon class
            FlipFirePoint(false); // Player faces right
        }
        else if (horizontalVelocity < -0.01f) // Moving Left
        {
            firePoint.GetComponent<SpriteRenderer>().flipX = true;
            FlipFirePoint(true); // Player faces left
        }
    }

    private void FlipFirePoint(bool facingLeft)
    {
        if (firePoint == null) return;
        isFlipped = facingLeft;

        Vector3 newPosition = originalFirePointLocalPosition;

        if (facingLeft)
        {
            // Invert the X position to move it to the opposite side
            newPosition.x = -originalFirePointLocalPosition.x;
        }

        // Apply the new local position
        firePoint.localPosition = newPosition;
    }

    private void FireRanged(Vector2 direction)
    {
        SpawnProjectile(direction, direction, 1, 0f);
    }

    private void FireMelee(Vector2 direction)
    {
        // Melee is a spread of projectiles with a very short lifeTime
        
        // Calculate the starting angle based on the direction vector
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float halfSpread = spreadAngle / 2f;
        
        for (int i = 0; i < projectilesPerShot; i++)
        {
            float angleOffset = -halfSpread + (float)i / (projectilesPerShot - 1) * spreadAngle;
            
            float finalAngle = baseAngle + angleOffset;
            Vector2 spreadDirection = new Vector2(
                Mathf.Cos(finalAngle * Mathf.Deg2Rad),
                Mathf.Sin(finalAngle * Mathf.Deg2Rad)
            );

            SpawnProjectile(spreadDirection, direction, projectilesPerShot, angleOffset);
        }
    }

    private void SpawnProjectile(Vector2 travelDirection, Vector2 aimDirection, int burstCount, float angleOffset)
    {
        Projectile newProjectile = Instantiate(projectile, firePoint.position, firePoint.rotation);
        
        float projectileLifeTime = weaponId == 0 ? 5f : meleeLifeTime;
        
        newProjectile.Initialize(
            travelDirection,
            projectileSpeed,
            projectileLifeTime,
            this.gameObject
        );

        newProjectile.transform.right = travelDirection;
    }
}