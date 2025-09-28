using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WeaponAnimationController : MonoBehaviour
{
    private Animator animator;

    [Header("Animation Settings")]
    [Tooltip("The duration (in seconds) of the attack animation clip.")]
    [SerializeField] private float animationDuration = 0.5f;

    [Header("Animator Parameters")]
    [Tooltip("The name of the Trigger parameter to start the animation (e.g., 'Attack').")]
    [SerializeField] private string attackTriggerName = "Attack";
    [Tooltip("The name of the Float parameter to track progress (e.g., 'AttackTime').")]
    [SerializeField] private string timeFloatName = "AttackTime";

    private bool isAnimating = false;
    private float animationTimer = 0f;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // This method must be called by the WeaponController's TryAttack() method.
    public void StartAttackAnimation()
    {
        if (isAnimating)
        {
            return; // Ignore the call if an attack is already running
        }

        // 1. Reset timer and flag
        animationTimer = 0f;
        isAnimating = true;

        // 2. Trigger the animation state change
        animator.SetTrigger(attackTriggerName);
        
        // 3. Immediately set the time parameter to 0
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
        if (animationTimer >= animationDuration)
        {
            // Cap the value and stop the update cycle
            isAnimating = false;
            animationTimer = animationDuration;
        }

        // Push the current time progress to the Animator
        animator.SetFloat(timeFloatName, animationTimer);
    }
}
