using UnityEngine;

// This ensures the necessary components are always present on the GameObject
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class AnimationManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    [Header("Configuration")]
    [Tooltip("Minimum horizontal speed to transition from Idle to Walk.")]
    [SerializeField] private float walkThreshold = 0.1f;
    [Tooltip("Vertical speed threshold to consider the player airborne.")]
    [SerializeField] private float airThreshold = 0.05f;

    private enum MovementState { Idle, Walking, Jumping, Falling }
    private MovementState currentState = MovementState.Idle;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        DetermineState();

        UpdateAnimator();

        FlipSprite();
    }

    // ----------------------
    // STATE LOGIC
    // ----------------------

    private void DetermineState()
    {
        float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);
        float verticalSpeed = rb.linearVelocity.y;

        bool isAirborne = Mathf.Abs(verticalSpeed) > airThreshold;

        if (isAirborne)
        {
            currentState = (verticalSpeed > 0) ? MovementState.Jumping : MovementState.Falling;
        }
        else // We are on the ground (Vertical speed is near zero)
        {
            if (horizontalSpeed > walkThreshold)
            {
                currentState = MovementState.Walking;
            }
            else
            {
                currentState = MovementState.Idle;
            }
        }
    }

    // ----------------------
    // ANIMATOR CONTROL
    // ----------------------

    private void UpdateAnimator()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        bool isAirborne = currentState == MovementState.Jumping || currentState == MovementState.Falling;
        anim.SetBool("IsAirborne", isAirborne);
    }

    // ----------------------
    // SPRITE FLIPPING
    // ----------------------

    private void FlipSprite()
    {
        float horizontalVelocity = rb.linearVelocity.x;

        if (horizontalVelocity > 0.01f) // Moving Right
        {
            // FlipX = false keeps the sprite facing right
            spriteRenderer.flipX = false;
        }
        else if (horizontalVelocity < -0.01f) // Moving Left
        {
            // FlipX = true flips the sprite to face left
            spriteRenderer.flipX = true;
        }
        // If speed is near zero, the sprite maintains its last direction (good for idling).
    }
}