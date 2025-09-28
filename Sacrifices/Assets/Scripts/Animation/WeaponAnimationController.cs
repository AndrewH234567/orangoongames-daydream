using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimationController : MonoBehaviour
{
    private Animator animator;

    [Header("Animation Settings")]
    [Tooltip("The max (in seconds) of the attack animation clip.")]
    [SerializeField] private float maxAnimationTime = 0.5f;

    [Header("Animator Parameters")]
    [Tooltip("The name of the Float parameter to track progress (e.g., 'AttackTime').")]
    [SerializeField] private string timeFloatName = "AttackTime";
    [Tooltip("The speed of the animatoin")]
    [SerializeField] private float speed;

    private bool isAnimating = false;
    private float animationTimer = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
        animationTimer = maxAnimationTime;
        animator.SetFloat(timeFloatName, maxAnimationTime);
    }

    // This method must be called by the WeaponController's TryAttack() method.
    public void StartAttackAnimation()
    {
        if (isAnimating)
        {
            return;
        }

        animator.speed = speed;
        animationTimer = 0f;
        isAnimating = true;
        
        animator.SetFloat(timeFloatName, 0f); 
    }

    void Update()
    {
        if (!isAnimating)
        {
            return;
        }

        // Increment the timer based on real time
        animationTimer += Time.deltaTime;

        // Check if the attack animation duration has ended
        if (animationTimer >= maxAnimationTime
)
        {
            // Cap the value and stop the update cycle
            isAnimating = false;
            animationTimer = maxAnimationTime
    ;
        }

        // Push the current time progress to the Animator
        animator.SetFloat(timeFloatName, animationTimer);
    }
}
