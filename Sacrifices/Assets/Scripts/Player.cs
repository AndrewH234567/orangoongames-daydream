using UnityEngine;
using System.Collections;

public class AnimationWithDelay : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(SlowAnimation());
    }

    IEnumerator SlowAnimation()
    {
        while (true)
        {
            animator.speed = 1;  // Play one frame or a bit
            yield return new WaitForSeconds(0.155f);  // wait 155 ms
            animator.speed = 0;  // pause animation
            yield return new WaitForSeconds(0.155f);  // wait 155 ms before next step
        }
    }
}